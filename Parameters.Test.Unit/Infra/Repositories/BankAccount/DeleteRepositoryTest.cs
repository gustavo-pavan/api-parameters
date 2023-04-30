using Parameters.Infra.Repository.BankAccount;

namespace Parameters.Test.Unit.Infra.Repositories.BankAccount;

public class DeleteRepositoryTest
{
    [Fact]
    public void Should_Throw_Exception_When_Id_Is_Invalid_In_Delete_Account()
    {
        var mongoContextMock = MongoContextMock.Mock(new List<BankAccountEntity>());

        DeleteBankAccountRepository repository = new(mongoContextMock.Object);

        var func = async () => await repository.Execute(new BankAccountEntity(Guid.Empty, "", 0, ""));

        func.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"Can't delete because {nameof(BaseEntity.Id)} is not valid!");
    }
}