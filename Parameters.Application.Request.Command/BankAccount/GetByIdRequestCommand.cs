namespace Parameters.Application.Request.Command.BankAccount;

public class GetByIdRequestCommand : IRequest<BankAccountEntity?>
{
    public Guid Id { get; set; }
}