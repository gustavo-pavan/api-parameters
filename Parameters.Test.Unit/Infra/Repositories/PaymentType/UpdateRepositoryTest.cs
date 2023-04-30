using Parameters.Infra.Repository.PaymentType;

namespace Parameters.Test.Unit.Infra.Repositories.PaymentType;

public class UpdateRepositoryTest
{
    private readonly Faker _faker = new();

    [Fact]
    public void Should_Throw_Exception_When_Id_Is_Invalid_In_Update_Account()
    {
        PaymentTypeEntity paymentType = new(_faker.Name.FullName(), _faker.Random.AlphaNumeric(400));

        var mongoContextMock = MongoContextMock.Mock(new List<PaymentTypeEntity> { paymentType });

        UpdatePaymentTypeRepository repository = new(mongoContextMock.Object);

        var func = async () => await repository.Execute(paymentType);

        func.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"Can't update because {nameof(BaseEntity.Id)} is not valid!");
    }
}