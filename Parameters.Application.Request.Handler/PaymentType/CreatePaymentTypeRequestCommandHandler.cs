using Parameters.Application.Request.Command.PaymentType;
using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Application.Request.Handler.PaymentType;

public class
    CreatePaymentTypeRequestCommandHandler : IRequestHandler<CreatePaymentTypeRequestCommand, PaymentTypeEntity>
{
    private readonly ILogger<CreatePaymentTypeRequestCommandHandler> _logger;
    private readonly IPaymentTypeCreateRepository _paymentTypeCreateRepository;

    public CreatePaymentTypeRequestCommandHandler(IPaymentTypeCreateRepository paymentTypeCreateRepository,
        ILogger<CreatePaymentTypeRequestCommandHandler> logger)
    {
        _paymentTypeCreateRepository = paymentTypeCreateRepository;
        _logger = logger;
    }

    public async Task<PaymentTypeEntity> Handle(CreatePaymentTypeRequestCommand paymentTypeRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to create new payment type");
            var paymentType = new PaymentTypeEntity(paymentTypeRequest.Name, paymentTypeRequest.Description);

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