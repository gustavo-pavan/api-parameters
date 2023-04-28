using Parameters.Infra.Repository.PaymentType;

namespace Parameters.Test.Unit.Infra.Repositories.PaymentType;

public class CreateRepositoryTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Create_Account_And_Generate_Id()
    {
        PaymentTypeEntity paymentType = new(_faker.Name.FullName(), _faker.Random.AlphaNumeric(400));

        var mongoContextMock = MongoContextMock.Mock(new List<PaymentTypeEntity> { paymentType });

        CreateRepository repository = new(mongoContextMock.Object);

        await repository.Execute(paymentType);
        paymentType.Id.Should().Be(paymentType.Id);
    }

    [Fact]
    public void Should_Throw_Exception_When_Id_Is_Invalid_In_Create_Account()
    {
        PaymentTypeEntity paymentType = new(_faker.Random.Guid(), _faker.Name.FullName(), _faker.Random.AlphaNumeric(400));

        var mongoContextMock = MongoContextMock.Mock(new List<PaymentTypeEntity> { paymentType });

        CreateRepository repository = new(mongoContextMock.Object);

        var func = async () => await repository.Execute(paymentType);

        func.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"Can't create because {nameof(BaseEntity.Id)} is not valid!");
    }
}