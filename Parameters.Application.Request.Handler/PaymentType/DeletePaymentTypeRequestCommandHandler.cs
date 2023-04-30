using Parameters.Application.Notification.Command.PaymentType;
using Parameters.Application.Request.Command.PaymentType;
using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Application.Request.Handler.PaymentType;

public class DeletePaymentTypeRequestCommandHandler : IRequestHandler<DeletePaymentTypeRequestCommand, bool>
{
    private readonly ILogger<DeletePaymentTypeRequestCommandHandler> _logger;
    private readonly IDeletePaymentTypeRepository _deletePaymentTypeRepository;
    private readonly IGetByIdPaymentTypeRepository _getByIdPaymentTypeRepository;

    public DeletePaymentTypeRequestCommandHandler(IDeletePaymentTypeRepository deletePaymentTypeRepository,
        ILogger<DeletePaymentTypeRequestCommandHandler> logger,
        IGetByIdPaymentTypeRepository getByIdPaymentTypeRepository)
    {
        _deletePaymentTypeRepository = deletePaymentTypeRepository;
        _logger = logger;
        _getByIdPaymentTypeRepository = getByIdPaymentTypeRepository;
    }

    public async Task<bool> Handle(DeletePaymentTypeRequestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to delete payment type");

            var paymentType = await _getByIdPaymentTypeRepository.Execute(request.Id);

            _logger.LogInformation("Execute transaction with database");
            await _deletePaymentTypeRepository.Execute(request.Id);

            _logger.LogInformation("Send new notification to delete payment type");
            paymentType?.AddDomainEvent(new DeletePaymentTypeNotificationCommand { Id = paymentType.Id });

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