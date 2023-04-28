using Parameters.Application.Request.Command.BankAccount;
using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Application.Request.Handler.BankAccount;

public class UpdateRequestCommandHandler : IRequestHandler<UpdateRequestCommand, BankAccountEntity>
{
    private readonly ILogger<CreateRequestCommandHandler> _logger;
    private readonly IUpdateRepository _updateRepository;

    public UpdateRequestCommandHandler(IUpdateRepository updateRepository,
        ILogger<CreateRequestCommandHandler> logger)
    {
        _updateRepository = updateRepository;
        _logger = logger;
    }

    public async Task<BankAccountEntity> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to update account bank");
            var account = new BankAccountEntity(request.Id, request.Name, request.Balance, request.Description);

            _logger.LogInformation("Execute transaction with database");
            await _updateRepository.Execute(account);

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