using MediatR;
using Microsoft.Extensions.Logging;
using Parameters.Applicaiton.Notification.Command.PaymentType;
using Parameters.Application.Integration.Handler.PaymentType;
using Parameters.Helper.Events.IntegrationEventLog.Services;

namespace Parameters.Application.Notification.Handler.PaymentType;

public class DeletePaymentTypeNotificationHandler : INotificationHandler<DeletePaymentTypeNotificationCommand>
{
    private readonly IIntegrationEventService _service;
    private readonly ILogger<DeletePaymentTypeNotificationHandler> _logger;
    private readonly ILogger<DeletePaymentTypeIntegrationHandler> _loggerIntegration;

    public DeletePaymentTypeNotificationHandler(IIntegrationEventService service, ILogger<DeletePaymentTypeNotificationHandler> logger, ILogger<DeletePaymentTypeIntegrationHandler> loggerIntegration)
    {
        _service = service;
        _logger = logger;
        _loggerIntegration = loggerIntegration;
    }

    public async Task Handle(DeletePaymentTypeNotificationCommand notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start notification delete payment type in integration event");

        var @event = new DeletePaymentTypeIntegrationHandler(_service, _loggerIntegration);

        await @event.Handler(new()
        {
            Id = notification.Id
        });

        _logger.LogInformation("Finish notification delete payment type in integration event");
    }
}
