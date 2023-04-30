using Parameters.Application.Request.Command.PaymentType;
using Parameters.Application.Request.Handler.PaymentType;
using Parameters.Domain.Repository.PaymentType;
using Parameters.Infra.Repository.PaymentType;

namespace Parameters.Test.Unit.Application.Handler.PaymentType;

public class DeleteCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Delete_Account()
    {
        var mockLogger = new Mock<ILogger<DeletePaymentTypeRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<PaymentTypeEntity>());

        DeletePaymentTypeRepository repository = new(mongoContextMock.Object);

        Mock<IGetByIdPaymentTypeRepository> getIdRepository = new();

        getIdRepository.Setup(x => x.Execute(It.IsAny<Guid>())).ReturnsAsync(new PaymentTypeEntity(
            _faker.Random.Guid(),
            _faker.Name.FullName(),
            _faker.Random.AlphaNumeric(400)
        ));

        DeletePaymentTypeRequestCommandHandler paymentTypeRequestCommand = new(repository, mockLogger.Object,
            getIdRepository.Object);

        DeletePaymentTypeRequestCommand command = new()
        {
            Id = _faker.Random.Guid()
        };

        var result = await paymentTypeRequestCommand.Handle(command, CancellationToken.None);

        result.Should().BeTrue();
    }

    [Fact]
    public void Should_Throw_Exception_Update_Account()
    {
        var mockLogger = new Mock<ILogger<DeletePaymentTypeRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<PaymentTypeEntity>());

        DeletePaymentTypeRepository repository = new(mongoContextMock.Object);

        DeletePaymentTypeRequestCommandHandler paymentTypeRequestCommand = new(repository, mockLogger.Object,
            new Mock<IGetByIdPaymentTypeRepository>().Object);

        DeletePaymentTypeRequestCommand command = new();

        Func<Task> func = () => paymentTypeRequestCommand.Handle(command, CancellationToken.None);
        func.Should().ThrowAsync<ArgumentException>();
    }
}