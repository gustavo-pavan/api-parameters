using MediatR;
using Microsoft.Extensions.Logging;
using Parameters.Applicaiton.Notification.Command.FlowParameter;
using Parameters.Application.Integration.Command.FlowParameter;
using Parameters.Application.Integration.Handler.FlowParameter;
using Parameters.Helper.Events.IntegrationEventLog.Services;

namespace Parameters.Application.Notification.Handler.FlowParameter;

public class UpdateFlowParameterNotificationHandler : INotificationHandler<UpdateFlowParameterNotificationCommand>
{
    private readonly ILogger<UpdateFlowParameterNotificationHandler> _logger;
    private readonly ILogger<UpdateFlowParameterIntegrationHandler> _loggerIntegration;
    private readonly IIntegrationEventService _service;

    public UpdateFlowParameterNotificationHandler(IIntegrationEventService service,
        ILogger<UpdateFlowParameterNotificationHandler> logger,
        ILogger<UpdateFlowParameterIntegrationHandler> loggerIntegration)
    {
        _service = service;
        _logger = logger;
        _loggerIntegration = loggerIntegration;
    }

    public async Task Handle(UpdateFlowParameterNotificationCommand notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start notification update flow parameter in integration event");

        var @event = new UpdateFlowParameterIntegrationHandler(_service, _loggerIntegration);

        await @event.Handler(new UpdateFlowParameterIntegrationCommand
        {
            Id = notification.Id,
            Name = notification.Name
        });

        _logger.LogInformation("Finish notification update flow parameter in integration event");
    }
}