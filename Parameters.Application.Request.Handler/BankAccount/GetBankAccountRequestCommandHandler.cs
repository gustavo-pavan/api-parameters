using Parameters.Application.Request.Command.BankAccount;
using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Application.Request.Handler.BankAccount;

public class GetBankAccountRequestCommandHandler : IRequestHandler<GetBankAccountRequestCommand, IEnumerable<BankAccountEntity>>
{
    private readonly IBaseAccountGetRepository _baseAccountGetRepository;
    private readonly ILogger<GetBankAccountRequestCommandHandler> _logger;

    public GetBankAccountRequestCommandHandler(IBaseAccountGetRepository baseAccountGetRepository,
        ILogger<GetBankAccountRequestCommandHandler> logger)
    {
        _baseAccountGetRepository = baseAccountGetRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<BankAccountEntity>> Handle(GetBankAccountRequestCommand bankAccountRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to get accounts");
            _logger.LogInformation("Execute transaction with database");
            var result = await _baseAccountGetRepository.Execute();

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