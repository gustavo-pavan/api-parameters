namespace Parameters.Application.Request.Command.PaymentType;

public class GetByIdPaymentTypeRequestCommand : IRequest<PaymentTypeEntity?>
{
    public Guid Id { get; set; }
}