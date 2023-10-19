using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QuerySuportRequestStatus: Query, IRequest<QueryResult<SuportRequestStatus>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(SuportRequestStatus.Name), Search);
            return this;
        }
    }
    internal class QuerySuportRequestStatusHandler : IRequestHandler<QuerySuportRequestStatus, QueryResult<SuportRequestStatus>>
    {
        private readonly ISuportRequestStatusRepository _suportRequestStatusRepository;
        
        
        public QuerySuportRequestStatusHandler(ISuportRequestStatusRepository suportRequestStatusRepository)
        {
            _suportRequestStatusRepository = suportRequestStatusRepository;
        }
        
        public async Task<QueryResult<SuportRequestStatus>> Handle(QuerySuportRequestStatus request, CancellationToken cancellationToken)
        {
            return await _suportRequestStatusRepository.QuerySuportRequestStatus(request.ApplyCustomFilters());
        }
    }
}

