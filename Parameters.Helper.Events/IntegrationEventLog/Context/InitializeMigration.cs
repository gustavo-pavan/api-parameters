using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Parameters.Helper.Events.IntegrationEventLog.Context;

public static class InitializeMigration
{
    public static void InitializeDatabase(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider().CreateScope();
        var context = provider.ServiceProvider.GetService<IntegrationEventContext>();

        if (context != null && !context.Database.CanConnect())
        {
            context.Database.EnsureCreated();
        }
        else
        {
            if (context is not null && context.Database.CanConnect())
            {
                var result = context.Database
                    .SqlQueryRaw<int>(@$"
                        SELECT COUNT(T.schema_id) as [VALUE] FROM sys.tables AS T
                         INNER JOIN sys.schemas AS S ON T.schema_id = S.schema_id
                         WHERE T.Name = 'IntegrationEventLog'").FirstOrDefault();

                if (result == default(int))
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}