using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryFullSubmissionStatus: Query, IRequest<QueryResult<FullSubmissionStatus>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(FullSubmissionStatus.Name), Search);
            return this;
        }
    }
    internal class QueryFullSubmissionStatusHandler : IRequestHandler<QueryFullSubmissionStatus, QueryResult<FullSubmissionStatus>>
    {
        private readonly IFullSubmissionStatusRepository _fullSubmissionStatusRepository;
        
        
        public QueryFullSubmissionStatusHandler(IFullSubmissionStatusRepository fullSubmissionStatusRepository)
        {
            _fullSubmissionStatusRepository = fullSubmissionStatusRepository;
        }
        
        public async Task<QueryResult<FullSubmissionStatus>> Handle(QueryFullSubmissionStatus request, CancellationToken cancellationToken)
        {
            return await _fullSubmissionStatusRepository.QueryFullSubmissionStatus(request.ApplyCustomFilters());
        }
    }
}

