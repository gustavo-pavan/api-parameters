using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Parameters.Helper.Events.EventBus;
using Parameters.Helper.Events.EventBus.Interfaces;
using Parameters.Helper.Events.EventRabbitMQ.Connection;
using Parameters.Helper.Events.EventRabbitMQ.Event;
using RabbitMQ.Client;

namespace Parameters.Helper.Events.EventRabbitMQ.Container;

public static class RegisterEvent
{
    public static void RegisterRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        var logger = services.BuildServiceProvider()
            .GetService<ILogger<RabbitMQConnection>>();

        services.AddScoped<IRabbitMQConnection>(sp =>
        {
            var factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQConfiguration:HostName"],
                DispatchConsumersAsync = true
            };

            if (!string.IsNullOrEmpty(configuration["RabbitMQConfiguration:UserName"]))
                factory.UserName = configuration["RabbitMQConfiguration:UserName"];

            if (!string.IsNullOrEmpty(configuration["RabbitMQConfiguration:Password"]))
                factory.Password = configuration["RabbitMQConfiguration:Password"];

            var retryCount = 5;

            if (!string.IsNullOrEmpty(configuration["RabbitMQConfiguration:RetryCount"]))
                retryCount = int.Parse(configuration["RabbitMQConfiguration:RetryCount"]);


            return new RabbitMQConnection(factory, logger!, retryCount);
        });

        services.RegisterEventBus(configuration);
    }

    private static void RegisterEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventBusManager, EventBusManager>();

        var provider = services.BuildServiceProvider();

        services.AddSingleton<IEvent, EventRabbitMq>(sp =>
        {
            return new EventRabbitMq(
                provider.GetService<IRabbitMQConnection>()!,
                provider.GetService<ILogger<EventRabbitMq>>()!,
                provider.GetService<IEventBusManager>()!,
                configuration,
                services
            );
        });
    }
}