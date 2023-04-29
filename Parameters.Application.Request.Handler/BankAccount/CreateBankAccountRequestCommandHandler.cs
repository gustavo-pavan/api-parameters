using Parameters.Application.Request.Command.BankAccount;
using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Application.Request.Handler.BankAccount;

public class
    CreateBankAccountRequestCommandHandler : IRequestHandler<CreateBankAccountRequestCommand, BankAccountEntity>
{
    private readonly IBaseAccountCreateRepository _baseAccountCreateRepository;
    private readonly ILogger<CreateBankAccountRequestCommandHandler> _logger;

    public CreateBankAccountRequestCommandHandler(IBaseAccountCreateRepository baseAccountCreateRepository,
        ILogger<CreateBankAccountRequestCommandHandler> logger)
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