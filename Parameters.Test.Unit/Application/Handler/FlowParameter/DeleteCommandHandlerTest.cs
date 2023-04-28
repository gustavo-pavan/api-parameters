using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Application.Request.Handler.FlowParameter;
using Parameters.Infra.Repository.FlowParameter;

namespace Parameters.Test.Unit.Application.Handler.FlowParameter;

public class DeleteCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Delete_Account()
    {
        var mockLogger = new Mock<ILogger<DeleteRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<FlowParameterEntity>());

        FlowParameterDeleteRepository repository = new(mongoContextMock.Object);

        DeleteRequestCommandHandler requestCommand = new(repository, mockLogger.Object);

        DeleteRequestCommand command = new()
        {
            Id = _faker.Random.Guid()
        };

        var result = await requestCommand.Handle(command, CancellationToken.None);

        result.Should().BeTrue();
    }

    [Fact]
    public void Should_Throw_Exception_Update_Account()
    {
        var mockLogger = new Mock<ILogger<DeleteRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<FlowParameterEntity>());

        FlowParameterDeleteRepository repository = new(mongoContextMock.Object);

        DeleteRequestCommandHandler requestCommand = new(repository, mockLogger.Object);

        DeleteRequestCommand command = new();

        Func<Task> func = () => requestCommand.Handle(command, CancellationToken.None);
        func.Should().ThrowAsync<ArgumentException>();
    }
}