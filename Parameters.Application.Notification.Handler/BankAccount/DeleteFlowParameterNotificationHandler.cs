﻿using MediatR;
using Microsoft.Extensions.Logging;
using Parameters.Application.Integration.Command.BankAccount;
using Parameters.Application.Integration.Handler.BankAccount;
using Parameters.Application.Notification.Command.BankAccount;
using Parameters.Helper.Events.IntegrationEventLog.Services;

namespace Parameters.Application.Notification.Handler.BankAccount;

public class DeleteBankAccountNotificationHandler : INotificationHandler<DeleteBankAccountNotificationCommand>
{
    private readonly ILogger<DeleteBankAccountNotificationHandler> _logger;
    private readonly ILogger<DeleteBankAccountIntegrationHandler> _loggerIntegration;
    private readonly IIntegrationEventService _service;

    public DeleteBankAccountNotificationHandler(IIntegrationEventService service,
        ILogger<DeleteBankAccountNotificationHandler> logger,
        ILogger<DeleteBankAccountIntegrationHandler> loggerIntegration)
    {
        _service = service;
        _logger = logger;
        _loggerIntegration = loggerIntegration;
    }

    public async Task Handle(DeleteBankAccountNotificationCommand notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start notification delete bank account in integration event");

        var @event = new DeleteBankAccountIntegrationHandler(_service, _loggerIntegration);

        await @event.Handler(new DeleteBankAccountIntegrationCommand
        {
            Id = notification.Id
        });

        _logger.LogInformation("Finish notification delete bank account in integration event");
    }
}