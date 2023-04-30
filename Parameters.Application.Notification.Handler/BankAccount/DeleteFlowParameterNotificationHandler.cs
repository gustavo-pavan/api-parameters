using MediatR;
using Microsoft.Extensions.Logging;
using Parameters.Applicaiton.Notification.Command.BankAccount;
using Parameters.Application.Integration.Handler.BankAccount;
using Parameters.Helper.Events.IntegrationEventLog.Services;

namespace Parameters.Application.Notification.Handler.BankAccount;

public class DeleteBankAccountNotificationHandler : INotificationHandler<DeleteBankAccountNotificationCommand>
{
    private readonly IIntegrationEventService _service;
    private readonly ILogger<DeleteBankAccountNotificationHandler> _logger;
    private readonly ILogger<DeleteBankAccountIntegrationHandler> _loggerIntegration;

    public DeleteBankAccountNotificationHandler(IIntegrationEventService service, ILogger<DeleteBankAccountNotificationHandler> logger, ILogger<DeleteBankAccountIntegrationHandler> loggerIntegration)
    {
        _service = service;
        _logger = logger;
        _loggerIntegration = loggerIntegration;
    }

    public async Task Handle(DeleteBankAccountNotificationCommand notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start notification delete bank account in integration event");

        var @event = new DeleteBankAccountIntegrationHandler(_service, _loggerIntegration);

        await @event.Handler(new()
        {
            Id = notification.Id
        });

        _logger.LogInformation("Finish notification delete bank account in integration event");
    }
}
