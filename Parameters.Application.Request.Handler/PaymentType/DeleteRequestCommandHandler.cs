using Parameters.Application.Request.Command.PaymentType;
using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Application.Request.Handler.PaymentType;

public class DeleteRequestCommandHandler : IRequestHandler<DeleteRequestCommand, bool>
{
    private readonly ILogger<DeleteRequestCommandHandler> _logger;
    private readonly IPaymentTypeDeleteRepository _paymentTypeDeleteRepository;

    public DeleteRequestCommandHandler(IPaymentTypeDeleteRepository paymentTypeDeleteRepository,
        ILogger<DeleteRequestCommandHandler> logger)
    {
        _paymentTypeDeleteRepository = paymentTypeDeleteRepository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to delete payment type");
            _logger.LogInformation("Execute transaction with database");
            await _paymentTypeDeleteRepository.Execute(request.Id);

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