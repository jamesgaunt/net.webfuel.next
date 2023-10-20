using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QuerySupportRequestStatus: Query, IRequest<QueryResult<SupportRequestStatus>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(SupportRequestStatus.Name), Search);
            return this;
        }
    }
    internal class QuerySupportRequestStatusHandler : IRequestHandler<QuerySupportRequestStatus, QueryResult<SupportRequestStatus>>
    {
        private readonly ISupportRequestStatusRepository _supportRequestStatusRepository;
        
        
        public QuerySupportRequestStatusHandler(ISupportRequestStatusRepository supportRequestStatusRepository)
        {
            _supportRequestStatusRepository = supportRequestStatusRepository;
        }
        
        public async Task<QueryResult<SupportRequestStatus>> Handle(QuerySupportRequestStatus request, CancellationToken cancellationToken)
        {
            return await _supportRequestStatusRepository.QuerySupportRequestStatus(request.ApplyCustomFilters());
        }
    }
}

