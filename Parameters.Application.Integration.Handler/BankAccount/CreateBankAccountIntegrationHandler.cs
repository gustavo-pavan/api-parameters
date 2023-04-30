namespace Parameters.Application.Integration.Handler.BankAccount;

public class CreateBankAccountIntegrationHandler : IIntegrationEventHandler<CreateBankAccountIntegrationCommand>
{
    private readonly IIntegrationEventService _eventService;
    private readonly ILogger<CreateBankAccountIntegrationHandler> _logger;

    public CreateBankAccountIntegrationHandler(IIntegrationEventService eventService, ILogger<CreateBankAccountIntegrationHandler> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    public async Task Handler(CreateBankAccountIntegrationCommand @event)
    {
        try
        {
            _logger.LogInformation("Start integration event create bank account");

            await _eventService.SaveEventAsync(@event);

            _logger.LogInformation("Finish integration event create bank account");
        }
        catch (Exception e)
        {
            _logger.LogError("Exception: {message}", e.Message);
            throw;
        }
    }
}