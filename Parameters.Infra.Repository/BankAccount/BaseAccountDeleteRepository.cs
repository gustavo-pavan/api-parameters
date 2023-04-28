using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Infra.Repository.BankAccount;

public class BaseAccountDeleteRepository : DeleteBaseRepository<BankAccountEntity>, IBaseAccountDeleteRepository
{
    public BaseAccountDeleteRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}