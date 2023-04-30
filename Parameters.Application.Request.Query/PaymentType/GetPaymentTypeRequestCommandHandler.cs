using Parameters.Application.Request.Command.PaymentType;
using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Application.Request.Query.PaymentType;

public class
    GetPaymentTypeRequestCommandHandler : IRequestHandler<GetPaymentTypeGetRequestCommand,
        IEnumerable<PaymentTypeEntity>>
{
    private readonly ILogger<GetPaymentTypeRequestCommandHandler> _logger;
    private readonly IGetPaymentTypeRepository _getPaymentTypeRepository;

    public GetPaymentTypeRequestCommandHandler(IGetPaymentTypeRepository getPaymentTypeRepository,
        ILogger<GetPaymentTypeRequestCommandHandler> logger)
    {
        _getPaymentTypeRepository = getPaymentTypeRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<PaymentTypeEntity>> Handle(GetPaymentTypeGetRequestCommand paymentTypeGetRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to get payment type");
            _logger.LogInformation("Execute transaction with database");
            var result = await _getPaymentTypeRepository.Execute();

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