using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Infra.Repository.BankAccount;

public class GetByIdBankAccountRepository : GetByIdBaseRepository<BankAccountEntity>, IGetByIdBankAccountRepository
{
    public GetByIdBankAccountRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}