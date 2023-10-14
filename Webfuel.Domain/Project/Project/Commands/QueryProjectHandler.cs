using MediatR;

namespace Webfuel.Domain
{

    internal class QueryProjectHandler : IRequestHandler<QueryProject, QueryResult<Project>>
    {
        private readonly IProjectRepository _userRepository;

        public QueryProjectHandler(IProjectRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<QueryResult<Project>> Handle(QueryProject request, CancellationToken cancellationToken)
        {
            return await _userRepository.QueryProject(request.ApplyCustomFilters());
        }
    }
}
