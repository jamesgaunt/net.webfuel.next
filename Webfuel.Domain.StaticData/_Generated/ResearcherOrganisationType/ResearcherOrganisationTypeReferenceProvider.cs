using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    public interface IResearcherOrganisationTypeReferenceProvider: IReportReferenceProvider
    {
    }
    
    [Service(typeof(IResearcherOrganisationTypeReferenceProvider))]
    internal class ResearcherOrganisationTypeReferenceProvider : IResearcherOrganisationTypeReferenceProvider
    {
        private readonly IResearcherOrganisationTypeRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public ResearcherOrganisationTypeReferenceProvider(IResearcherOrganisationTypeRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ReportReference?> GetReportReference(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            var item = staticData.ResearcherOrganisationType.FirstOrDefault(x => x.Id == id);
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
            var result = await _repository.QueryResearcherOrganisationType(query);
            
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
