using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Infra.Repository.BankAccount;

public class GetBankAccountRepository : GetBaseRepository<BankAccountEntity>, IGetBankAccountRepository
{
    public GetBankAccountRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}