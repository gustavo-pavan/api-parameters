using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Infra.Repository.BankAccount;

public class UpdateRepository : UpdateBaseRepository<BankAccountEntity>, IUpdateRepository
{
    public UpdateRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}