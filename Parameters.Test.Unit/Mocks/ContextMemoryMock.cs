using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Parameters.Helper.Events.IntegrationEventLog.Context;

namespace Parameters.Test.Unit.Mocks;

public class ContextMemoryMock
{
    public static IntegrationEventContext Mock()
    {
        var loggerMock = new Mock<ILogger<IntegrationEventContext>>();
        var configurationMock = new Mock<IConfiguration>();
        var guid = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<IntegrationEventContext>().UseInMemoryDatabase(guid)
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;
        var context = new IntegrationEventContext(options, configurationMock.Object, loggerMock.Object);
        return context;
    }
}