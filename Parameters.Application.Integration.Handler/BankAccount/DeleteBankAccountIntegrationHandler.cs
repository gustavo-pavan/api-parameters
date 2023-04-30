namespace Parameters.Application.Integration.Handler.BankAccount;

public class DeleteBankAccountIntegrationHandler : IIntegrationEventHandler<DeleteBankAccountIntegrationCommand>
{
    private readonly IIntegrationEventService _eventService;
    private readonly ILogger<DeleteBankAccountIntegrationHandler> _logger;

    public DeleteBankAccountIntegrationHandler(IIntegrationEventService eventService, ILogger<DeleteBankAccountIntegrationHandler> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    public async Task Handler(DeleteBankAccountIntegrationCommand @event)
    {
        try
        {
            _logger.LogInformation("Start integration event delete bank account");

            await _eventService.SaveEventAsync(@event);

            _logger.LogInformation("Finish integration event delete bank account");
        }
        catch (Exception e)
        {
            _logger.LogError("Exception: {message}", e.Message);
            throw;
        }
    }
}