namespace Parameters.Application.Request.Command.BankAccount;

public class GetByIdBankAccountRequestCommand : IRequest<BankAccountEntity?>
{
    public Guid Id { get; set; }
}