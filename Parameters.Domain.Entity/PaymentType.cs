namespace Parameters.Domain.Entity;

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