using Parameters.Application.Request.Command.BankAccount;
using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Application.Request.Handler.BankAccount;

public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, BankAccountEntity>
{
    private readonly IBaseAccountCreateRepository _baseAccountCreateRepository;
    private readonly ILogger<CreateRequestCommandHandler> _logger;

    public CreateRequestCommandHandler(IBaseAccountCreateRepository baseAccountCreateRepository,
        ILogger<CreateRequestCommandHandler> logger)
    {
        _baseAccountCreateRepository = baseAccountCreateRepository;
        _logger = logger;
    }

    public async Task<BankAccountEntity> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to create new account bank");
            var account = new BankAccountEntity(request.Name, request.Balance, request.Description);

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