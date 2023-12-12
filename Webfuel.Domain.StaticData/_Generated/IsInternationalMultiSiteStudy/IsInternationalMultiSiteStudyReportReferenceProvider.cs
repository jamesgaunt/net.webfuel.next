using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportReferenceProvider<IsInternationalMultiSiteStudy>))]
    internal class IsInternationalMultiSiteStudyReportReferenceProvider : IReportReferenceProvider<IsInternationalMultiSiteStudy>
    {
        private readonly IIsInternationalMultiSiteStudyRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public IsInternationalMultiSiteStudyReportReferenceProvider(IIsInternationalMultiSiteStudyRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ReportReference?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            var entity = staticData.IsInternationalMultiSiteStudy.FirstOrDefault(x => x.Id == id);
            
            return entity == null ? null : new ReportReference
            {
                Id = entity.Id,
                Name = entity.Name,
                Entity = entity
            };
        }
        
        public async Task<QueryResult<ReportReference>> Query(Query query)
        {
            var result = await _repository.QueryIsInternationalMultiSiteStudy(query);
            
            return new QueryResult<ReportReference>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.Select(x => new ReportReference
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Entity = x
                    }).ToList()
            };
        }
    }
}
