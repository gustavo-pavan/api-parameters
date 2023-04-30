using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Infra.Repository.BankAccount;

public class UpdateBankAccountRepository : UpdateBaseRepository<BankAccountEntity>, IUpdateBankAccountRepository
{
    public UpdateBankAccountRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}