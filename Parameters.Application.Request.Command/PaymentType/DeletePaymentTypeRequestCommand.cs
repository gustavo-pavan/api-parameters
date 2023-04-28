namespace Parameters.Application.Request.Command.PaymentType;

public class DeletePaymentTypeRequestCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}