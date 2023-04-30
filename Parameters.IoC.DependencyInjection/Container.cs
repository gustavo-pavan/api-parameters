using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Parameters.Helper.Behavior;
using Parameters.Helper.Events.EventBus;
using Parameters.Helper.Events.EventRabbitMQ.Container;
using Parameters.Helper.Events.IntegrationEventLog.Context;
using Parameters.Helper.Events.IntegrationEventLog.Services;
using Parameters.Infra.Context;
using Parameters.Infra.Context.UoW;

namespace Parameters.IoC.DependencyInjection;

public static class Container
{
    public static void AddDependency(this IServiceCollection service)
    {
        service.AddScoped<IMongoContext, MongoContext>();

        service.AddMediatR(x =>
            {
                x.RegisterServicesFromAssemblies(Assembly.Load("Parameters.Application.Request.Command"),
                    Assembly.Load("Parameters.Application.Request.Handler"),
                    Assembly.Load("Parameters.Application.Request.Query"),
                    Assembly.Load("Parameters.Applicaiton.Notification.Command"),
                    Assembly.Load("Parameters.Application.Notification.Handler"));
            }
        );

        service.AddTransient(typeof(IPipelineBehavior<,>), typeof(PipelineBehavior<,>));
        service.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        service.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        service.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));

        service.AddValidatorsFromAssembly(Assembly.Load("Parameters.Application.Request.Validation"));

        service.AddRepository();
        service.RegisterIntegrationEvents();

        service.RegisterRabbitMQ(service.BuildServiceProvider().GetService<IConfiguration>()!);

        service.AddScoped<SingletonTransaction>();
    }

    private static void RegisterIntegrationEvents(this IServiceCollection services)
    {
        services.AddDbContext<IntegrationEventContext>();
        services.AddTransient(typeof(IParameterIntegrationEventService), typeof(ParameterIntegrationEventService));
        services.AddTransient(typeof(IIntegrationEventService), typeof(IntegrationEventService));
    }
}