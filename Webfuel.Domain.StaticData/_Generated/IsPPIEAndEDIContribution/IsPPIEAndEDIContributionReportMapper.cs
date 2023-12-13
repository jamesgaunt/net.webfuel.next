using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMapper<IsPPIEAndEDIContribution>))]
    internal class IsPPIEAndEDIContributionReportMapper : IReportMapper<IsPPIEAndEDIContribution>
    {
        private readonly IIsPPIEAndEDIContributionRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public IsPPIEAndEDIContributionReportMapper(IIsPPIEAndEDIContributionRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.IsPPIEAndEDIContribution.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<object>> Query(Query query)
        {
            var result = await _repository.QueryIsPPIEAndEDIContribution(query);
            
            return new QueryResult<object>
            {
                TotalCount = result.TotalCount,
                Items = result.Items
            };
        }
        
        public Guid Id(object reference)
        {
            if (reference is not IsPPIEAndEDIContribution entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string DisplayName(object reference)
        {
            if (reference is not IsPPIEAndEDIContribution entity)
            throw new Exception($"Cannot get display name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
