using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Infra.Repository.FlowParameter;

public class GetRepository : GetBaseRepository<FlowParameterEntity>, IGetRepository
{
    public GetRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}