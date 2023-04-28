using Parameters.Application.Request.Command.BankAccount;
using Parameters.Application.Request.Handler.BankAccount;
using Parameters.Infra.Repository.BankAccount;

namespace Parameters.Test.Unit.Application.Handler.BankAccount;

public class UpdateCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Update_Account()
    {
        var mockLogger = new Mock<ILogger<CreateBankAccountRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<BankAccountEntity>());

        BaseAccountUpdateRepository repository = new(mongoContextMock.Object);

        UpdateBankAccountRequestCommandHandler bankAccountRequestCommand = new(repository, mockLogger.Object);

        UpdateBankAccountRequestCommand command = new()
        {
            Id = _faker.Random.Guid(),
            Balance = _faker.Random.Decimal(0, 1000),
            Description = _faker.Random.AlphaNumeric(400),
            Name = _faker.Name.FullName()
        };

        var result = await bankAccountRequestCommand.Handle(command, CancellationToken.None);

        result.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Should_Throw_Exception_Update_Account()
    {
        var mockLogger = new Mock<ILogger<CreateBankAccountRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<BankAccountEntity>());

        BaseAccountUpdateRepository repository = new(mongoContextMock.Object);

        UpdateBankAccountRequestCommandHandler bankAccountRequestCommand = new(repository, mockLogger.Object);

        UpdateBankAccountRequestCommand command = new()
        {
            Balance = _faker.Random.Decimal(0, 1000),
            Description = _faker.Random.AlphaNumeric(400),
            Name = _faker.Name.FullName()
        };

        Func<Task> func = () => bankAccountRequestCommand.Handle(command, CancellationToken.None);
        func.Should().ThrowAsync<ArgumentException>();
    }
}