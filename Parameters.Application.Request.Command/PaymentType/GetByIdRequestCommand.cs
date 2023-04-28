namespace Parameters.Application.Request.Command.PaymentType;

public class GetByIdRequestCommand : IRequest<PaymentTypeEntity?>
{
    public Guid Id { get; set; }
}