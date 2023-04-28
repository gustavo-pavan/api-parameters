namespace Parameters.Application.Request.Command.FlowParameter;

public class GetByIdFlowParameterRequestCommand : IRequest<FlowParameterEntity?>
{
    public Guid Id { get; set; }
}