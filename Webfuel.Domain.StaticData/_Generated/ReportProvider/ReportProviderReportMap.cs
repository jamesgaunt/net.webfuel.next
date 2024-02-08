using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMap<ReportProvider>))]
    internal class ReportProviderReportMap : IReportMap<ReportProvider>
    {
        private readonly IReportProviderRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public ReportProviderReportMap(IReportProviderRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.ReportProvider.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<ReportMapEntity>> Query(Query query)
        {
            query.Contains(nameof(ReportProvider.Name), query.Search);
            
            var result = await _repository.QueryReportProvider(query);
            
            return new QueryResult<ReportMapEntity>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.Select(p => new ReportMapEntity
                    {
                        Id = p.Id,
                        Name = p.Name
                    }).ToList()
            };
        }
        
        public Guid Id(object reference)
        {
            if (reference is not ReportProvider entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string Name(object reference)
        {
            if (reference is not ReportProvider entity)
            throw new Exception($"Cannot get name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
