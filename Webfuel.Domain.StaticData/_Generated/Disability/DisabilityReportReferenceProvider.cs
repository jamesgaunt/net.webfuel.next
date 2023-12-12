using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportReferenceProvider<Disability>))]
    internal class DisabilityReportReferenceProvider : IReportReferenceProvider<Disability>
    {
        private readonly IDisabilityRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public DisabilityReportReferenceProvider(IDisabilityRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ReportReference?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            var entity = staticData.Disability.FirstOrDefault(x => x.Id == id);
            
            return entity == null ? null : new ReportReference
            {
                Id = entity.Id,
                Name = entity.Name,
                Entity = entity
            };
        }
        
        public async Task<QueryResult<ReportReference>> Query(Query query)
        {
            var result = await _repository.QueryDisability(query);
            
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
