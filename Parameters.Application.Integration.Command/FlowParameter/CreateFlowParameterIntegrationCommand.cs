namespace Parameters.Application.Integration.Command.BankAccount;

public class CreateFlowParameterIntegrationCommand : IntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}