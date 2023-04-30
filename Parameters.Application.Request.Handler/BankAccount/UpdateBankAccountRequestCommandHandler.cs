using Parameters.Application.Request.Command.BankAccount;
using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Application.Request.Handler.BankAccount;

public class
    UpdateBankAccountRequestCommandHandler : IRequestHandler<UpdateBankAccountRequestCommand, BankAccountEntity>
{
    private readonly IBaseAccountUpdateRepository _baseAccountUpdateRepository;
    private readonly ILogger<CreateBankAccountRequestHandler> _logger;

    public UpdateBankAccountRequestCommandHandler(IBaseAccountUpdateRepository baseAccountUpdateRepository,
        ILogger<CreateBankAccountRequestHandler> logger)
    {
        _baseAccountUpdateRepository = baseAccountUpdateRepository;
        _logger = logger;
    }

    public async Task<BankAccountEntity> Handle(UpdateBankAccountRequestCommand bankAccountRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to update account bank");
            var account = new BankAccountEntity(bankAccountRequest.Id, bankAccountRequest.Name,
                bankAccountRequest.Balance, bankAccountRequest.Description);

            _logger.LogInformation("Execute transaction with database");
            await _baseAccountUpdateRepository.Execute(account);

            _logger.LogInformation("Update account with success");
            return account;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error Update: {e.Message}");
            throw;
        }
    }
}