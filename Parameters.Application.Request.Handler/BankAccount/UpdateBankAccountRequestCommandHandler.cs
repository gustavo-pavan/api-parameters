using Parameters.Application.Notification.Command.BankAccount;
using Parameters.Application.Request.Command.BankAccount;
using Parameters.Application.Request.Dto;
using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Application.Request.Handler.BankAccount;

public class
    UpdateBankAccountRequestCommandHandler : IRequestHandler<UpdateBankAccountRequestCommand, BankAccountDto>
{
    private readonly IUpdateBankAccountRepository _updateBankAccountRepository;
    private readonly ILogger<UpdateBankAccountRequestCommandHandler> _logger;

    public UpdateBankAccountRequestCommandHandler(IUpdateBankAccountRepository updateBankAccountRepository,
        ILogger<UpdateBankAccountRequestCommandHandler> logger)
    {
        _updateBankAccountRepository = updateBankAccountRepository;
        _logger = logger;
    }

    public async Task<BankAccountDto> Handle(UpdateBankAccountRequestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to update account bank");
            var account = new BankAccountEntity(request.Id, request.Name,
                request.Balance, request.Description);

            _logger.LogInformation("Execute transaction with database");
            await _updateBankAccountRepository.Execute(account);

            _logger.LogInformation("Send new notification to update account");
            account.AddDomainEvent(new UpdateBankAccountNotificationCommand { Id = account.Id, Name = account.Name });

            _logger.LogInformation("Update account with success");
            return new(account);
        }
        catch (Exception e)
        {
            _logger.LogError($"Error Update: {e.Message}");
            throw;
        }
    }
}