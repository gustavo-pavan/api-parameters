using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Infra.Repository.BankAccount;

public class DeleteBankAccountRepository : DeleteBaseRepository<BankAccountEntity>, IDeleteBankAccountRepository
{
    public DeleteBankAccountRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}