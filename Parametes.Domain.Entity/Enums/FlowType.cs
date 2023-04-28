namespace Parameters.Domain.Entity.Enums;

public class FlowType : FlowEnumeration
{
    public static FlowType Debit = new(1, "Debit", "Paid or payable bills");
    public static FlowType Credit = new(2, "Credit", "Down payment or you will still receive the money");

    public FlowType(int id, string name, string description) : base(id, name, description)
    {
    }
}