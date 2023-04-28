using Parameters.Application.Request.Command.PaymentType;
using Parameters.Application.Request.Handler.PaymentType;
using Parameters.Infra.Repository.PaymentType;

namespace Parameters.Test.Unit.Application.Handler.PaymentType;

public class GetByIdCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Get_All_Account()
    {
        var mockLogger = new Mock<ILogger<GetByIdRequestCommandHandler>>();
        PaymentTypeEntity paymentType = new(Guid.NewGuid(), _faker.Name.FullName(),_faker.Random.AlphaNumeric(400));
        var mongoContextMock = MongoContextMock.Mock(new List<PaymentTypeEntity> { paymentType });

        GetByIdRepository repository = new(mongoContextMock.Object);

        GetByIdRequestCommandHandler requestCommand = new(repository, mockLogger.Object);

        var result =
            await requestCommand.Handle(new GetByIdRequestCommand { Id = Guid.NewGuid() }, CancellationToken.None);

        result?.Should().NotBeNull();
        result?.Should().Be(paymentType);
    }
}