using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Infra.Repository.BankAccount;

public class BaseAccountUpdateRepository : UpdateBaseRepository<BankAccountEntity>, IBaseAccountUpdateRepository
{
    public BaseAccountUpdateRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}