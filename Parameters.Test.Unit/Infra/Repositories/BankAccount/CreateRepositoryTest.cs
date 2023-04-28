﻿using Parameters.Infra.Repository.BankAccount;

namespace Parameters.Test.Unit.Infra.Repositories.BankAccount;

public class CreateRepositoryTest
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Create_Account_And_Generate_Id()
    {
        BankAccountEntity account = new(_faker.Name.FullName(), _faker.Random.Decimal(0, 100),
            _faker.Random.AlphaNumeric(400));

        var mongoContextMock = MongoContextMock.Mock(new List<BankAccountEntity> { account });

        CreateRepository repository = new(mongoContextMock.Object);

        await repository.Execute(account);
        account.Id.Should().Be(account.Id);
    }

    [Fact]
    public void Should_Throw_Exception_When_Id_Is_Invalid_In_Create_Account()
    {
        BankAccountEntity account = new(_faker.Random.Guid(), _faker.Name.FullName(), _faker.Random.Decimal(0, 100),
            _faker.Random.AlphaNumeric(400));

        var mongoContextMock = MongoContextMock.Mock(new List<BankAccountEntity> { account });

        CreateRepository repository = new(mongoContextMock.Object);

        var func = async () => await repository.Execute(account);

        func.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"Can't create because {nameof(BaseEntity.Id)} is not valid!");
    }
}