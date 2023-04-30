using Parameters.Application.Integration.Command.FlowParameter;

namespace Parameters.Application.Integration.Handler.FlowParameter;

public class UpdateFlowParameterIntegrationHandler : IIntegrationEventHandler<UpdateFlowParameterIntegrationCommand>
{
    private readonly IIntegrationEventService _eventService;
    private readonly ILogger<UpdateFlowParameterIntegrationHandler> _logger;

    public UpdateFlowParameterIntegrationHandler(IIntegrationEventService eventService, ILogger<UpdateFlowParameterIntegrationHandler> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    public async Task Handler(UpdateFlowParameterIntegrationCommand @event)
    {
        try
        {
            _logger.LogInformation("Start integration event update flow parameter");

            await _eventService.SaveEventAsync(@event);

            _logger.LogInformation("Finish integration event update flow parameter");
        }
        catch (Exception e)
        {
            _logger.LogError("Exception: {message}", e.Message);
            throw;
        }
    }
}