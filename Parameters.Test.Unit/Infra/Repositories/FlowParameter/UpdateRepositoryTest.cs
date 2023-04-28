using Parameters.Domain.Entity.Enums;
using Parameters.Infra.Repository.FlowParameter;

namespace Parameters.Test.Unit.Infra.Repositories.FlowParameter;

public class UpdateRepositoryTest
{
    private readonly Faker _faker = new();

    [Fact]
    public void Should_Throw_Exception_When_Id_Is_Invalid_In_Update_Account()
    {
        FlowParameterEntity flowParameter = new(_faker.Name.FullName(),
            FlowEnumeration.FromValue<FlowType>(_faker.Random.Int(1, 2)), _faker.Random.AlphaNumeric(400));

        var mongoContextMock = MongoContextMock.Mock(new List<FlowParameterEntity> { flowParameter });

        UpdateRepository repository = new(mongoContextMock.Object);

        var func = async () => await repository.Execute(flowParameter);

        func.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"Can't update because {nameof(BaseEntity.Id)} is not valid!");
    }
}