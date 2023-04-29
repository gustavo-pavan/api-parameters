using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Parameters.Helper.Events.EventBus.Entity;
using Parameters.Helper.Events.EventBus.Interfaces;
using Parameters.Helper.Events.EventRabbitMQ.Connection;
using Parameters.Helper.Extensions;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using EventBusManager = Parameters.Helper.Events.EventBus.EventBusManager;
using IModel = RabbitMQ.Client.IModel;

namespace Parameters.Helper.Events.EventRabbitMQ.Event;

public class EventRabbitMq : IEvent, IDisposable
{
    private readonly string BROKER_NAME = "";
    private readonly string TYPE_EXCHANGE = "";
    private readonly IRabbitMQConnection _rabbitMQConnection;
    private readonly ILogger<EventRabbitMq> _logger;
    private readonly IEventBusManager _eventBusManager;
    private readonly int _retryCount;
    private string? _queueName;
    private IModel _consumerChannel;
    private readonly IServiceCollection _services;
    private readonly IConfiguration _configuration;


    public EventRabbitMq(IRabbitMQConnection rabbitMQConnection, ILogger<EventRabbitMq> logger,
        IEventBusManager eventBusManager, IConfiguration configuration,
        IServiceCollection services, int retryCount = 3, string? queueName = null)
    {
        _rabbitMQConnection = rabbitMQConnection ?? throw new ArgumentNullException(nameof(IRabbitMQConnection));
        _eventBusManager = eventBusManager ?? new EventBusManager();
        _logger = logger;
        _retryCount = retryCount;
        _configuration = configuration;
        _queueName = _configuration["RabbitMQConfiguration:QUEUE_NAME"] ?? queueName;
        BROKER_NAME = _configuration["RabbitMQConfiguration:BROKER_NAME"];
        TYPE_EXCHANGE = _configuration["RabbitMQConfiguration:TYPE_EXCHANGE"];
        _services = services;
        _consumerChannel = CreateConsumerChannel();
        _eventBusManager.OnEventRemoved += SubsManagerOnEventRemoved;
    }

    private void SubsManagerOnEventRemoved(object? sender, string e)
    {
        if (!_rabbitMQConnection.IsConnected) _rabbitMQConnection.TryConnect();

        using var channel = _rabbitMQConnection.CreateModel();
        channel.QueueUnbind(_queueName,
            BROKER_NAME,
            e);

        if (_eventBusManager.IsEmpty)
        {
            _queueName = string.Empty;
            _consumerChannel.Close();
        }
    }

    private IModel CreateConsumerChannel()
    {
        if (!_rabbitMQConnection.IsConnected) _rabbitMQConnection.TryConnect();

        _logger.LogTrace("Creating RabbitMQ consumer channel");

        var channel = _rabbitMQConnection.CreateModel();

        channel.ExchangeDeclare(BROKER_NAME, TYPE_EXCHANGE);
        channel.QueueDeclare(_queueName, true, false, false, null);
        channel.CallbackException += (sender, ex) =>
        {
            _logger.LogWarning(ex.Exception, "Recreating RabbitMQ consumer channel");

            _consumerChannel.Dispose();
            _consumerChannel = CreateConsumerChannel();
            StartBasicConsume();
        };
        return channel;
    }

    private void StartBasicConsume()
    {
        _logger.LogTrace("Starting RabbitMQ basic consume");

        if (_consumerChannel != null)
        {
            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

            consumer.Received += ConsumerReceived;

            _consumerChannel.BasicConsume(
                _queueName,
                false,
                consumer);
        }
        else
        {
            _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
        }
    }

    private async Task ConsumerReceived(object sender, BasicDeliverEventArgs eventArgs)
    {
        var eventName = eventArgs.RoutingKey;
        var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

        try
        {
            if (message.ToLowerInvariant().Contains("throw-fake-exception"))
                throw new InvalidOperationException($"Fake exception requested: \"{message}\"");

            await ProcessEvent(eventName, message);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "ERROR Processing message \"{Message}\"", message);
        }

