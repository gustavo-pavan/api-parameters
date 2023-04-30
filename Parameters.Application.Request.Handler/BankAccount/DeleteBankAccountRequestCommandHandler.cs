using Parameters.Application.Notification.Command.BankAccount;
using Parameters.Application.Request.Command.BankAccount;
using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Application.Request.Handler.BankAccount;

public class DeleteBankAccountRequestCommandHandler : IRequestHandler<DeleteBankAccountRequestCommand, bool>
{
    private readonly IDeleteBankAccountRepository _deleteBankAccountRepository;
    private readonly ILogger<DeleteBankAccountRequestCommandHandler> _logger;
    private readonly IGetByIdBankAccountRepository _getByIdBankAccountRepository;

    public DeleteBankAccountRequestCommandHandler(IDeleteBankAccountRepository deleteBankAccountRepository,
        ILogger<DeleteBankAccountRequestCommandHandler> logger,
        IGetByIdBankAccountRepository getByIdBankAccountRepository)
    {
        _deleteBankAccountRepository = deleteBankAccountRepository;
        _logger = logger;
        _getByIdBankAccountRepository = getByIdBankAccountRepository;
    }

    public async Task<bool> Handle(DeleteBankAccountRequestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to delete account bank");

            _logger.LogInformation("Get user in database");
            var entity = await _getByIdBankAccountRepository.Execute(request.Id);

            _logger.LogInformation("Execute transaction with database");
            await _deleteBankAccountRepository.Execute(request.Id);

            _logger.LogInformation("Send new notification to delete account");
            entity?.AddDomainEvent(new DeleteBankAccountNotificationCommand { Id = request.Id });

            _logger.LogInformation("Delete account with success");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error: {e.Message}");
            throw;
        }
    }
}