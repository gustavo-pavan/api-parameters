using Parameters.Application.Request.Command.PaymentType;
using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Application.Request.Handler.PaymentType;

public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, PaymentTypeEntity>
{
    private readonly ILogger<CreateRequestCommandHandler> _logger;
    private readonly IPaymentTypeCreateRepository _paymentTypeCreateRepository;

    public CreateRequestCommandHandler(IPaymentTypeCreateRepository paymentTypeCreateRepository,
        ILogger<CreateRequestCommandHandler> logger)
    {
        _paymentTypeCreateRepository = paymentTypeCreateRepository;
        _logger = logger;
    }

    public async Task<PaymentTypeEntity> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to create new payment type");
            var paymentType = new PaymentTypeEntity(request.Name, request.Description);

            _logger.LogInformation("Execute transaction with database");
            await _paymentTypeCreateRepository.Execute(paymentType);

            _logger.LogInformation("Create payment type with success");
            return paymentType;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error: {e.Message}");
            throw;
        }
    }
}