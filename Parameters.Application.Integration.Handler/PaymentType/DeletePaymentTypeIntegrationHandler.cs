using Parameters.Application.Integration.Command.PaymentType;

namespace Parameters.Application.Integration.Handler.PaymentType;

public class DeletePaymentTypeIntegrationHandler : IIntegrationEventHandler<DeletePaymentTypeIntegrationCommand>
{
    private readonly IIntegrationEventService _eventService;
    private readonly ILogger<DeletePaymentTypeIntegrationHandler> _logger;

    public DeletePaymentTypeIntegrationHandler(IIntegrationEventService eventService,
        ILogger<DeletePaymentTypeIntegrationHandler> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    public async Task Handler(DeletePaymentTypeIntegrationCommand @event)
    {
        try
        {
            _logger.LogInformation("Start integration event delete payment type");

            await _eventService.SaveEventAsync(@event);

            _logger.LogInformation("Finish integration event delete payment type");
        }
        catch (Exception e)
        {
            _logger.LogError("Exception: {message}", e.Message);
            throw;
        }
    }
}