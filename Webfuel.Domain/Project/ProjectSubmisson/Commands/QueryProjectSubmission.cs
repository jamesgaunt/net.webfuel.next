using MediatR;

namespace Webfuel.Domain
{
    public class QueryProjectSubmission : Query, IRequest<QueryResult<ProjectSubmission>>
    {
        public required Guid ProjectId { get; set; }

        public Query ApplyCustomFilters()
        {
            this.Equal(nameof(ProjectSubmission.ProjectId), ProjectId);
            return this;
        }
    }

    internal class QueryProjectSubmissionHandler : IRequestHandler<QueryProjectSubmission, QueryResult<ProjectSubmission>>
    {
        private readonly IProjectSubmissionRepository _projectSubmissionRepository;

        public QueryProjectSubmissionHandler(IProjectSubmissionRepository projectSubmissionRepository)
        {
            _projectSubmissionRepository = projectSubmissionRepository;
        }

        public async Task<QueryResult<ProjectSubmission>> Handle(QueryProjectSubmission request, CancellationToken cancellationToken)
        {
            return await _projectSubmissionRepository.QueryProjectSubmission(request.ApplyCustomFilters());
        }
    }
}
