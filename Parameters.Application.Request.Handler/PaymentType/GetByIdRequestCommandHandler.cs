using Parameters.Application.Request.Command.PaymentType;
using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Application.Request.Handler.PaymentType;

public class GetByIdRequestCommandHandler : IRequestHandler<GetByIdRequestCommand, PaymentTypeEntity?>
{
    private readonly ILogger<GetByIdRequestCommandHandler> _logger;
    private readonly IPaymentTypeGetByIdRepository _paymentTypeGetByIdRepository;

    public GetByIdRequestCommandHandler(IPaymentTypeGetByIdRepository paymentTypeGetByIdRepository,
        ILogger<GetByIdRequestCommandHandler> logger)
    {
        _paymentTypeGetByIdRepository = paymentTypeGetByIdRepository;
        _logger = logger;
    }

    public async Task<PaymentTypeEntity?> Handle(GetByIdRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to get payment type");
            _logger.LogInformation("Execute transaction with database");
            var result = await _paymentTypeGetByIdRepository.Execute(request.Id);

            _logger.LogInformation("Get payment type with success");
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error: {e.Message}");
            throw;
        }
    }
}