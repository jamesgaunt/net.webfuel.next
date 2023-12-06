using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    public interface IResearchMethodologyReferenceProvider: IReportReferenceProvider
    {
    }
    
    [Service(typeof(IResearchMethodologyReferenceProvider))]
    internal class ResearchMethodologyReferenceProvider : IResearchMethodologyReferenceProvider
    {
        private readonly IResearchMethodologyRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public ResearchMethodologyReferenceProvider(IResearchMethodologyRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ReportReference?> GetReportReference(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            var item = staticData.ResearchMethodology.FirstOrDefault(x => x.Id == id);
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
            var result = await _repository.QueryResearchMethodology(query);
            
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
