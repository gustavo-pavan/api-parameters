using Parameters.Domain.Entity.Enums;

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
            FlowType = FlowEnumeration.FromValue<FlowType>(_faker.Random.Int(1, 2))
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

    [Fact]
    public void Should_Throw_Exception_When_Description_Is_Invalid()
    {
        var action = () => new FlowParameter(_dynamic?.Id, _dynamic?.Name, _dynamic?.FlowType, null);
        action.Should().Throw<ArgumentException>().WithMessage($"{nameof(FlowParameter.Description)} can't be null or empty");
    }
}