using Parameters.Application.Request.Command.BankAccount;
using Parameters.Application.Request.Handler.BankAccount;
using Parameters.Domain.Repository.BankAccount;
using Parameters.Domain.Repository.FlowParameter;
using Parameters.Infra.Repository.BankAccount;

namespace Parameters.Test.Unit.Application.Handler.BankAccount;

public class DeleteCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Delete_Account()
    {
        var mockLogger = new Mock<ILogger<DeleteBankAccountRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<BankAccountEntity>());

        DeleteBankAccountRepository repository = new(mongoContextMock.Object);

        Mock<IGetByIdBankAccountRepository> getIdRepository = new();

        getIdRepository.Setup(x => x.Execute(It.IsAny<Guid>())).ReturnsAsync(new BankAccountEntity(
            _faker.Random.Guid(),
            _faker.Name.FullName(),
            _faker.Random.Decimal(200),
            _faker.Random.AlphaNumeric(400)
            ));

        DeleteBankAccountRequestCommandHandler bankAccountRequestCommand = new(repository, mockLogger.Object, getIdRepository.Object);

        DeleteBankAccountRequestCommand command = new()
        {
            Id = _faker.Random.Guid()
        };

        var result = await bankAccountRequestCommand.Handle(command, CancellationToken.None);

        result.Should().BeTrue();
    }

    [Fact]
    public void Should_Throw_Exception_Update_Account()
    {
        var mockLogger = new Mock<ILogger<DeleteBankAccountRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<BankAccountEntity>());

        DeleteBankAccountRepository repository = new(mongoContextMock.Object);

        DeleteBankAccountRequestCommandHandler bankAccountRequestCommand = new(repository, mockLogger.Object,
            new Mock<IGetByIdBankAccountRepository>().Object);

        DeleteBankAccountRequestCommand command = new();

        Func<Task> func = () => bankAccountRequestCommand.Handle(command, CancellationToken.None);
        func.Should().ThrowAsync<ArgumentException>();
    }
}