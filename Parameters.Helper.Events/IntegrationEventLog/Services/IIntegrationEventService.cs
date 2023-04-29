using Microsoft.EntityFrameworkCore.Storage;
using EntityIntegration = Parameters.Helper.Events.EventBus.Entity.IntegrationEvent;
using IntegrationEventEntity = Parameters.Helper.Events.IntegrationEventLog.Entity.IntegrationEventLogEntry;

namespace Parameters.Helper.Events.IntegrationEventLog.Services;

public interface IIntegrationEventService
{
    Task<IEnumerable<IntegrationEventEntity>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId);

    Task SaveEventAsync(EntityIntegration @event, Guid transactionId);

    Task MarkEventAsPublishedAsync(Guid eventId);

    Task MarkEventAsInProgressAsync(Guid eventId);

    Task MarkEventAsFailedAsync(Guid eventId);
}