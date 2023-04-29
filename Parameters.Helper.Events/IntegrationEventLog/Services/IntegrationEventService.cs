using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Parameters.Helper.Events.IntegrationEventLog.Context;
using Parameters.Helper.Events.IntegrationEventLog.Enums;
using EntityIntegration = Parameters.Helper.Events.EventBus.Entity.IntegrationEvent;
using IntegrationEventEntity = Parameters.Helper.Events.IntegrationEventLog.Entity.IntegrationEventLogEntry;

namespace Parameters.Helper.Events.IntegrationEventLog.Services;

public class IntegrationEventService : IIntegrationEventService, IDisposable
{
    public IntegrationEventService(IntegrationEventContext context)
    {
        _context = context;

        _eventTypes = Assembly.Load("Parameters.Application.Request.Command")
            .GetTypes()
            .Where(x => x.Name.Contains("IntegrationEvent"))
            .ToList();
    }

    public async Task<IEnumerable<IntegrationEventEntity>> RetrieveEventLogsPendingToPublishAsync(
        Guid transactionId)
    {
        var tId = transactionId.ToString();
        var result = await _context.IntegrationEventLogs
            .Where(x => x.TransactionId == tId && x.State == EventState.NotPublished)
            .ToListAsync();

        if (result.Any())
            try
            {
                var results = result.OrderBy(o => o.CreationDate)
                    .Select(x => x.DeserializeJsonContent(_eventTypes.Find(p => p.Name == x.EventTypeShortName)!));

                return results;
            }
            catch (Exception e)
            {
                throw;
            }

        return new List<IntegrationEventEntity>();
    }

    public Task SaveEventAsync(EntityIntegration @event, IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));

        var eventLog = new IntegrationEventEntity(@event, transaction.TransactionId);
        _context.IntegrationEventLogs.Add(eventLog);
        return Task.CompletedTask;
    }

    public Task MarkEventAsPublishedAsync(Guid eventId)
    {
        return UpdateEventStatus(eventId, EventState.Published);
    }

    public Task MarkEventAsInProgressAsync(Guid eventId)
    {
        return UpdateEventStatus(eventId, EventState.InProgress);
    }

    public Task MarkEventAsFailedAsync(Guid eventId)
    {
        return UpdateEventStatus(eventId, EventState.PublishedFailed);
    }

    private Task UpdateEventStatus(Guid eventId, EventState status)
    {
        var eventLog = _context.IntegrationEventLogs.Single(x => x.EventId == eventId);
        eventLog.State = status;
        if (status == EventState.InProgress)
            eventLog.TimesSent++;

        _context.IntegrationEventLogs.Update(eventLog);
        return _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
            _context?.Dispose();
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private readonly IntegrationEventContext _context;

    private readonly List<Type> _eventTypes;

    private volatile bool _disposed;
}