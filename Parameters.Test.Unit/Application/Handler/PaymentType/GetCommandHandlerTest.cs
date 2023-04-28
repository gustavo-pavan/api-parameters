using Parameters.Application.Request.Command.PaymentType;
using Parameters.Application.Request.Handler.PaymentType;
using Parameters.Infra.Repository.PaymentType;

namespace Parameters.Test.Unit.Application.Handler.PaymentType;

public class GetCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public void Should_Get_All_Account()
    {
        var mockLogger = new Mock<ILogger<GetRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<PaymentTypeEntity>
        {
            new(Guid.NewGuid(), _faker.Name.FullName(), _faker.Random.AlphaNumeric(400))
        });

        GetRepository repository = new(mongoContextMock.Object);

        GetRequestCommandHandler requestCommand = new(repository, mockLogger.Object);

        var result = requestCommand.Handle(new GetRequestCommand(), CancellationToken.None).Result?.ToArray();

        result?.Should().NotBeNull();
        result?.Count().Should().Be(1);
    }
}