using Parameters.Application.Request.Command.PaymentType;
using Parameters.Application.Request.Handler.PaymentType;
using Parameters.Infra.Repository.PaymentType;

namespace Parameters.Test.Unit.Application.Handler.PaymentType;

public class CreateCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Create_Account()
    {
        var mockLogger = new Mock<ILogger<CreatePaymentTypeRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<PaymentTypeEntity>());

        PaymentTypeCreateRepository repository = new(mongoContextMock.Object);

        CreatePaymentTypeRequestCommandHandler paymentTypeRequestCommand = new(repository, mockLogger.Object);

        CreatePaymentTypeRequestCommand command = new()
        {
            Description = _faker.Random.AlphaNumeric(400),
            Name = _faker.Name.FullName()
        };

        var result = await paymentTypeRequestCommand.Handle(command, CancellationToken.None);

        result.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Should_Throw_Exception_Create_Account()
    {
        var mockLogger = new Mock<ILogger<CreatePaymentTypeRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<PaymentTypeEntity>());

        PaymentTypeCreateRepository repository = new(mongoContextMock.Object);

        CreatePaymentTypeRequestCommandHandler paymentTypeRequestCommand = new(repository, mockLogger.Object);

        CreatePaymentTypeRequestCommand command = new()
        {
            Description = _faker.Random.AlphaNumeric(400),
            Name = _faker.Name.FullName()
        };

        Func<Task> func = () => paymentTypeRequestCommand.Handle(command, CancellationToken.None);
        func.Should().ThrowAsync<ArgumentException>();
    }
}