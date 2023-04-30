using MediatR;
using Microsoft.Extensions.Logging;
using Parameters.Applicaiton.Notification.Command.BankAccount;
using Parameters.Application.Integration.Handler.BankAccount;
using Parameters.Helper.Events.IntegrationEventLog.Services;

namespace Parameters.Application.Notification.Handler.BankAccount;

public class UpdateBankAccountNotificationHandler : INotificationHandler<UpdateBankAccountNotificationCommand>
{
    private readonly IIntegrationEventService _service;
    private readonly ILogger<UpdateBankAccountNotificationHandler> _logger;
    private readonly ILogger<UpdateBankAccountIntegrationHandler> _loggerIntegration;

    public UpdateBankAccountNotificationHandler(IIntegrationEventService service, ILogger<UpdateBankAccountNotificationHandler> logger, ILogger<UpdateBankAccountIntegrationHandler> loggerIntegration)
    {
        _service = service;
        _logger = logger;
        _loggerIntegration = loggerIntegration;
    }

    public async Task Handle(UpdateBankAccountNotificationCommand notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start notification update bank account in integration event");

        var @event = new UpdateBankAccountIntegrationHandler(_service, _loggerIntegration);

        await @event.Handler(new()
        {
            Id = notification.Id,
            Name = notification.Name
        });

        _logger.LogInformation("Finish notification update bank account in integration event");
    }
}
