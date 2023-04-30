using MediatR;
using Microsoft.Extensions.Logging;
using Parameters.Application.Integration.Command.PaymentType;
using Parameters.Application.Integration.Handler.PaymentType;
using Parameters.Application.Notification.Command.PaymentType;
using Parameters.Helper.Events.IntegrationEventLog.Services;

namespace Parameters.Application.Notification.Handler.PaymentType;

public class DeletePaymentTypeNotificationHandler : INotificationHandler<DeletePaymentTypeNotificationCommand>
{
    private readonly ILogger<DeletePaymentTypeNotificationHandler> _logger;
    private readonly ILogger<DeletePaymentTypeIntegrationHandler> _loggerIntegration;
    private readonly IIntegrationEventService _service;

    public DeletePaymentTypeNotificationHandler(IIntegrationEventService service,
        ILogger<DeletePaymentTypeNotificationHandler> logger,
        ILogger<DeletePaymentTypeIntegrationHandler> loggerIntegration)
    {
        _service = service;
        _logger = logger;
        _loggerIntegration = loggerIntegration;
    }

    public async Task Handle(DeletePaymentTypeNotificationCommand notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start notification delete payment type in integration event");

        var @event = new DeletePaymentTypeIntegrationHandler(_service, _loggerIntegration);

        await @event.Handler(new DeletePaymentTypeIntegrationCommand
        {
            Id = notification.Id
        });

        _logger.LogInformation("Finish notification delete payment type in integration event");
    }
}