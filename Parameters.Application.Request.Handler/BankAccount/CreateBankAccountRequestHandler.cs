using Parameters.Application.Notification.Command.BankAccount;
using Parameters.Application.Request.Command.BankAccount;
using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Application.Request.Handler.BankAccount;

public class
    CreateBankAccountRequestHandler : IRequestHandler<CreateBankAccountRequestCommand, BankAccountEntity>
{
    private readonly IBaseAccountCreateRepository _baseAccountCreateRepository;
    private readonly ILogger<CreateBankAccountRequestHandler> _logger;

    public CreateBankAccountRequestHandler(IBaseAccountCreateRepository baseAccountCreateRepository,
        ILogger<CreateBankAccountRequestHandler> logger)
    {
        _baseAccountCreateRepository = baseAccountCreateRepository;
        _logger = logger;
    }

    public async Task<BankAccountEntity> Handle(CreateBankAccountRequestCommand bankAccountRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to create new account bank");
            var account = new BankAccountEntity(bankAccountRequest.Name, bankAccountRequest.Balance,
                bankAccountRequest.Description);

            _logger.LogInformation("Execute transaction with database");
            await _baseAccountCreateRepository.Execute(account);

            _logger.LogInformation("Send new notification to create account");
            account.AddDomainEvent(new CreateFlowParameterNotificationCommand() { Id = account.Id, Name = account.Name });

            _logger.LogInformation("Create account with success");
            return account;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error: {e.Message}");
            throw;
        }
    }
}