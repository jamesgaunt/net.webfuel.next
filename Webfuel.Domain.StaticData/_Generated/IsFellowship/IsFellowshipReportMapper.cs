using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMapper<IsFellowship>))]
    internal class IsFellowshipReportMapper : IReportMapper<IsFellowship>
    {
        private readonly IIsFellowshipRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public IsFellowshipReportMapper(IIsFellowshipRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.IsFellowship.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<object>> Query(Query query)
        {
            query.Contains(nameof(IsFellowship.Name), query.Search);
            
            var result = await _repository.QueryIsFellowship(query);
            
            return new QueryResult<object>
            {
                TotalCount = result.TotalCount,
                Items = result.Items
            };
        }
        
        public Guid Id(object reference)
        {
            if (reference is not IsFellowship entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string DisplayName(object reference)
        {
            if (reference is not IsFellowship entity)
            throw new Exception($"Cannot get display name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
