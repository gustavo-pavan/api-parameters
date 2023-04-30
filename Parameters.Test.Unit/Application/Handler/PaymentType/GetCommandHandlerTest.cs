using Parameters.Application.Request.Command.PaymentType;
using Parameters.Application.Request.Query.PaymentType;
using Parameters.Infra.Repository.PaymentType;

namespace Parameters.Test.Unit.Application.Query.PaymentType;

public class GetCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public void Should_Get_All_Account()
    {
        var mockLogger = new Mock<ILogger<GetPaymentTypeRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<PaymentTypeEntity>
        {
            new(Guid.NewGuid(), _faker.Name.FullName(), _faker.Random.AlphaNumeric(400))
        });

        GetPaymentTypeRepository repository = new(mongoContextMock.Object);

        GetPaymentTypeRequestCommandHandler paymentTypeRequestCommand = new(repository, mockLogger.Object);

        var result = paymentTypeRequestCommand.Handle(new GetPaymentTypeGetRequestCommand(), CancellationToken.None)
            .Result?.ToArray();

        result?.Should().NotBeNull();
        result?.Count().Should().Be(1);
    }
}