using Parameters.Application.Request.Command.BankAccount;
using Parameters.Domain.Repository.BankAccount;

namespace BankAccount.Application.Request.Handler.Account;

public class GetByIdRequestCommandHandler : IRequestHandler<GetByIdRequestCommand, BankAccountEntity?>
{
    private readonly IGetByIdRepository _getByIdRepository;
    private readonly ILogger<GetByIdRequestCommandHandler> _logger;

    public GetByIdRequestCommandHandler(IGetByIdRepository getByIdRepository,
        ILogger<GetByIdRequestCommandHandler> logger)
    {
        _getByIdRepository = getByIdRepository;
        _logger = logger;
    }

    public async Task<BankAccountEntity?> Handle(GetByIdRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to get accounts");
            _logger.LogInformation("Execute transaction with database");
            var result = await _getByIdRepository.Execute(request.Id);

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