using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    public interface IHowDidYouFindUsReferenceProvider: IReportReferenceProvider
    {
    }
    
    [Service(typeof(IHowDidYouFindUsReferenceProvider))]
    internal class HowDidYouFindUsReferenceProvider : IHowDidYouFindUsReferenceProvider
    {
        private readonly IHowDidYouFindUsRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public HowDidYouFindUsReferenceProvider(IHowDidYouFindUsRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ReportReference?> GetReportReference(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            var item = staticData.HowDidYouFindUs.FirstOrDefault(x => x.Id == id);
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
            var result = await _repository.QueryHowDidYouFindUs(query);
            
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
