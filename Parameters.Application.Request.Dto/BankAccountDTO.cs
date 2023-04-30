using Parameters.Domain.Entity;

namespace Parameters.Application.Request.Dto;

public class BankAccountDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Balance { get; set; }

    public BankAccountDto(BankAccount bankAccount)
    {
        Id = bankAccount.Id;
        Name = bankAccount.Name;
        Description = bankAccount.Description;
        Balance = bankAccount.Balance;
    }
}