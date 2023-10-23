using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryWorkActivity: Query, IRequest<QueryResult<WorkActivity>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(WorkActivity.Name), Search);
            return this;
        }
    }
    internal class QueryWorkActivityHandler : IRequestHandler<QueryWorkActivity, QueryResult<WorkActivity>>
    {
        private readonly IWorkActivityRepository _workActivityRepository;
        
        
        public QueryWorkActivityHandler(IWorkActivityRepository workActivityRepository)
        {
            _workActivityRepository = workActivityRepository;
        }
        
        public async Task<QueryResult<WorkActivity>> Handle(QueryWorkActivity request, CancellationToken cancellationToken)
        {
            return await _workActivityRepository.QueryWorkActivity(request.ApplyCustomFilters());
        }
    }
}

