using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Infra.Repository.BankAccount;

public class GetRepository : GetBaseRepository<BankAccountEntity>, IGetRepository
{
    public GetRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}