namespace Parameters.Application.Request.Command.FlowParameter;

public class UpdateFlowParameterUpdateRequestCommand : IRequest<FlowParameterEntity>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int FlowType { get; set; }
}