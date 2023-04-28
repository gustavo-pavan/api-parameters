using System.Reflection;

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

public class FlowType : FlowEnumeration
{
    public static FlowType Debit = new(1, "Debit", "Paid or payable bills");
    public static FlowType Credit = new(2, "Credit", "Down payment or you will still receive the money");

    public FlowType(int id, string name, string description) : base(id, name, description)
    {
    }
}

public abstract class FlowEnumeration : IComparable
{
    protected FlowEnumeration(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public int Id { get; }
    public string Name { get; }
    public string Description { get; private set; }

    public int CompareTo(object? obj)
    {
        return Id.CompareTo(((FlowEnumeration)obj!)!.Id);
    }

    public static IEnumerable<T> GetAll<T>() where T : FlowEnumeration
    {
        return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(x => x.GetValue(null))
            .Cast<T>();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not FlowEnumeration otherValue) return false;

        var typeMatches = GetType() == obj.GetType();
        var valueMatches = Id.Equals(otherValue.Id);
        return typeMatches && valueMatches;
    }

    public static T FromValue<T>(int value) where T : FlowEnumeration
    {
        var match = Parse<T, int>(value, "value", item => item.Id == value);
        return match;
    }

    public static T FromName<T>(string name) where T : FlowEnumeration
    {
        var match = Parse<T, string>(name, "name", item => item.Name == name);
        return match;
    }

    private static T Parse<T, TK>(TK value, string description, Func<T, bool> predicate) where T : FlowEnumeration
    {
        var match = GetAll<T>().FirstOrDefault(predicate);
        if (match != null) return match;

        var message = string.Format($"{value} is not a valid {description} in {typeof(T)}");
        throw new Exception(message);
    }
}