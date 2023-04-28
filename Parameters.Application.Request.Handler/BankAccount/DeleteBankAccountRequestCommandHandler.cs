using Parameters.Application.Request.Command.BankAccount;
using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Application.Request.Handler.BankAccount;

public class DeleteBankAccountRequestCommandHandler : IRequestHandler<DeleteBankAccountRequestCommand, bool>
{
    private readonly IBaseAccountDeleteRepository _baseAccountDeleteRepository;
    private readonly ILogger<DeleteBankAccountRequestCommandHandler> _logger;

    public DeleteBankAccountRequestCommandHandler(IBaseAccountDeleteRepository baseAccountDeleteRepository,
        ILogger<DeleteBankAccountRequestCommandHandler> logger)
    {
        _baseAccountDeleteRepository = baseAccountDeleteRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteBankAccountRequestCommand bankAccountRequest, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to delete account bank");
            _logger.LogInformation("Execute transaction with database");
            await _baseAccountDeleteRepository.Execute(bankAccountRequest.Id);

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