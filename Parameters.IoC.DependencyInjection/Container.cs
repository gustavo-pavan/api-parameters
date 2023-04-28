using System.Reflection;
using FluentValidation;
using MediatR;
using Parameters.Helper.Behavior;
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
                    Assembly.Load("Parameters.Application.Request.Handler"));
            }
        );

        service.AddTransient(typeof(IPipelineBehavior<,>), typeof(PipelineBehavior<,>));
        service.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        service.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        service.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));

        service.AddValidatorsFromAssembly(Assembly.Load("Parameters.Application.Request.Validation"));

        service.AddRepository();
    }
}