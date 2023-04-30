using MediatR;
using Microsoft.Extensions.Logging;
using Parameters.Application.Integration.Handler.BankAccount;
using Parameters.Application.Notification.Command.BankAccount;
using Parameters.Helper.Events.IntegrationEventLog.Services;

namespace Parameters.Application.Notification.Handler.BankAccount;

public class CreateFlowParameterNotificationHandler : INotificationHandler<CreateFlowParameterNotificationCommand>
{
    private readonly IIntegrationEventService _service;
    private readonly ILogger<CreateFlowParameterNotificationHandler> _logger;
    private readonly ILogger<CreateFlowParameterIntegrationHandler> _loggerIntegration;

    public CreateFlowParameterNotificationHandler(IIntegrationEventService service, ILogger<CreateFlowParameterNotificationHandler> logger, ILogger<CreateFlowParameterIntegrationHandler> loggerIntegration)
    {
        _service = service;
        _logger = logger;
        _loggerIntegration = loggerIntegration;
    }

    public async Task Handle(CreateFlowParameterNotificationCommand notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start notification create bank account in integration event");

        var @event = new CreateFlowParameterIntegrationHandler(_service, _loggerIntegration);

        await @event.Handler(new()
        {
            Id = notification.Id,
            Name = notification.Name
        });

        _logger.LogInformation("Finish notification create bank account in integration event");
    }
}
