using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class QueryFundingCallType: Query, IRequest<QueryResult<FundingCallType>>
    {
        
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(FundingCallType.Name), Search);
            return this;
        }
    }
    internal class QueryFundingCallTypeHandler : IRequestHandler<QueryFundingCallType, QueryResult<FundingCallType>>
    {
        private readonly IFundingCallTypeRepository _fundingCallTypeRepository;
        
        
        public QueryFundingCallTypeHandler(IFundingCallTypeRepository fundingCallTypeRepository)
        {
            _fundingCallTypeRepository = fundingCallTypeRepository;
        }
        
        public async Task<QueryResult<FundingCallType>> Handle(QueryFundingCallType request, CancellationToken cancellationToken)
        {
            return await _fundingCallTypeRepository.QueryFundingCallType(request.ApplyCustomFilters());
        }
    }
}

