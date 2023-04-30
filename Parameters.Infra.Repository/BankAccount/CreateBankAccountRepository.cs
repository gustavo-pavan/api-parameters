using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Infra.Repository.BankAccount;

public class CreateBankAccountRepository : CreateBaseRepository<BankAccountEntity>, ICreateBankAccountRepository
{
    public CreateBankAccountRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}