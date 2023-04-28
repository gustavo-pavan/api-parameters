using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Infra.Repository.BankAccount;

public class BaseAccountGetRepository : GetBaseRepository<BankAccountEntity>, IBaseAccountGetRepository
{
    public BaseAccountGetRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}