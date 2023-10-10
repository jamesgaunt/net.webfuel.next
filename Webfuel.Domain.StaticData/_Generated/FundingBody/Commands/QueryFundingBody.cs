using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryFundingBody: Query, IRequest<QueryResult<FundingBody>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(FundingBody.Name), Search);
            return this;
        }
    }
    internal class QueryFundingBodyHandler : IRequestHandler<QueryFundingBody, QueryResult<FundingBody>>
    {
        private readonly IFundingBodyRepository _fundingBodyRepository;
        
        
        public QueryFundingBodyHandler(IFundingBodyRepository fundingBodyRepository)
        {
            _fundingBodyRepository = fundingBodyRepository;
        }
        
        public async Task<QueryResult<FundingBody>> Handle(QueryFundingBody request, CancellationToken cancellationToken)
        {
            return await _fundingBodyRepository.QueryFundingBody(request.ApplyCustomFilters());
        }
    }
}

