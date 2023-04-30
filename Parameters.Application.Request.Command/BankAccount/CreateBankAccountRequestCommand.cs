using Parameters.Application.Request.Dto;

namespace Parameters.Application.Request.Command.BankAccount;

public class CreateBankAccountRequestCommand : IRequest<BankAccountDto>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Balance { get; set; }
}