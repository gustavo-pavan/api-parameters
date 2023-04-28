namespace Parameters.Application.Request.Command.FlowParameter;

public class DeleteRequestCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}