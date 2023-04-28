using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Application.Request.Handler.FlowParameter;
using Parameters.Infra.Repository.FlowParameter;

namespace Parameters.Test.Unit.Application.Handler.FlowParameter;

public class UpdateCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Update_Account()
    {
        var mockLogger = new Mock<ILogger<UpdateFlowParameterCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<FlowParameterEntity>());

        FlowParameterUpdateRepository repository = new(mongoContextMock.Object);

        UpdateFlowParameterCommandHandler flowParameterCommand = new(repository, mockLogger.Object);

        UpdateFlowParameterUpdateRequestCommand command = new()
        {
            Id = _faker.Random.Guid(),
            FlowType = _faker.Random.Int(1, 2),
            Description = _faker.Random.AlphaNumeric(400),
            Name = _faker.Name.FullName()
        };

        var result = await flowParameterCommand.Handle(command, CancellationToken.None);

        result.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Should_Throw_Exception_Update_Account()
    {
        var mockLogger = new Mock<ILogger<UpdateFlowParameterCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<FlowParameterEntity>());

        FlowParameterUpdateRepository repository = new(mongoContextMock.Object);

        UpdateFlowParameterCommandHandler flowParameterCommand = new(repository, mockLogger.Object);

        UpdateFlowParameterUpdateRequestCommand command = new()
        {
            FlowType = _faker.Random.Int(1, 2),
            Description = _faker.Random.AlphaNumeric(400),
            Name = _faker.Name.FullName()
        };

        Func<Task> func = () => flowParameterCommand.Handle(command, CancellationToken.None);
        func.Should().ThrowAsync<ArgumentException>();
    }
}