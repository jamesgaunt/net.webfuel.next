using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryIsPaidRSSAdviserCoapplicant: Query, IRequest<QueryResult<IsPaidRSSAdviserCoapplicant>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(IsPaidRSSAdviserCoapplicant.Name), Search);
            return this;
        }
    }
    internal class QueryIsPaidRSSAdviserCoapplicantHandler : IRequestHandler<QueryIsPaidRSSAdviserCoapplicant, QueryResult<IsPaidRSSAdviserCoapplicant>>
    {
        private readonly IIsPaidRSSAdviserCoapplicantRepository _isPaidRSSAdviserCoapplicantRepository;
        
        
        public QueryIsPaidRSSAdviserCoapplicantHandler(IIsPaidRSSAdviserCoapplicantRepository isPaidRSSAdviserCoapplicantRepository)
        {
            _isPaidRSSAdviserCoapplicantRepository = isPaidRSSAdviserCoapplicantRepository;
        }
        
        public async Task<QueryResult<IsPaidRSSAdviserCoapplicant>> Handle(QueryIsPaidRSSAdviserCoapplicant request, CancellationToken cancellationToken)
        {
            return await _isPaidRSSAdviserCoapplicantRepository.QueryIsPaidRSSAdviserCoapplicant(request.ApplyCustomFilters());
        }
    }
}

