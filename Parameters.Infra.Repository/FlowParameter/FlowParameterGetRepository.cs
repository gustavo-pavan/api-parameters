using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Infra.Repository.FlowParameter;

public class FlowParameterGetRepository : GetBaseRepository<FlowParameterEntity>, IFlowParameterGetRepository
{
    public FlowParameterGetRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}