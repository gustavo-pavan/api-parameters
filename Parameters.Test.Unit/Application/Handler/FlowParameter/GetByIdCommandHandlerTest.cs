using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Application.Request.Handler.FlowParameter;
using Parameters.Domain.Entity.Enums;
using Parameters.Infra.Repository.FlowParameter;

namespace Parameters.Test.Unit.Application.Handler.FlowParameter;

public class GetByIdCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Get_All_Account()
    {
        var mockLogger = new Mock<ILogger<GetByIdRequestCommandHandler>>();
        FlowParameterEntity flowParameter = new(Guid.NewGuid(), _faker.Name.FullName(),
            FlowEnumeration.FromValue<FlowType>(_faker.Random.Int(1, 2)),
            _faker.Random.AlphaNumeric(400));
        var mongoContextMock = MongoContextMock.Mock(new List<FlowParameterEntity> { flowParameter });

        FlowParameterGetByIdRepository repository = new(mongoContextMock.Object);

        GetByIdRequestCommandHandler requestCommand = new(repository, mockLogger.Object);

        var result =
            await requestCommand.Handle(new GetByIdRequestCommand { Id = Guid.NewGuid() }, CancellationToken.None);

        result?.Should().NotBeNull();
        result?.Should().Be(flowParameter);
    }
}