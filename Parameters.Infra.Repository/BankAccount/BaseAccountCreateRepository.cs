using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Infra.Repository.BankAccount;

public class BaseAccountCreateRepository : CreateBaseRepository<BankAccountEntity>, IBaseAccountCreateRepository
{
    public BaseAccountCreateRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}