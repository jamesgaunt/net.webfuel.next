using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMapper<AgeRange>))]
    internal class AgeRangeReportMapper : IReportMapper<AgeRange>
    {
        private readonly IAgeRangeRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public AgeRangeReportMapper(IAgeRangeRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.AgeRange.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<object>> Query(Query query)
        {
            query.Contains(nameof(AgeRange.Name), query.Search);
            
            var result = await _repository.QueryAgeRange(query);
            
            return new QueryResult<object>
            {
                TotalCount = result.TotalCount,
                Items = result.Items
            };
        }
        
        public Guid Id(object reference)
        {
            if (reference is not AgeRange entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string DisplayName(object reference)
        {
            if (reference is not AgeRange entity)
            throw new Exception($"Cannot get display name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
