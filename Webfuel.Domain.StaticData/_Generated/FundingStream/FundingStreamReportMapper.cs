using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMapper<FundingStream>))]
    internal class FundingStreamReportMapper : IReportMapper<FundingStream>
    {
        private readonly IFundingStreamRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public FundingStreamReportMapper(IFundingStreamRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.FundingStream.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<object>> Query(Query query)
        {
            var result = await _repository.QueryFundingStream(query);
            
            return new QueryResult<object>
            {
                TotalCount = result.TotalCount,
                Items = result.Items
            };
        }
        
        public Guid Id(object reference)
        {
            if (reference is not FundingStream entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string DisplayName(object reference)
        {
            if (reference is not FundingStream entity)
            throw new Exception($"Cannot get display name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
