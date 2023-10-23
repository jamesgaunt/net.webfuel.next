using MediatR;

namespace Webfuel.Domain
{

    internal class QueryProjectSupportHandler : IRequestHandler<QueryProjectSupport, QueryResult<ProjectSupport>>
    {
        private readonly IProjectSupportRepository _projectSupportRepository;

        public QueryProjectSupportHandler(IProjectSupportRepository projectSupportRepository)
        {
            _projectSupportRepository = projectSupportRepository;
        }

        public async Task<QueryResult<ProjectSupport>> Handle(QueryProjectSupport request, CancellationToken cancellationToken)
        {
            return await _projectSupportRepository.QueryProjectSupport(request.ApplyCustomFilters());
        }
    }
}
