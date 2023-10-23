using MediatR;

namespace Webfuel.Domain
{

    internal class QueryProjectHandler : IRequestHandler<QueryProject, QueryResult<Project>>
    {
        private readonly IProjectRepository _projectRepository;

        public QueryProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<QueryResult<Project>> Handle(QueryProject request, CancellationToken cancellationToken)
        {
            return await _projectRepository.QueryProject(request.ApplyCustomFilters());
        }
    }
}
