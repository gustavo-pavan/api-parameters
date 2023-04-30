namespace Parameters.Application.Integration.Command.BankAccount;

public class DeleteBankAccountIntegrationCommand : IntegrationEvent
{
    public Guid Id { get; set; }
}