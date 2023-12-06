using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    public interface ISiteReferenceProvider: IReportReferenceProvider
    {
    }
    
    [Service(typeof(ISiteReferenceProvider))]
    internal class SiteReferenceProvider : ISiteReferenceProvider
    {
        private readonly ISiteRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public SiteReferenceProvider(ISiteRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ReportReference?> GetReportReference(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            var item = staticData.Site.FirstOrDefault(x => x.Id == id);
            if (item == null)
            return null;
            
            return new ReportReference
            {
                Id = item.Id,
                Name = item.Name,
            };
        }
        
        public async Task<QueryResult<ReportReference>> QueryReportReference(Query query)
        {
            var result = await _repository.QuerySite(query);
            
            return new QueryResult<ReportReference>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.Select(x => new ReportReference
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }).ToList(),
            };
        }
    }
}
