using Parameters.Application.Request.Command.PaymentType;
using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Application.Request.Handler.PaymentType;

public class UpdatePaymentTypeRequestCommandHandler : IRequestHandler<UpdatePaymentTypeRequestCommand, PaymentTypeEntity>
{
    private readonly ILogger<UpdatePaymentTypeRequestCommandHandler> _logger;
    private readonly IPaymentTypeUpdateRepository _paymentTypeUpdateRepository;

    public UpdatePaymentTypeRequestCommandHandler(IPaymentTypeUpdateRepository paymentTypeUpdateRepository,
        ILogger<UpdatePaymentTypeRequestCommandHandler> logger)
    {
        _paymentTypeUpdateRepository = paymentTypeUpdateRepository;
        _logger = logger;
    }

    public async Task<PaymentTypeEntity> Handle(UpdatePaymentTypeRequestCommand paymentTypeRequest, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to update payment");
            var paymentType = new PaymentTypeEntity(paymentTypeRequest.Id, paymentTypeRequest.Name, paymentTypeRequest.Description);
            _logger.LogInformation("Execute transaction with database");
            await _paymentTypeUpdateRepository.Execute(paymentType);

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