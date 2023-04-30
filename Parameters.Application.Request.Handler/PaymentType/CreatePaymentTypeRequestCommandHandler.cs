using Parameters.Application.Notification.Command.PaymentType;
using Parameters.Application.Request.Command.PaymentType;
using Parameters.Domain.Repository.PaymentType;

namespace Parameters.Application.Request.Handler.PaymentType;

public class
    CreatePaymentTypeRequestCommandHandler : IRequestHandler<CreatePaymentTypeRequestCommand, PaymentTypeEntity>
{
    private readonly ILogger<CreatePaymentTypeRequestCommandHandler> _logger;
    private readonly ICreatePaymentTypeRepository _createPaymentTypeRepository;

    public CreatePaymentTypeRequestCommandHandler(ICreatePaymentTypeRepository createPaymentTypeRepository,
        ILogger<CreatePaymentTypeRequestCommandHandler> logger)
    {
        _createPaymentTypeRepository = createPaymentTypeRepository;
        _logger = logger;
    }

    public async Task<PaymentTypeEntity> Handle(CreatePaymentTypeRequestCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Start handler to create new payment type");
            var paymentType = new PaymentTypeEntity(request.Name, request.Description);

            _logger.LogInformation("Execute transaction with database");
            await _createPaymentTypeRepository.Execute(paymentType);

            _logger.LogInformation("Send new notification to create payment type");
            paymentType.AddDomainEvent(new CreatePaymentTypeNotificationCommand
                { Id = paymentType.Id, Name = paymentType.Name });

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