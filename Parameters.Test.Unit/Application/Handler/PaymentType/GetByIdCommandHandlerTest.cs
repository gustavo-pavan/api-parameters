using Parameters.Application.Request.Command.PaymentType;
using Parameters.Application.Request.Handler.PaymentType;
using Parameters.Application.Request.Query.PaymentType;
using Parameters.Infra.Repository.PaymentType;

namespace Parameters.Test.Unit.Application.Handler.PaymentType;

public class GetByIdCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Get_All_Account()
    {
        var mockLogger = new Mock<ILogger<GetByIdPaymentTypeRequestCommandHandler>>();
        PaymentTypeEntity paymentType = new(Guid.NewGuid(), _faker.Name.FullName(), _faker.Random.AlphaNumeric(400));
        var mongoContextMock = MongoContextMock.Mock(new List<PaymentTypeEntity> { paymentType });

        PaymentTypeGetByIdRepository repository = new(mongoContextMock.Object);

        GetByIdPaymentTypeRequestCommandHandler paymentTypeRequestCommand = new(repository, mockLogger.Object);

        var result =
            await paymentTypeRequestCommand.Handle(new GetByIdPaymentTypeRequestCommand { Id = Guid.NewGuid() },
                CancellationToken.None);

        result?.Should().NotBeNull();
        result?.Should().Be(paymentType);
    }
}