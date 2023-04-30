using Parameters.Application.Notification.Command.PaymentType;
using Parameters.Application.Request.Command.PaymentType;
using Parameters.Application.Request.Dto;
using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Application.Request.Handler.PaymentType;

public class
    UpdatePaymentTypeRequestCommandHandler : IRequestHandler<UpdatePaymentTypeRequestCommand, PaymentTypeDto>
{
    private readonly ILogger<UpdatePaymentTypeRequestCommandHandler> _logger;
    private readonly IUpdatePaymentTypeRepository _updatePaymentTypeRepository;

    public UpdatePaymentTypeRequestCommandHandler(IUpdatePaymentTypeRepository updatePaymentTypeRepository,
        ILogger<UpdatePaymentTypeRequestCommandHandler> logger)
    {
        _updatePaymentTypeRepository = updatePaymentTypeRepository;
        _logger = logger;
    }

    public async Task<PaymentTypeDto> Handle(UpdatePaymentTypeRequestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to update payment");
            var paymentType = new PaymentTypeEntity(request.Id, request.Name,
                request.Description);
            _logger.LogInformation("Execute transaction with database");
            await _updatePaymentTypeRepository.Execute(paymentType);

            _logger.LogInformation("Send new notification to update payment type");
            paymentType.AddDomainEvent(new UpdatePaymentTypeNotificationCommand
                { Id = paymentType.Id, Name = paymentType.Name });

            _logger.LogInformation("Update payment with success");
            return new(paymentType);
        }
        catch (Exception e)
        {
            _logger.LogError($"Error Update: {e.Message}");
            throw;
        }
    }
}