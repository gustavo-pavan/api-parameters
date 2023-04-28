using Parameters.Application.Request.Command.BankAccount;
using Parameters.Application.Request.Handler.BankAccount;
using Parameters.Infra.Repository.BankAccount;

namespace Parameters.Test.Unit.Application.Handler.BankAccount;

public class GetCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public void Should_Get_All_Account()
    {
        var mockLogger = new Mock<ILogger<GetRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<BankAccountEntity>
        {
            new(Guid.NewGuid(), _faker.Name.FullName(), _faker.Random.Decimal(200), _faker.Random.AlphaNumeric(400))
        });

        GetRepository repository = new(mongoContextMock.Object);

        GetRequestCommandHandler requestCommand = new(repository, mockLogger.Object);

        var result = requestCommand.Handle(new GetRequestCommand(), CancellationToken.None).Result?.ToArray();

        result?.Should().NotBeNull();
        result?.Count().Should().Be(1);
    }
}