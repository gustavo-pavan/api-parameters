namespace Parameters.Application.Request.Command.BankAccount;

public class DeleteBankAccountRequestCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}