namespace Parameters.Application.Integration.Handler.BankAccount;

public class UpdateBankAccountIntegrationHandler : IIntegrationEventHandler<UpdateBankAccountIntegrationCommand>
{
    private readonly IIntegrationEventService _eventService;
    private readonly ILogger<UpdateBankAccountIntegrationHandler> _logger;

    public UpdateBankAccountIntegrationHandler(IIntegrationEventService eventService,
        ILogger<UpdateBankAccountIntegrationHandler> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    public async Task Handler(UpdateBankAccountIntegrationCommand @event)
    {
        try
        {
            _logger.LogInformation("Start integration event update bank account");

            await _eventService.SaveEventAsync(@event);

            _logger.LogInformation("Finish integration event update bank account");
        }
        catch (Exception e)
        {
            _logger.LogError("Exception: {message}", e.Message);
            throw;
        }
    }
}