using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMapper<FundingCallType>))]
    internal class FundingCallTypeReportMapper : IReportMapper<FundingCallType>
    {
        private readonly IFundingCallTypeRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public FundingCallTypeReportMapper(IFundingCallTypeRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.FundingCallType.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<object>> Query(Query query)
        {
            query.Contains(nameof(FundingCallType.Name), query.Search);
            
            var result = await _repository.QueryFundingCallType(query);
            
            return new QueryResult<object>
            {
                TotalCount = result.TotalCount,
                Items = result.Items
            };
        }
        
        public Guid Id(object reference)
        {
            if (reference is not FundingCallType entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string DisplayName(object reference)
        {
            if (reference is not FundingCallType entity)
            throw new Exception($"Cannot get display name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
