using MediatR;
using Microsoft.Extensions.Logging;
using Parameters.Applicaiton.Notification.Command.PaymentType;
using Parameters.Application.Integration.Command.PaymentType;
using Parameters.Application.Integration.Handler.PaymentType;
using Parameters.Helper.Events.IntegrationEventLog.Services;

namespace Parameters.Application.Notification.Handler.PaymentType;

public class CreatePaymentTypeNotificationHandler : INotificationHandler<CreatePaymentTypeNotificationCommand>
{
    private readonly ILogger<CreatePaymentTypeNotificationHandler> _logger;
    private readonly ILogger<CreatePaymentTypeIntegrationHandler> _loggerIntegration;
    private readonly IIntegrationEventService _service;

    public CreatePaymentTypeNotificationHandler(IIntegrationEventService service,
        ILogger<CreatePaymentTypeNotificationHandler> logger,
        ILogger<CreatePaymentTypeIntegrationHandler> loggerIntegration)
    {
        _service = service;
        _logger = logger;
        _loggerIntegration = loggerIntegration;
    }

    public async Task Handle(CreatePaymentTypeNotificationCommand notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start notification create payment type in integration event");

        var @event = new CreatePaymentTypeIntegrationHandler(_service, _loggerIntegration);

        await @event.Handler(new CreatePaymentTypeIntegrationCommand
        {
            Id = notification.Id,
            Name = notification.Name
        });

        _logger.LogInformation("Finish notification create payment type in integration event");
    }
}