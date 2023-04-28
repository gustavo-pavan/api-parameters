using Parameters.Application.Request.Command.PaymentType;
using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Application.Request.Handler.PaymentType;

public class UpdateRequestCommandHandler : IRequestHandler<UpdateRequestCommand, PaymentTypeEntity>
{
    private readonly ILogger<BankAccount.CreateRequestCommandHandler> _logger;
    private readonly IUpdateRepository _updateRepository;

    public UpdateRequestCommandHandler(IUpdateRepository updateRepository,
        ILogger<BankAccount.CreateRequestCommandHandler> logger)
    {
        _updateRepository = updateRepository;
        _logger = logger;
    }

    public async Task<PaymentTypeEntity> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to update payment");
            var paymentType = new PaymentTypeEntity(request.Id, request.Name, request.Description);
            _logger.LogInformation("Execute transaction with database");
            await _updateRepository.Execute(paymentType);

            _logger.LogInformation("Update payment with success");
            return paymentType;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error Update: {e.Message}");
            throw;
        }
    }
}