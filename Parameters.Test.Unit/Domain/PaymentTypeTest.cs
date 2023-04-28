namespace Parameters.Test.Unit.Domain;

public class PaymentTypeTest
{
    private readonly Faker _faker = new();
    private dynamic? _dynamic;

    public PaymentTypeTest()
    {
        _dynamic = new
        {
            Id = _faker.Random.Guid(),
            Name = _faker.Name.FullName(),
            Description = _faker.Random.AlphaNumeric(400)
        };
    }

    public void Dispose()
    {
        _dynamic = null;
    }

    [Fact]
    public void Should_Create_New_Instance_Equal_Object()
    {
        PaymentType flow = new(_dynamic?.Id, _dynamic?.Name, _dynamic?.Description);
        (_dynamic as object).Should().BeEquivalentTo(flow);
    }


    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Should_Throw_Exception_When_Name_Is_Invalid(string name)
    {
        var action = () => new PaymentType(_dynamic?.Id, name, _dynamic?.Description);
        action.Should().Throw<ArgumentException>().WithMessage($"{nameof(FlowParameter.Name)} can't be null or empty");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Should_Throw_Exception_When_Description_Is_Invalid(string description)
    {
        var action = () => new PaymentType(_dynamic?.Id, _dynamic?.Name, description);
        action.Should().Throw<ArgumentException>().WithMessage($"{nameof(FlowParameter.Description)} can't be null or empty");
    }
}

public class PaymentType : BaseEntity
{
    public PaymentType(string name, string description)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException($"{nameof(Name)} can't be null or empty"); 
        
        if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
            throw new ArgumentException($"{nameof(Description)} can't be null or empty");

        Name = name;
        Description = description;
    }

    public PaymentType(Guid id, string name, string description) : this(name, description)
    {
        Id = id;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
}