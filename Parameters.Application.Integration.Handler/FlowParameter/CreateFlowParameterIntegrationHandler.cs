using Parameters.Application.Integration.Command.FlowParameter;

namespace Parameters.Application.Integration.Handler.FlowParameter;

public class CreateFlowParameterIntegrationHandler : IIntegrationEventHandler<CreateFlowParameterIntegrationCommand>
{
    private readonly IIntegrationEventService _eventService;
    private readonly ILogger<CreateFlowParameterIntegrationHandler> _logger;

    public CreateFlowParameterIntegrationHandler(IIntegrationEventService eventService, ILogger<CreateFlowParameterIntegrationHandler> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    public async Task Handler(CreateFlowParameterIntegrationCommand @event)
    {
        try
        {
            _logger.LogInformation("Start integration event create flow parameter");

            await _eventService.SaveEventAsync(@event);

            _logger.LogInformation("Finish integration event create flow parameter");
        }
        catch (Exception e)
        {
            _logger.LogError("Exception: {message}", e.Message);
            throw;
        }
    }
}