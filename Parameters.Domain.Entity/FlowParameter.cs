using Parameters.Domain.Entity.Enums;

namespace Parameters.Domain.Entity;

public class FlowParameter : BaseEntity
{
    public FlowParameter(string name, FlowType flowType, string description)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException($"{nameof(Name)} can't be null or empty");

        if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
            throw new ArgumentException($"{nameof(Description)} can't be null or empty");

        Name = name;
        FlowType = flowType;
        Description = description;
    }

    public FlowParameter(Guid id, string name, FlowType flowType, string description) : this(name, flowType, description)
    {
        Id = id;
    }

    public string Name { get; private set; }
    public FlowType FlowType { get; private set; }
    public string Description { get; private set; }
}