namespace Parameters.Application.Request.Command.PaymentType;

public class DeleteRequestCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}