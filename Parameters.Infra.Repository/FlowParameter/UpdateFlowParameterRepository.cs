using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Infra.Repository.FlowParameter;

public class UpdateFlowParameterRepository : UpdateBaseRepository<FlowParameterEntity>, IUpdateFlowParameterRepository
{
    public UpdateFlowParameterRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}