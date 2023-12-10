using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    public interface IResearcherRoleReferenceProvider: IReportReferenceProvider
    {
    }
    
    [Service(typeof(IResearcherRoleReferenceProvider))]
    internal class ResearcherRoleReferenceProvider : IResearcherRoleReferenceProvider
    {
        private readonly IResearcherRoleRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public ResearcherRoleReferenceProvider(IResearcherRoleRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ReportReference?> GetReportReference(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            var item = staticData.ResearcherRole.FirstOrDefault(x => x.Id == id);
            if (item == null)
            return null;
            
            return new ReportReference
            {
                Id = item.Id,
                Name = item.Name,
                Entity = item,
            };
        }
        
        public async Task<QueryResult<ReportReference>> QueryReportReference(Query query)
        {
            var result = await _repository.QueryResearcherRole(query);
            
            return new QueryResult<ReportReference>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.Select(x => new ReportReference
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Entity = x,
                    }).ToList(),
            };
        }
    }
}
