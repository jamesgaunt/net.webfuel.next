using MediatR;

namespace Webfuel.Domain
{

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
