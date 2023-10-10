using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryFundingStream: Query, IRequest<QueryResult<FundingStream>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(FundingStream.Name), Search);
            return this;
        }
    }
    internal class QueryFundingStreamHandler : IRequestHandler<QueryFundingStream, QueryResult<FundingStream>>
    {
        private readonly IFundingStreamRepository _fundingStreamRepository;
        
        
        public QueryFundingStreamHandler(IFundingStreamRepository fundingStreamRepository)
        {
            _fundingStreamRepository = fundingStreamRepository;
        }
        
        public async Task<QueryResult<FundingStream>> Handle(QueryFundingStream request, CancellationToken cancellationToken)
        {
            return await _fundingStreamRepository.QueryFundingStream(request.ApplyCustomFilters());
        }
    }
}

