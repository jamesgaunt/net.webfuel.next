using MediatR;

namespace Webfuel.Domain
{
    internal class QueryProjectChangeLogHandler : IRequestHandler<QueryProjectChangeLog, QueryResult<ProjectChangeLog>>
    {
        private readonly IProjectChangeLogRepository _projectChangeLogRepository;

        public QueryProjectChangeLogHandler(IProjectChangeLogRepository projectChangeLogRepository)
        {
            _projectChangeLogRepository = projectChangeLogRepository;
        }

        public async Task<QueryResult<ProjectChangeLog>> Handle(QueryProjectChangeLog request, CancellationToken cancellationToken)
        {
            return await _projectChangeLogRepository.QueryProjectChangeLog(request.ApplyCustomFilters());
        }
    }
}
