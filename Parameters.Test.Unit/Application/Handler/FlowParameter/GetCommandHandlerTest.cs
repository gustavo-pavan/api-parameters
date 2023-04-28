using Parameters.Application.Request.Command.FlowParameter;
using Parameters.Application.Request.Handler.FlowParameter;
using Parameters.Domain.Entity.Enums;
using Parameters.Infra.Repository.FlowParameter;

namespace Parameters.Test.Unit.Application.Handler.FlowParameter;

public class GetCommandHandlerTest
{
    private readonly Faker _faker = new();

    [Fact]
    public void Should_Get_All_Account()
    {
        var mockLogger = new Mock<ILogger<GetRequestCommandHandler>>();

        var mongoContextMock = MongoContextMock.Mock(new List<FlowParameterEntity>
        {
            new(Guid.NewGuid(), _faker.Name.FullName(), FlowEnumeration.FromValue<FlowType>(_faker.Random.Int(1,2)), _faker.Random.AlphaNumeric(400))
        });

        GetRepository repository = new(mongoContextMock.Object);

        GetRequestCommandHandler requestCommand = new(repository, mockLogger.Object);

        var result = requestCommand.Handle(new GetRequestCommand(), CancellationToken.None).Result?.ToArray();

        result?.Should().NotBeNull();
        result?.Count().Should().Be(1);
    }
}