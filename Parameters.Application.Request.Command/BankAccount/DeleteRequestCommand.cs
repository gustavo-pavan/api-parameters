namespace Parameters.Application.Request.Command.BankAccount;

public class DeleteRequestCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}