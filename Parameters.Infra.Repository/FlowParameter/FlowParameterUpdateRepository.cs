using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Infra.Repository.FlowParameter;

public class FlowParameterUpdateRepository : UpdateBaseRepository<FlowParameterEntity>, IFlowParameterUpdateRepository
{
    public FlowParameterUpdateRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}