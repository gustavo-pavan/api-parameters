using Parameters.Application.Request.Command.PaymentType;
using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Application.Request.Handler.PaymentType;

public class DeletePaymentTypeRequestCommandHandler : IRequestHandler<DeletePaymentTypeRequestCommand, bool>
{
    private readonly ILogger<DeletePaymentTypeRequestCommandHandler> _logger;
    private readonly IPaymentTypeDeleteRepository _paymentTypeDeleteRepository;

    public DeletePaymentTypeRequestCommandHandler(IPaymentTypeDeleteRepository paymentTypeDeleteRepository,
        ILogger<DeletePaymentTypeRequestCommandHandler> logger)
    {
        _paymentTypeDeleteRepository = paymentTypeDeleteRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeletePaymentTypeRequestCommand paymentTypeRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to delete payment type");
            _logger.LogInformation("Execute transaction with database");
            await _paymentTypeDeleteRepository.Execute(paymentTypeRequest.Id);

            _logger.LogInformation("Delete payment type with success");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"Error: {e.Message}");
            throw;
        }
    }
}