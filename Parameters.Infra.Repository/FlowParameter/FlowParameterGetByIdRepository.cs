using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Infra.Repository.FlowParameter;

public class FlowParameterGetByIdRepository : GetByIdBaseRepository<FlowParameterEntity>,
    IFlowParameterGetByIdRepository
{
    public FlowParameterGetByIdRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}