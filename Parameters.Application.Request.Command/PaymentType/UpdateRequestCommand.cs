﻿namespace Parameters.Application.Request.Command.PaymentType;

public class UpdateRequestCommand : IRequest<PaymentTypeEntity>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

}