using Parameters.Application.Integration.Command.PaymentType;

namespace Parameters.Application.Integration.Handler.PaymentType;

public class UpdatePaymentTypeIntegrationHandler : IIntegrationEventHandler<UpdatePaymentTypeIntegrationCommand>
{
    private readonly IIntegrationEventService _eventService;
    private readonly ILogger<UpdatePaymentTypeIntegrationHandler> _logger;

    public UpdatePaymentTypeIntegrationHandler(IIntegrationEventService eventService,
        ILogger<UpdatePaymentTypeIntegrationHandler> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    public async Task Handler(UpdatePaymentTypeIntegrationCommand @event)
    {
        try
        {
            _logger.LogInformation("Start integration event update payment type");

            await _eventService.SaveEventAsync(@event);

            _logger.LogInformation("Finish integration event update payment type");
        }
        catch (Exception e)
        {
            _logger.LogError("Exception: {message}", e.Message);
            throw;
        }
    }
}