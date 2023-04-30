using Parameters.Application.Request.Dto;

namespace Parameters.Application.Request.Command.PaymentType;

public class CreatePaymentTypeRequestCommand : IRequest<PaymentTypeDto>
{
    public string Name { get; set; }
    public string Description { get; set; }
}