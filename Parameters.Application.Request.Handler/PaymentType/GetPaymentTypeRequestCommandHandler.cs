using Parameters.Application.Request.Command.PaymentType;
using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Application.Request.Handler.PaymentType;

public class
    GetPaymentTypeRequestCommandHandler : IRequestHandler<GetPaymentTypeGetRequestCommand,
        IEnumerable<PaymentTypeEntity>>
{
    private readonly ILogger<GetPaymentTypeRequestCommandHandler> _logger;
    private readonly IPaymentTypeGetRepository _paymentTypeGetRepository;

    public GetPaymentTypeRequestCommandHandler(IPaymentTypeGetRepository paymentTypeGetRepository,
        ILogger<GetPaymentTypeRequestCommandHandler> logger)
    {
        _paymentTypeGetRepository = paymentTypeGetRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<PaymentTypeEntity>> Handle(GetPaymentTypeGetRequestCommand paymentTypeGetRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to get payment type");
            _logger.LogInformation("Execute transaction with database");
            var result = await _paymentTypeGetRepository.Execute();

            _logger.LogInformation("Get payments type lows with success");
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error: {e.Message}");
            throw;
        }
    }
}