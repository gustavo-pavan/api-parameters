namespace Parameters.Application.Request.Command.FlowParameter;

public class DeleteFlowParameterRequestCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}