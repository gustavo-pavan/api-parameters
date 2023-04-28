using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Infra.Repository.FlowParameter;

public class DeleteRepository : DeleteBaseRepository<FlowParameterEntity>, IDeleteRepository
{
    public DeleteRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}