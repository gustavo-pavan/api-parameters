using Parameters.Application.Notification.Command.BankAccount;
using Parameters.Application.Request.Command.BankAccount;
using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Application.Request.Handler.BankAccount;

public class CreateBankAccountRequestHandler : IRequestHandler<CreateBankAccountRequestCommand, BankAccountEntity>
{
    private readonly ICreateBankAccountRepository _createBankAccountRepository;
    private readonly ILogger<CreateBankAccountRequestHandler> _logger;

    public CreateBankAccountRequestHandler(ICreateBankAccountRepository createBankAccountRepository,
        ILogger<CreateBankAccountRequestHandler> logger)
    {
        _createBankAccountRepository = createBankAccountRepository;
        _logger = logger;
    }

    public async Task<BankAccountEntity> Handle(CreateBankAccountRequestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to create new account bank");
            var account = new BankAccountEntity(request.Name, request.Balance,
                request.Description);

            _logger.LogInformation("Execute transaction with database");
            await _createBankAccountRepository.Execute(account);

            _logger.LogInformation("Send new notification to create account");
            account.AddDomainEvent(new CreateBankAccountNotificationCommand { Id = account.Id, Name = account.Name });

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