namespace Parameters.Test.Unit.Domain;

public class FlowParameterTest
{
    private readonly Faker _faker = new();
    private dynamic? _dynamic;

    public FlowParameterTest()
    {
        _dynamic = new
        {
            Id = _faker.Random.Guid(),
            Name = _faker.Name.FullName(),
            Description = _faker.Random.AlphaNumeric(400),
            FlowType = _faker.Random.Int(0,1)
        };
    }

    public void Dispose()
    {
        _dynamic = null;
    }

    [Fact]
    public void Should_Create_New_Instance_Equal_Object()
    {
        FlowParameter flow = new(_dynamic?.Id, _dynamic?.Name, _dynamic?.FlowType, _dynamic?.Description);
        (_dynamic as object).Should().BeEquivalentTo(flow);
    }


    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Should_Throw_Exception_When_Name_Is_Invalid(string name)
    {
        var action = () => new FlowParameter(_dynamic?.Id, name, _dynamic?.FlowType, _dynamic?.Description);
        action.Should().Throw<ArgumentException>().WithMessage($"{nameof(FlowParameter.Name)} can't be null or empty");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Should_Throw_Exception_When_Description_Is_Invalid(string description)
    {
        var action = () => new FlowParameter(_dynamic?.Id, _dynamic?.Name, _dynamic?.FlowType, description);
        action.Should().Throw<ArgumentException>().WithMessage($"{nameof(FlowParameter.Description)} can't be null or empty");
    }
}

public class FlowParameter : BaseEntity
{
    public FlowParameter(string name, int flowType, string description)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException($"{nameof(Name)} can't be null or empty");
        
        if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
            throw new ArgumentException($"{nameof(Description)} can't be null or empty");

        Name = name;
        FlowType = flowType;
        Description = description;
    }

    public FlowParameter(Guid id, string name, int flowType, string description) : this(name, flowType, description)
    {
        Id = id;
    }

    public string Name { get; private set; }
    public int FlowType { get; private set; }
    public string Description { get; private set; }
}