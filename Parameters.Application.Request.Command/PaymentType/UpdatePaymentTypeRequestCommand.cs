using Parameters.Application.Request.Dto;

namespace Parameters.Application.Request.Command.PaymentType;

public class UpdatePaymentTypeRequestCommand : IRequest<PaymentTypeDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}