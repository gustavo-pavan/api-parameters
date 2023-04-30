using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Infra.Repository.FlowParameter;

public class DeleteFlowParameterRepository : DeleteBaseRepository<FlowParameterEntity>, IDeleteFlowParameterRepository
{
    public DeleteFlowParameterRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}