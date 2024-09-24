using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QuerySubmissionStatus: Query, IRequest<QueryResult<SubmissionStatus>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(SubmissionStatus.Name), Search);
            return this;
        }
    }
    internal class QuerySubmissionStatusHandler : IRequestHandler<QuerySubmissionStatus, QueryResult<SubmissionStatus>>
    {
        private readonly ISubmissionStatusRepository _submissionStatusRepository;
        
        
        public QuerySubmissionStatusHandler(ISubmissionStatusRepository submissionStatusRepository)
        {
            _submissionStatusRepository = submissionStatusRepository;
        }
        
        public async Task<QueryResult<SubmissionStatus>> Handle(QuerySubmissionStatus request, CancellationToken cancellationToken)
        {
            return await _submissionStatusRepository.QuerySubmissionStatus(request.ApplyCustomFilters());
        }
    }
}

