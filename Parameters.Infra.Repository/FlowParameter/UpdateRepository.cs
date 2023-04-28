using Parameters.Domain.Repository.FlowParameter;

namespace Parameters.Infra.Repository.FlowParameter;

public class UpdateRepository : UpdateBaseRepository<FlowParameterEntity>, IUpdateRepository
{
    public UpdateRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }
}