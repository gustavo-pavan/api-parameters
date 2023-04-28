using Parameters.Application.Request.Command.BankAccount;
using Parameters.Application.Request.Handler.BankAccount;
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

        BaseAccountDeleteRepository repository = new(mongoContextMock.Object);

        DeleteBankAccountRequestCommandHandler bankAccountRequestCommand = new(repository, mockLogger.Object);

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

        BaseAccountDeleteRepository repository = new(mongoContextMock.Object);

        DeleteBankAccountRequestCommandHandler bankAccountRequestCommand = new(repository, mockLogger.Object);

        DeleteBankAccountRequestCommand command = new();

        Func<Task> func = () => bankAccountRequestCommand.Handle(command, CancellationToken.None);
        func.Should().ThrowAsync<ArgumentException>();
    }
}