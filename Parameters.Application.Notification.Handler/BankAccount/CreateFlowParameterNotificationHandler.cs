using MediatR;
using Microsoft.Extensions.Logging;
using Parameters.Applicaiton.Notification.Command.BankAccount;
using Parameters.Application.Integration.Handler.BankAccount;
using Parameters.Helper.Events.IntegrationEventLog.Services;

namespace Parameters.Application.Notification.Handler.BankAccount;

public class CreateBankAccountNotificationHandler : INotificationHandler<CreateBankAccountNotificationCommand>
{
    private readonly IIntegrationEventService _service;
    private readonly ILogger<CreateBankAccountNotificationHandler> _logger;
    private readonly ILogger<CreateBankAccountIntegrationHandler> _loggerIntegration;

    public CreateBankAccountNotificationHandler(IIntegrationEventService service, ILogger<CreateBankAccountNotificationHandler> logger, ILogger<CreateBankAccountIntegrationHandler> loggerIntegration)
    {
        _service = service;
        _logger = logger;
        _loggerIntegration = loggerIntegration;
    }

    public async Task Handle(CreateBankAccountNotificationCommand notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start notification create bank account in integration event");

        var @event = new CreateBankAccountIntegrationHandler(_service, _loggerIntegration);

        await @event.Handler(new()
        {
            Id = notification.Id,
            Name = notification.Name
        });

        _logger.LogInformation("Finish notification create bank account in integration event");
    }
}
