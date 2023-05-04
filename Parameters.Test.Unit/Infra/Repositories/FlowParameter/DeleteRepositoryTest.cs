using Parameters.Infra.Repository.FlowParameter;

namespace Parameters.Test.Unit.Infra.Repositories.FlowParameter;

public class DeleteRepositoryTest
{
    [Fact]
    public void Should_Throw_Exception_When_Id_Is_Invalid_In_Delete_Account()
    {
        //Test
        var mongoContextMock = MongoContextMock.Mock(new List<FlowParameterEntity>());

        DeleteFlowParameterRepository repository = new(mongoContextMock.Object);

        var func = async () => await repository.Execute(new FlowParameterEntity(Guid.Empty, null, null, null));

        func.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"Can't delete because {nameof(BaseEntity.Id)} is not valid!");
    }
}