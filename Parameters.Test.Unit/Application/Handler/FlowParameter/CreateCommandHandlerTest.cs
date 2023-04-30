using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Application.Request.Handler.FlowParameter;
using Parameters.Infra.Repository.FlowParameter;

namespace Parameters.Test.Unit.Application.Handler.FlowParameter;

public class CreateCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Create_Account()
    {
        var mockLogger = new Mock<ILogger<CreateFlowParameterRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<FlowParameterEntity>());

        CreateFlowParameterRepository repository = new(mongoContextMock.Object);

        CreateFlowParameterRequestCommandHandler flowParameterRequestCommand = new(repository, mockLogger.Object);

        CreateFlowParameterRequestCommand command = new()
        {
            FlowType = _faker.Random.Int(1, 2),
            Description = _faker.Random.AlphaNumeric(400),
            Name = _faker.Name.FullName()
        };

        var result = await flowParameterRequestCommand.Handle(command, CancellationToken.None);

        result.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Should_Throw_Exception_Create_Account()
    {
        var mockLogger = new Mock<ILogger<CreateFlowParameterRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<FlowParameterEntity>());

        CreateFlowParameterRepository repository = new(mongoContextMock.Object);

        CreateFlowParameterRequestCommandHandler flowParameterRequestCommand = new(repository, mockLogger.Object);

        CreateFlowParameterRequestCommand command = new()
        {
            FlowType = _faker.Random.Int(1, 2),
            Description = _faker.Random.AlphaNumeric(400),
            Name = _faker.Name.FullName()
        };

        Func<Task> func = () => flowParameterRequestCommand.Handle(command, CancellationToken.None);
        func.Should().ThrowAsync<ArgumentException>();
    }
}