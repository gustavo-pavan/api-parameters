using Parameters.Application.Request.Dto;

namespace Parameters.Application.Request.Command.FlowParameter;

public class CreateFlowParameterRequestCommand : IRequest<FlowParameterDto>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int FlowType { get; set; }
}