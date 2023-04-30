using Microsoft.Extensions.Logging;
using Parameters.Helper.Events.EventBus;
using Parameters.Helper.Events.EventBus.Interfaces;

namespace Parameters.Helper.Events.IntegrationEventLog.Services;

public class ParameterIntegrationEventService : IParameterIntegrationEventService
{
    private readonly IEvent _event;
    private readonly IIntegrationEventService _integrationEventService;
    private readonly ILogger<ParameterIntegrationEventService> _logger;
    private readonly SingletonTransaction _singletonTransaction;

    public ParameterIntegrationEventService(IEvent @event, IIntegrationEventService integrationEventService,
        ILogger<ParameterIntegrationEventService> logger, SingletonTransaction singletonTransaction)
    {
        _event = @event;
        _integrationEventService = integrationEventService;
        _logger = logger;
        _singletonTransaction = singletonTransaction;
    }

    public async Task PublishEventsThroughEventBusAsync()
    {
        var pendingEvents = await _integrationEventService.RetrieveEventLogsPendingToPublishAsync(_singletonTransaction.TransactionId);

        try
        {
            if (pendingEvents.Any())
                foreach (var pendingEvent in pendingEvents)
                {
                    _logger.LogInformation("Publishing integration event: {IntegrationEventId}", pendingEvent.EventId);

                    try
                    {
                        _logger.LogInformation("Mark event in progress event - ({event})", pendingEvent.EventId);
                        await _integrationEventService.MarkEventAsInProgressAsync(pendingEvent.EventId);

                        _event.Publish(pendingEvent.IntegrationEvent);

                        _logger.LogInformation("Mark event in published event - ({event})", pendingEvent.EventId);
                        await _integrationEventService.MarkEventAsPublishedAsync(pendingEvent.EventId);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex,
                            "Exception publishing integration event: {IntegrationEventId} --- Exception: {ex}",
                            pendingEvent.EventId, ex.Message);
                        await _integrationEventService.MarkEventAsFailedAsync(pendingEvent.EventId);
                        throw new Exception(ex.Message);
                    }
                }
        }
        catch (Exception e)
        {

            throw;
        }
    }
}