using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Infra.Repository.BankAccount;

public class BaseAccountGetByIdRepository : GetByIdBaseRepository<BankAccountEntity>, IBaseAccountGetByIdRepository
{
    public BaseAccountGetByIdRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}