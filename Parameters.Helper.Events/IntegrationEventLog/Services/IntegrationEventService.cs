using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Parameters.Helper.Events.EventBus;
using Parameters.Helper.Events.IntegrationEventLog.Context;
using Parameters.Helper.Events.IntegrationEventLog.Enums;
using EntityIntegration = Parameters.Helper.Events.EventBus.Entity.IntegrationEvent;
using IntegrationEventEntity = Parameters.Helper.Events.IntegrationEventLog.Entity.IntegrationEventLogEntry;

namespace Parameters.Helper.Events.IntegrationEventLog.Services;

public class IntegrationEventService : IIntegrationEventService, IDisposable
{
    private readonly IntegrationEventContext _context;

    private readonly List<Type> _eventTypes;
    private readonly SingletonTransaction _singletonTransaction;

    private volatile bool _disposed;

    public IntegrationEventService(IntegrationEventContext context, SingletonTransaction singletonTransaction)
    {
        _context = context;

        _eventTypes = Assembly.Load("Parameters.Application.Integration.Command")
            .GetTypes()
            .Where(x => x.Name.Contains("Integration"))
            .ToList();

        _singletonTransaction = singletonTransaction;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<IEnumerable<IntegrationEventEntity>> RetrieveEventLogsPendingToPublishAsync(
        Guid transactionId)
    {
        var tId = transactionId.ToString();
        var result = await _context.IntegrationEventLogs
            .Where(x => x.TransactionId == tId && x.State == EventState.NotPublished)
            .ToListAsync();

        if (result.Any())
        {
            var results = result.OrderBy(o => o.CreationDate)
                .Select(x => x.DeserializeJsonContent(_eventTypes.Find(p => p.Name == x.EventTypeShortName)!));

            return results;
        }

        return new List<IntegrationEventEntity>();
    }

    public Task SaveEventAsync(EntityIntegration @event)
    {
        if (Guid.Empty.Equals(_singletonTransaction.TransactionId))
            throw new ArgumentNullException(nameof(_singletonTransaction.TransactionId));

        var eventLog = new IntegrationEventEntity(@event, _singletonTransaction.TransactionId);
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
}