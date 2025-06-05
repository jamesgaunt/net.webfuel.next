using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMap<ResearcherProfessionalBackground>))]
    internal class ResearcherProfessionalBackgroundReportMap : IReportMap<ResearcherProfessionalBackground>
    {
        private readonly IResearcherProfessionalBackgroundRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public ResearcherProfessionalBackgroundReportMap(IResearcherProfessionalBackgroundRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.ResearcherProfessionalBackground.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<ReportMapEntity>> Query(Query query)
        {
            query.Contains(nameof(ResearcherProfessionalBackground.Name), query.Search);
            
            var result = await _repository.QueryResearcherProfessionalBackground(query);
            
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
            if (reference is not ResearcherProfessionalBackground entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string Name(object reference)
        {
            if (reference is not ResearcherProfessionalBackground entity)
            throw new Exception($"Cannot get name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
