using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMapper<SupportProvided>))]
    internal class SupportProvidedReportMapper : IReportMapper<SupportProvided>
    {
        private readonly ISupportProvidedRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public SupportProvidedReportMapper(ISupportProvidedRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.SupportProvided.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<object>> Query(Query query)
        {
            query.Contains(nameof(SupportProvided.Name), query.Search);
            
            var result = await _repository.QuerySupportProvided(query);
            
            return new QueryResult<object>
            {
                TotalCount = result.TotalCount,
                Items = result.Items
            };
        }
        
        public Guid Id(object reference)
        {
            if (reference is not SupportProvided entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string DisplayName(object reference)
        {
            if (reference is not SupportProvided entity)
            throw new Exception($"Cannot get display name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
