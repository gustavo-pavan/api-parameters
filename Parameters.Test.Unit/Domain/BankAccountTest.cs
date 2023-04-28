namespace Parameters.Test.Unit.Domain;

public class BankAccountTest
{
    private readonly Faker _faker = new();
    private dynamic? _dynamic;

    public BankAccountTest()
    {
        _dynamic = new
        {
            Id = _faker.Random.Guid(),
            Name = _faker.Name.FullName(),
            Description = _faker.Random.AlphaNumeric(400),
            Balance = _faker.Random.Decimal(0, 1000)
        };
    }

    public void Dispose()
    {
        _dynamic = null;
    }

    [Fact]
    public void Should_Create_New_Instance_Equal_Object()
    {
        BankAccountEntity account = new(_dynamic?.Id, _dynamic?.Name, _dynamic?.Balance, _dynamic?.Description);
        (_dynamic as object).Should().BeEquivalentTo(account);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Should_Throw_Exception_When_Name_Is_Invalid(string name)
    {
        var action = () => new BankAccountEntity(_dynamic?.Id, name, _dynamic?.Balance, _dynamic?.Description);
        action.Should().Throw<ArgumentException>()
            .WithMessage($"{nameof(BankAccountEntity.Name)} can't be null or empty");
    }
}