using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Infra.Repository.BankAccount;

public class CreateRepository : CreateBaseRepository<BankAccountEntity>, ICreateRepository
{
    public CreateRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}