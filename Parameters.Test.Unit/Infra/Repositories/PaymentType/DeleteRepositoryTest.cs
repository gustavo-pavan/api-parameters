using Parameters.Infra.Repository.PaymentType;

namespace Parameters.Test.Unit.Infra.Repositories.PaymentType;

public class DeleteRepositoryTest
{
    [Fact]
    public void Should_Throw_Exception_When_Id_Is_Invalid_In_Delete_Account()
    {
        var mongoContextMock = MongoContextMock.Mock(new List<PaymentTypeEntity>());

        DeletePaymentTypeRepository repository = new(mongoContextMock.Object);

        var func = async () => await repository.Execute(Guid.Empty);

        func.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"Can't delete because {nameof(BaseEntity.Id)} is not valid!");
    }
}