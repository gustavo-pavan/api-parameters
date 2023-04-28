﻿using Parameters.Application.Request.Command.BankAccount;
using Parameters.Application.Request.Handler.BankAccount;
using Parameters.Infra.Repository.BankAccount;

namespace Parameters.Test.Unit.Application.Handler.BankAccount;

public class CreateCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Create_Account()
    {
        var mockLogger = new Mock<ILogger<CreateRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<BankAccountEntity>());

        CreateRepository repository = new(mongoContextMock.Object);

        CreateRequestCommandHandler requestCommand = new(repository, mockLogger.Object);

        CreateRequestCommand command = new()
        {
            Balance = _faker.Random.Decimal(0, 1000),
            Description = _faker.Random.AlphaNumeric(400),
            Name = _faker.Name.FullName()
        };

        var result = await requestCommand.Handle(command, CancellationToken.None);

        result.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Should_Throw_Exception_Create_Account()
    {
        var mockLogger = new Mock<ILogger<CreateRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<BankAccountEntity>());

        CreateRepository repository = new(mongoContextMock.Object);

        CreateRequestCommandHandler requestCommand = new(repository, mockLogger.Object);

        CreateRequestCommand command = new()
        {
            Balance = _faker.Random.Decimal(0, 1000),
            Description = _faker.Random.AlphaNumeric(400),
            Name = _faker.Name.FullName()
        };

        Func<Task> func = () => requestCommand.Handle(command, CancellationToken.None);
        func.Should().ThrowAsync<ArgumentException>();
    }
}