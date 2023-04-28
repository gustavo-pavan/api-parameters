namespace Parameters.Application.Request.Command.FlowParameter;

public class GetByIdRequestCommand : IRequest<FlowParameterEntity?>
{
    public Guid Id { get; set; }
}