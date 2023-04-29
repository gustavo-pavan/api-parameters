namespace Parameters.Application.Integration.Command.BankAccount;

public class CreateBankAccountCommand : IntegrationEvent
{
    public Guid BankAccountEventId { get; set; }
}