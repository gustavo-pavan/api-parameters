using MediatR;
using Microsoft.Extensions.Logging;
using Parameters.Application.Integration.Command.BankAccount;
using Parameters.Application.Integration.Handler.BankAccount;
using Parameters.Application.Notification.Command.BankAccount;
using Parameters.Helper.Events.IntegrationEventLog.Services;

namespace Parameters.Application.Notification.Handler.BankAccount;

public class UpdateBankAccountNotificationHandler : INotificationHandler<UpdateBankAccountNotificationCommand>
{
    private readonly ILogger<UpdateBankAccountNotificationHandler> _logger;
    private readonly ILogger<UpdateBankAccountIntegrationHandler> _loggerIntegration;
    private readonly IIntegrationEventService _service;

    public UpdateBankAccountNotificationHandler(IIntegrationEventService service,
        ILogger<UpdateBankAccountNotificationHandler> logger,
        ILogger<UpdateBankAccountIntegrationHandler> loggerIntegration)
    {
        _service = service;
        _logger = logger;
        _loggerIntegration = loggerIntegration;
    }

    public async Task Handle(UpdateBankAccountNotificationCommand notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start notification update bank account in integration event");

        var @event = new UpdateBankAccountIntegrationHandler(_service, _loggerIntegration);

        await @event.Handler(new UpdateBankAccountIntegrationCommand
        {
            Id = notification.Id,
            Name = notification.Name,
            Balance = notification.Balance,
        });

        _logger.LogInformation("Finish notification update bank account in integration event");
    }
}