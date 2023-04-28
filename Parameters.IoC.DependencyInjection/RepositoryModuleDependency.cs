using System.Reflection;
using Parameters.Domain.Repository.Generics;
using Parameters.Infra.Repository.Generics;

namespace Parameters.IoC.DependencyInjection;

public static class RepositoryModuleDependency
{
    public static void AddRepository(this IServiceCollection services)
    {
        var dictionary = new Dictionary<Type, Type>
        {
            { typeof(ICreateBaseRepository<>), typeof(CreateBaseRepository<>) },
            { typeof(IUpdateBaseRepository<>), typeof(UpdateBaseRepository<>) },
            { typeof(IDeleteBaseRepository<>), typeof(DeleteBaseRepository<>) },
            { typeof(IGetBaseRepository<>), typeof(GetBaseRepository<>) },
            { typeof(IGetByIdBaseRepository<>), typeof(GetByIdBaseRepository<>) }
        };

        foreach (var type in dictionary)
        {
            services.AddTransient(type.Key, type.Value);

            var abstractTypes = Assembly.GetAssembly(type.Key)?.GetTypes()
                ?.Where(x => x.IsInterface && x.GetInterface(type.Key.Name) != null).ToList();

            if (abstractTypes == null) return;
            {
                foreach (var abstractType in abstractTypes)
                {
                    var concrete = Assembly.GetAssembly(type.Value)?.GetTypes()
                        .Where(x => x.IsClass && x.GetInterface(abstractType.Name) != null)?.FirstOrDefault();

                    if (concrete is not null)
                        services.AddTransient(abstractType, concrete);
                }
            }
        }
    }
}