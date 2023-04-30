using Parameters.Application.Request.Command.PaymentType;
using Parameters.Application.Request.Handler.PaymentType;
using Parameters.Infra.Repository.PaymentType;

namespace Parameters.Test.Unit.Application.Handler.PaymentType;

public class UpdateCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Update_Account()
    {
        var mockLogger = new Mock<ILogger<UpdatePaymentTypeRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<PaymentTypeEntity>());

        UpdatePaymentTypeRepository repository = new(mongoContextMock.Object);

        UpdatePaymentTypeRequestCommandHandler paymentTypeRequestCommand = new(repository, mockLogger.Object);

        UpdatePaymentTypeRequestCommand command = new()
        {
            Id = _faker.Random.Guid(),
            Description = _faker.Random.AlphaNumeric(400),
            Name = _faker.Name.FullName()
        };

        var result = await paymentTypeRequestCommand.Handle(command, CancellationToken.None);

        result.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Should_Throw_Exception_Update_Account()
    {
        var mockLogger = new Mock<ILogger<UpdatePaymentTypeRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<PaymentTypeEntity>());

        UpdatePaymentTypeRepository repository = new(mongoContextMock.Object);

        UpdatePaymentTypeRequestCommandHandler paymentTypeRequestCommand = new(repository, mockLogger.Object);

        UpdatePaymentTypeRequestCommand command = new()
        {
            Description = _faker.Random.AlphaNumeric(400),
            Name = _faker.Name.FullName()
        };

        Func<Task> func = () => paymentTypeRequestCommand.Handle(command, CancellationToken.None);
        func.Should().ThrowAsync<ArgumentException>();
    }
}