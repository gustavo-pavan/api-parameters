using Parameters.Application.Request.Command.BankAccount;
using Parameters.Application.Request.Handler.BankAccount;
using Parameters.Infra.Repository.BankAccount;

namespace Parameters.Test.Unit.Application.Handler.BankAccount;

public class GetByIdCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Get_All_Account()
    {
        var mockLogger = new Mock<ILogger<GetByIdBankAccountRequestCommandHandler>>();
        BankAccountEntity account = new(Guid.NewGuid(), _faker.Name.FullName(), _faker.Random.Decimal(200),
            _faker.Random.AlphaNumeric(400));
        var mongoContextMock = MongoContextMock.Mock(new List<BankAccountEntity> { account });

        BaseAccountGetByIdRepository repository = new(mongoContextMock.Object);

        GetByIdBankAccountRequestCommandHandler bankAccountRequestCommand = new(repository, mockLogger.Object);

        var result =
            await bankAccountRequestCommand.Handle(new GetByIdBankAccountRequestCommand { Id = Guid.NewGuid() }, CancellationToken.None);

        result?.Should().NotBeNull();
        result?.Should().Be(account);
    }
}