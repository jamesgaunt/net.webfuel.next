using MediatR;

namespace Webfuel.Domain
{

    internal class QueryProjectTeamSupportHandler : IRequestHandler<QueryProjectTeamSupport, QueryResult<ProjectTeamSupport>>
    {
        private readonly IProjectTeamSupportRepository _projectTeamSupportRepository;

        public QueryProjectTeamSupportHandler(IProjectTeamSupportRepository projectTeamSupportRepository)
        {
            _projectTeamSupportRepository = projectTeamSupportRepository;
        }

        public async Task<QueryResult<ProjectTeamSupport>> Handle(QueryProjectTeamSupport request, CancellationToken cancellationToken)
        {
            return await _projectTeamSupportRepository.QueryProjectTeamSupport(request.ApplyCustomFilters());
        }
    }
}
