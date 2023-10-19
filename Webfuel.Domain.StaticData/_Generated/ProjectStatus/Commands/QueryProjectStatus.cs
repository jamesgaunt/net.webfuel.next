using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryProjectStatus: Query, IRequest<QueryResult<ProjectStatus>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(ProjectStatus.Name), Search);
            return this;
        }
    }
    internal class QueryProjectStatusHandler : IRequestHandler<QueryProjectStatus, QueryResult<ProjectStatus>>
    {
        private readonly IProjectStatusRepository _projectStatusRepository;
        
        
        public QueryProjectStatusHandler(IProjectStatusRepository projectStatusRepository)
        {
            _projectStatusRepository = projectStatusRepository;
        }
        
        public async Task<QueryResult<ProjectStatus>> Handle(QueryProjectStatus request, CancellationToken cancellationToken)
        {
            return await _projectStatusRepository.QueryProjectStatus(request.ApplyCustomFilters());
        }
    }
}

