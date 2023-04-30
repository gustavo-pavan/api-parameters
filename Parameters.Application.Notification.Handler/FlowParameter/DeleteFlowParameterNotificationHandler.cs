using MediatR;
using Microsoft.Extensions.Logging;
using Parameters.Applicaiton.Notification.Command.FlowParameter;
using Parameters.Application.Integration.Command.FlowParameter;
using Parameters.Application.Integration.Handler.FlowParameter;
using Parameters.Helper.Events.IntegrationEventLog.Services;

namespace Parameters.Application.Notification.Handler.FlowParameter;

public class DeleteFlowParameterNotificationHandler : INotificationHandler<DeleteFlowParameterNotificationCommand>
{
    private readonly ILogger<DeleteFlowParameterNotificationHandler> _logger;
    private readonly ILogger<DeleteFlowParameterIntegrationHandler> _loggerIntegration;
    private readonly IIntegrationEventService _service;

    public DeleteFlowParameterNotificationHandler(IIntegrationEventService service,
        ILogger<DeleteFlowParameterNotificationHandler> logger,
        ILogger<DeleteFlowParameterIntegrationHandler> loggerIntegration)
    {
        _service = service;
        _logger = logger;
        _loggerIntegration = loggerIntegration;
    }

    public async Task Handle(DeleteFlowParameterNotificationCommand notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start notification delete flow parameter in integration event");

        var @event = new DeleteFlowParameterIntegrationHandler(_service, _loggerIntegration);

        await @event.Handler(new DeleteFlowParameterIntegrationCommand
        {
            Id = notification.Id
        });

        _logger.LogInformation("Finish notification delete flow parameter in integration event");
    }
}