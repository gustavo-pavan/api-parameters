using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Parameters.Helper.Events.EventRabbitMQ.Connection;

public class RabbitMQConnection : IRabbitMQConnection
{
    private readonly IConnectionFactory _connectionFactory;

    private readonly ILogger<RabbitMQConnection> _logger;

    private readonly int _retryCount;

    private readonly object _syncRoot = new();

    private IConnection? _connection;

    public bool Disposed;

    public RabbitMQConnection(IConnectionFactory connectionFactory, ILogger<RabbitMQConnection> logger,
        int retryCount = 3)
    {
        _connectionFactory = connectionFactory;
        _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<RabbitMQConnection>));
        _retryCount = retryCount;
    }

    public bool IsConnected => _connection is { IsOpen: true } && !Disposed;

    public IModel CreateModel()
    {
        if (!IsConnected)
            throw new InvalidOperationException("No RabbitMq connection!");

        _logger.LogInformation("Create model rabbitmq!");
        ;
        return _connection!.CreateModel();
    }

    public void Dispose()
    {
        if (Disposed) return;

        Disposed = true;

        try
        {
            _connection!.ConnectionShutdown -= OnConnectionShutdown;
            _connection!.CallbackException -= OnCallbackException;
            _connection!.ConnectionBlocked -= OnConnectionBlocked;
            _connection.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception: {exception}", ex.Message);
        }
    }

    public bool TryConnect()
    {
        _logger.LogInformation("RabbitMQ client is trying connect");

        lock (_syncRoot)
        {
            var policy = Policy.Handle<SocketException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(_retryCount,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (ex, time) =>
                    {
                        _logger.LogWarning(ex,
                            "RabbitMQ Client could not connect after {TimeOut} ({ExceptionMessage})",
                            $"{time.TotalSeconds:n1}", ex.Message);
                    });

            policy.Execute(() => { _connection = _connectionFactory.CreateConnection(); });

            if (IsConnected)
            {
                _connection!.ConnectionShutdown += OnConnectionShutdown;
                _connection!.ConnectionBlocked += OnConnectionBlocked;
                _connection!.CallbackException += OnCallbackException;

                _logger.LogInformation(
                    "RabbitMQ client acquired a connection to '{HostName}' and is subscribed to failure events",
                    _connection.Endpoint.HostName);

                return true;
            }

            _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened");
            return false;
        }
    }

    private void OnConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
    {
        if (Disposed) return;

        _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect");
        TryConnect();
    }

    private void OnConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        if (Disposed) return;

        _logger.LogWarning("A RabbitMQ connection is on shutdown. Try to re-connect");

        TryConnect();
    }

    private void OnCallbackException(object? sender, CallbackExceptionEventArgs e)
    {
        if (Disposed) return;

        _logger.LogWarning("A RabbitMQ connection throw exception. Try to re-connect");

        TryConnect();
    }
}