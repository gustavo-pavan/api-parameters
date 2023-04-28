namespace Parameters.Application.Request.Command.FlowParameter;

public class CreateRequestCommand : IRequest<FlowParameterEntity>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int FlowType { get; set; }
}