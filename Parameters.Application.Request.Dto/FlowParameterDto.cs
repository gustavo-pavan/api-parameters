using Parameters.Domain.Entity;
using Parameters.Domain.Entity.Enums;

namespace Parameters.Application.Request.Dto;

public class FlowParameterDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public FlowType FlowType { get; set; }

    public FlowParameterDto(FlowParameter parameter)
    {
        Id = parameter.Id;
        Name = parameter.Name;
        Description = parameter.Description;
        FlowType = parameter.FlowType;
    }
}