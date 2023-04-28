using System.Reflection;

namespace Parameters.Domain.Entity.Enums;

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