using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryIsPaidRSSAdviserLead: Query, IRequest<QueryResult<IsPaidRSSAdviserLead>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(IsPaidRSSAdviserLead.Name), Search);
            return this;
        }
    }
    internal class QueryIsPaidRSSAdviserLeadHandler : IRequestHandler<QueryIsPaidRSSAdviserLead, QueryResult<IsPaidRSSAdviserLead>>
    {
        private readonly IIsPaidRSSAdviserLeadRepository _isPaidRSSAdviserLeadRepository;
        
        
        public QueryIsPaidRSSAdviserLeadHandler(IIsPaidRSSAdviserLeadRepository isPaidRSSAdviserLeadRepository)
        {
            _isPaidRSSAdviserLeadRepository = isPaidRSSAdviserLeadRepository;
        }
        
        public async Task<QueryResult<IsPaidRSSAdviserLead>> Handle(QueryIsPaidRSSAdviserLead request, CancellationToken cancellationToken)
        {
            return await _isPaidRSSAdviserLeadRepository.QueryIsPaidRSSAdviserLead(request.ApplyCustomFilters());
        }
    }
}

