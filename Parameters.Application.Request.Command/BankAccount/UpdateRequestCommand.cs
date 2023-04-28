﻿namespace Parameters.Application.Request.Command.BankAccount;

public class UpdateRequestCommand : IRequest<BankAccountEntity>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Balance { get; set; }
}