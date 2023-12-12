using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportReferenceProvider<IsLeadApplicantNHS>))]
    internal class IsLeadApplicantNHSReportReferenceProvider : IReportReferenceProvider<IsLeadApplicantNHS>
    {
        private readonly IIsLeadApplicantNHSRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public IsLeadApplicantNHSReportReferenceProvider(IIsLeadApplicantNHSRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ReportReference?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            var entity = staticData.IsLeadApplicantNHS.FirstOrDefault(x => x.Id == id);
            
            return entity == null ? null : new ReportReference
            {
                Id = entity.Id,
                Name = entity.Name,
                Entity = entity
            };
        }
        
        public async Task<QueryResult<ReportReference>> Query(Query query)
        {
            var result = await _repository.QueryIsLeadApplicantNHS(query);
            
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
