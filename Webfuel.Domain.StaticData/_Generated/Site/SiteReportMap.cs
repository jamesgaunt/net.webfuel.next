using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMap<Site>))]
    internal class SiteReportMap : IReportMap<Site>
    {
        private readonly ISiteRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public SiteReportMap(ISiteRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.Site.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<ReferenceLookup>> Query(Query query)
        {
            query.Contains(nameof(Site.Name), query.Search);
            
            var result = await _repository.QuerySite(query);
            
            return new QueryResult<ReferenceLookup>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.Select(p => new ReferenceLookup
                    {
                        Id = p.Id,
                        Name = p.Name
                    }).ToList()
            };
        }
        
        public Guid Id(object reference)
        {
            if (reference is not Site entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string Name(object reference)
        {
            if (reference is not Site entity)
            throw new Exception($"Cannot get name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
