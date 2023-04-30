using Parameters.Application.Integration.Command.FlowParameter;

namespace Parameters.Application.Integration.Handler.FlowParameter;

public class DeleteFlowParameterIntegrationHandler : IIntegrationEventHandler<DeleteFlowParameterIntegrationCommand>
{
    private readonly IIntegrationEventService _eventService;
    private readonly ILogger<DeleteFlowParameterIntegrationHandler> _logger;

    public DeleteFlowParameterIntegrationHandler(IIntegrationEventService eventService, ILogger<DeleteFlowParameterIntegrationHandler> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    public async Task Handler(DeleteFlowParameterIntegrationCommand @event)
    {
        try
        {
            _logger.LogInformation("Start integration event delete flow parameter");

            await _eventService.SaveEventAsync(@event);

            _logger.LogInformation("Finish integration event delete flow parameter");
        }
        catch (Exception e)
        {
            _logger.LogError("Exception: {message}", e.Message);
            throw;
        }
    }
}