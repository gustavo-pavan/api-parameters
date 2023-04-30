using Parameters.Domain.Entity;

namespace Parameters.Application.Request.Dto;

public class PaymentTypeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public PaymentTypeDto(PaymentType payment)
    {
        Id = payment.Id;
        Name = payment.Name;
        Description = payment.Description;
    }
}