﻿using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Application.Request.Handler.FlowParameter;
using Parameters.Domain.Entity.Enums;
using Parameters.Domain.Repository.FlowParameter;
using Parameters.Infra.Repository.FlowParameter;

namespace Parameters.Test.Unit.Application.Handler.FlowParameter;

public class DeleteCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Delete_Account()
    {
        var mockLogger = new Mock<ILogger<DeleteFlowParameterRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<FlowParameterEntity>());

        DeleteFlowParameterRepository repository = new(mongoContextMock.Object);

        Mock<IGetByIdFlowParameterRepository> getIdRepository = new();

        getIdRepository.Setup(x => x.Execute(It.IsAny<Guid>())).ReturnsAsync(new FlowParameterEntity(
            _faker.Random.Guid(),
            _faker.Name.FullName(),
           FlowType.Credit,
            _faker.Random.AlphaNumeric(400)
        ));


        DeleteFlowParameterRequestCommandHandler flowParameterRequestCommand = new(repository, mockLogger.Object,
            getIdRepository.Object);

        DeleteFlowParameterRequestCommand command = new()
        {
            Id = _faker.Random.Guid()
        };

        var result = await flowParameterRequestCommand.Handle(command, CancellationToken.None);

        result.Should().BeTrue();
    }

    [Fact]
    public void Should_Throw_Exception_Update_Account()
    {
        var mockLogger = new Mock<ILogger<DeleteFlowParameterRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<FlowParameterEntity>());

        DeleteFlowParameterRepository repository = new(mongoContextMock.Object);

        DeleteFlowParameterRequestCommandHandler flowParameterRequestCommand = new(repository, mockLogger.Object,
            new Mock<IGetByIdFlowParameterRepository>().Object);

        DeleteFlowParameterRequestCommand command = new();

        Func<Task> func = () => flowParameterRequestCommand.Handle(command, CancellationToken.None);
        func.Should().ThrowAsync<ArgumentException>();
    }
}