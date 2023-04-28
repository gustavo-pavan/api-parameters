using Parameters.Domain.Entity.Enums;
using Parameters.Infra.Repository.FlowParameter;

namespace Parameters.Test.Unit.Infra.Repositories.FlowParameter;

public class CreateRepositoryTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Create_Account_And_Generate_Id()
    {
        FlowParameterEntity flowParameter = new(_faker.Name.FullName(), FlowEnumeration.FromValue<FlowType>(_faker.Random.Int(1,2)), _faker.Random.AlphaNumeric(400));

        var mongoContextMock = MongoContextMock.Mock(new List<FlowParameterEntity> { flowParameter });

        CreateRepository repository = new(mongoContextMock.Object);

        await repository.Execute(flowParameter);
        flowParameter.Id.Should().Be(flowParameter.Id);
    }

    [Fact]
    public void Should_Throw_Exception_When_Id_Is_Invalid_In_Create_Account()
    {
        FlowParameterEntity account = new(_faker.Random.Guid(), _faker.Name.FullName(), FlowEnumeration.FromValue<FlowType>(_faker.Random.Int(1, 2)),_faker.Random.AlphaNumeric(400));

        var mongoContextMock = MongoContextMock.Mock(new List<FlowParameterEntity> { account });

        CreateRepository repository = new(mongoContextMock.Object);

        var func = async () => await repository.Execute(account);

        func.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"Can't create because {nameof(BaseEntity.Id)} is not valid!");
    }
}