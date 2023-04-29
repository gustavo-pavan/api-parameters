namespace Parameters.Application.Integration.Handler.BankAccount;

public class CreateBankAccountHandler : IIntegrationEventHandler<CreateBankAccountCommand>
{
    private readonly IIntegrationEventService _eventService;
    private readonly ILogger<CreateBankAccountHandler> _logger;

    public CreateBankAccountHandler(IIntegrationEventService eventService, ILogger<CreateBankAccountHandler> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    public async Task Handler(CreateBankAccountCommand @event)
    {
        try
        {
            _logger.LogInformation("Start integration event create account");

            await _eventService.SaveEventAsync(@event, SingletonTransaction.TransactionId);

            _logger.LogInformation("Finish integration event create account");
        }
        catch (Exception e)
        {
            _logger.LogError("Exception: {message}", e.Message);
            throw;
        }
    }
}