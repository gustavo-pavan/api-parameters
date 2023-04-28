using Parameters.Application.Request.Command.BankAccount;
using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Application.Request.Handler.BankAccount;

public class DeleteRequestCommandHandler : IRequestHandler<DeleteRequestCommand, bool>
{
    private readonly IBaseAccountDeleteRepository _baseAccountDeleteRepository;
    private readonly ILogger<DeleteRequestCommandHandler> _logger;

    public DeleteRequestCommandHandler(IBaseAccountDeleteRepository baseAccountDeleteRepository,
        ILogger<DeleteRequestCommandHandler> logger)
    {
        _baseAccountDeleteRepository = baseAccountDeleteRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to delete account bank");
            _logger.LogInformation("Execute transaction with database");
            await _baseAccountDeleteRepository.Execute(request.Id);

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