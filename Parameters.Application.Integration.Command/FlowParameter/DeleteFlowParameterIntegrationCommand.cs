namespace Parameters.Application.Integration.Command.FlowParameter;

public class DeleteFlowParameterIntegrationCommand : IntegrationEvent
{
    public Guid Id { get; set; }
}