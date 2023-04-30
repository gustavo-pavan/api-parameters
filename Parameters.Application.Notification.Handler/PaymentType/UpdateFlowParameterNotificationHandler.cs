using MediatR;
using Microsoft.Extensions.Logging;
using Parameters.Applicaiton.Notification.Command.PaymentType;
using Parameters.Application.Integration.Command.PaymentType;
using Parameters.Application.Integration.Handler.PaymentType;
using Parameters.Helper.Events.IntegrationEventLog.Services;

namespace Parameters.Application.Notification.Handler.PaymentType;

public class UpdatePaymentTypeNotificationHandler : INotificationHandler<UpdatePaymentTypeNotificationCommand>
{
    private readonly ILogger<UpdatePaymentTypeNotificationHandler> _logger;
    private readonly ILogger<UpdatePaymentTypeIntegrationHandler> _loggerIntegration;
    private readonly IIntegrationEventService _service;

    public UpdatePaymentTypeNotificationHandler(IIntegrationEventService service,
        ILogger<UpdatePaymentTypeNotificationHandler> logger,
        ILogger<UpdatePaymentTypeIntegrationHandler> loggerIntegration)
    {
        _service = service;
        _logger = logger;
        _loggerIntegration = loggerIntegration;
    }

    public async Task Handle(UpdatePaymentTypeNotificationCommand notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start notification update payment type in integration event");

        var @event = new UpdatePaymentTypeIntegrationHandler(_service, _loggerIntegration);

        await @event.Handler(new UpdatePaymentTypeIntegrationCommand
        {
            Id = notification.Id,
            Name = notification.Name
        });

        _logger.LogInformation("Finish notification update payment type in integration event");
    }
}