using Parameters.Application.Integration.Command.PaymentType;

namespace Parameters.Application.Integration.Handler.PaymentType;

public class CreatePaymentTypeIntegrationHandler : IIntegrationEventHandler<CreatePaymentTypeIntegrationCommand>
{
    private readonly IIntegrationEventService _eventService;
    private readonly ILogger<CreatePaymentTypeIntegrationHandler> _logger;

    public CreatePaymentTypeIntegrationHandler(IIntegrationEventService eventService,
        ILogger<CreatePaymentTypeIntegrationHandler> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    public async Task Handler(CreatePaymentTypeIntegrationCommand @event)
    {
        try
        {
            _logger.LogInformation("Start integration event create payment type");

            await _eventService.SaveEventAsync(@event);

            _logger.LogInformation("Finish integration event create payment type");
        }
        catch (Exception e)
        {
            _logger.LogError("Exception: {message}", e.Message);
            throw;
        }
    }
}