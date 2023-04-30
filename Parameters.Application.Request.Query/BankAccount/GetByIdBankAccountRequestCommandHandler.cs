using Parameters.Application.Request.Command.BankAccount;
using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Application.Request.Query.BankAccount;

public class
    GetByIdBankAccountRequestCommandHandler : IRequestHandler<GetByIdBankAccountRequestCommand, BankAccountEntity?>
{
    private readonly IGetByIdBankAccountRepository _getByIdBankAccountRepository;
    private readonly ILogger<GetByIdBankAccountRequestCommandHandler> _logger;

    public GetByIdBankAccountRequestCommandHandler(IGetByIdBankAccountRepository getByIdBankAccountRepository,
        ILogger<GetByIdBankAccountRequestCommandHandler> logger)
    {
        _getByIdBankAccountRepository = getByIdBankAccountRepository;
        _logger = logger;
    }

    public async Task<BankAccountEntity?> Handle(GetByIdBankAccountRequestCommand bankAccountRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to get accounts");
            _logger.LogInformation("Execute transaction with database");
            var result = await _getByIdBankAccountRepository.Execute(bankAccountRequest.Id);

            _logger.LogInformation("Get accounts with success");
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error: {e.Message}");
            throw;
        }
    }
}