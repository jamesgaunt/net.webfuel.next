using MediatR;

namespace Webfuel.Domain
{
    public class QueryProjectSupport : Query, IRequest<QueryResult<ProjectSupport>>
    {
        public required Guid ProjectId { get; set; }

        public bool OpenTeamSupportOnly { get; set; }

        public Query ApplyCustomFilters()
        {
            this.Equal(nameof(ProjectSupport.ProjectId), ProjectId);

            if(OpenTeamSupportOnly)
                this.SQL($"e.[SupportRequestedTeamId] IS NOT NULL AND e.[SupportRequestedCompletedAt] IS NULL");

            return this;
        }
    }

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
