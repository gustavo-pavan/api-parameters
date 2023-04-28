using Parameters.Domain.Repository.BankAccount;

namespace Parameters.Infra.Repository.BankAccount;

public class DeleteRepository: DeleteBaseRepository<BankAccountEntity>, IDeleteRepository
{
    public DeleteRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}