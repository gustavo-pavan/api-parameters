namespace Parameters.Application.Integration.Command.FlowParameter;

public class CreateFlowParameterIntegrationCommand : IntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int FlowType { get; set; }
}