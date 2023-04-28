using Parameters.Application.Request.Command.BankAccount;
using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Application.Request.Handler.BankAccount;

public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, BankAccountEntity>
{
    private readonly ICreateRepository _createRepository;
    private readonly ILogger<CreateRequestCommandHandler> _logger;

    public CreateRequestCommandHandler(ICreateRepository createRepository,
        ILogger<CreateRequestCommandHandler> logger)
    {
        _createRepository = createRepository;
        _logger = logger;
    }

    public async Task<BankAccountEntity> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to create new account bank");
            var account = new BankAccountEntity(request.Name, request.Balance, request.Description);

            _logger.LogInformation("Execute transaction with database");
            await _createRepository.Execute(account);

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