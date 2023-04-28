namespace Parameters.Application.Request.Command.PaymentType;

public class CreateRequestCommand : IRequest<PaymentTypeEntity>
{
    public string Name { get; set; }
    public string Description { get; set; }
}