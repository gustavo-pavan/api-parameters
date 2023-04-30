using Microsoft.Extensions.Logging;
using Parameters.Helper.Events.EventBus.Interfaces;

namespace Parameters.Helper.Events.IntegrationEventLog.Services;

public class ParameterIntegrationEventService : IParameterIntegrationEventService
{
    #region Properties

    private readonly IEvent _event;
    private readonly IIntegrationEventService _integrationEventService;
    private readonly ILogger<ParameterIntegrationEventService> _logger;

    #endregion

    #region Constructor

    public ParameterIntegrationEventService(IEvent @event, IIntegrationEventService integrationEventService,
        ILogger<ParameterIntegrationEventService> logger)
    {
        _event = @event;
        _integrationEventService = integrationEventService;
        _logger = logger;
    }

    #endregion

    #region Method

    public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
    {
        var pendingEvents = await _integrationEventService.RetrieveEventLogsPendingToPublishAsync(transactionId);

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

    #endregion
}