        _consumerChannel.BasicAck(eventArgs.DeliveryTag, false);
    }

    private async Task ProcessEvent(string eventName, string message)
    {
        _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);

        if (_eventBusManager.HasSubscriptionsForEvent(eventName))
        {
            var provider = _services.BuildServiceProvider();
            var subscriptions = _eventBusManager.GetHandlersForEvent(eventName);
            foreach (var subscription in subscriptions)
                if (subscription.IsDynamic)
                {
                    if (provider.GetService(subscription.HandlerType) is not IIntegrationEventDynamicHandler handler)
                        continue;
                    using dynamic eventData = JsonDocument.Parse(message);
                    await Task.Yield();
                    await handler.Handler(eventData);
                }
                else
                {
                    var handler = provider.GetService(subscription.HandlerType);
                    if (handler == null) continue;
                    var eventType = _eventBusManager.GetEventTypeByName(eventName);
                    var integrationEvent = JsonSerializer.Deserialize(message, eventType,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                    await Task.Yield();
                    await Task.FromResult(concreteType.GetMethod("Handle")
                        ?.Invoke(handler, new object[] { integrationEvent! }));
                }
        }
        else
        {
            _logger.LogWarning("No subscription for RabbitMQ event: {EventName}", eventName);
        }
    }

    public void Dispose()
    {
        if (_consumerChannel != null) _consumerChannel.Dispose();

        _eventBusManager.Clear();
    }

    public void Publish(IntegrationEvent @event)
    {
        if (!_rabbitMQConnection.IsConnected) _rabbitMQConnection.TryConnect();


        var policy = Policy.Handle<BrokerUnreachableException>()
            .Or<SocketException>()
            .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (ex, time) =>
                {
                    _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})",
                        @event.Id, $"{time.TotalSeconds:n1}", ex.Message);
                });

        var eventName = @event.GetType().Name;

        _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, eventName);

        using var channel = _rabbitMQConnection.CreateModel();
        _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);

        channel.ExchangeDeclare(BROKER_NAME, TYPE_EXCHANGE);

        var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
        {
            WriteIndented = true
        });

        policy.Execute(() =>
        {
            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2; // persistent

            _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);

            channel.BasicPublish(
                BROKER_NAME,
                eventName,
                true,
                properties,
                body);
        });
    }

    private void DoInternalSubscription(string eventName)
    {
        var containsKey = _eventBusManager.HasSubscriptionsForEvent(eventName);
        if (!containsKey)
        {
            if (!_rabbitMQConnection.IsConnected) _rabbitMQConnection.TryConnect();

            _consumerChannel.QueueBind(_queueName,
                BROKER_NAME,
                eventName);
        }
    }

    public void Subscribe<T, U>()
        where T : IntegrationEvent
        where U : IIntegrationEventHandler<T>
    {
        var eventName = _eventBusManager.GetEventKey<T>();

        _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName,
            typeof(T).GetGenericTypeName());

        DoInternalSubscription(eventName);
        _eventBusManager.AddSubscription<T, U>();
        StartBasicConsume();
    }

    public void SubscribeDynamic<T>(string @event) where T : IIntegrationEventDynamicHandler
    {
        _logger.LogInformation("Subscribing to dynamic event {EventName} with {EventHandler}", @event,
            typeof(T).GetGenericTypeName());
        DoInternalSubscription(@event);
        _eventBusManager.AddSubscription<T>(@event);
        StartBasicConsume();
    }

    public void Unsubscribe<T, U>()
        where T : IIntegrationEventHandler<U>
        where U : IntegrationEvent
    {
        var eventName = _eventBusManager.GetEventKey<T>();

        _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

        _eventBusManager.RemoveSubscription<U, T>();
    }

    public void UnsubscribeDynamic<T>(string @event) where T : IIntegrationEventDynamicHandler
    {
        _eventBusManager.RemoveDynamicSubscription<T>(@event);
    }
}