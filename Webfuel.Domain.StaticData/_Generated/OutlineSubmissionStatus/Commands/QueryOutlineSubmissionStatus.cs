using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryOutlineSubmissionStatus: Query, IRequest<QueryResult<OutlineSubmissionStatus>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(OutlineSubmissionStatus.Name), Search);
            return this;
        }
    }
    internal class QueryOutlineSubmissionStatusHandler : IRequestHandler<QueryOutlineSubmissionStatus, QueryResult<OutlineSubmissionStatus>>
    {
        private readonly IOutlineSubmissionStatusRepository _outlineSubmissionStatusRepository;
        
        
        public QueryOutlineSubmissionStatusHandler(IOutlineSubmissionStatusRepository outlineSubmissionStatusRepository)
        {
            _outlineSubmissionStatusRepository = outlineSubmissionStatusRepository;
        }
        
        public async Task<QueryResult<OutlineSubmissionStatus>> Handle(QueryOutlineSubmissionStatus request, CancellationToken cancellationToken)
        {
            return await _outlineSubmissionStatusRepository.QueryOutlineSubmissionStatus(request.ApplyCustomFilters());
        }
    }
}

