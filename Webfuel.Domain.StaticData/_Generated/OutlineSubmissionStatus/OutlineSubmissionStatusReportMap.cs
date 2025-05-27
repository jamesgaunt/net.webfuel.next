using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMap<OutlineSubmissionStatus>))]
    internal class OutlineSubmissionStatusReportMap : IReportMap<OutlineSubmissionStatus>
    {
        private readonly IOutlineSubmissionStatusRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public OutlineSubmissionStatusReportMap(IOutlineSubmissionStatusRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.OutlineSubmissionStatus.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<ReportMapEntity>> Query(Query query)
        {
            query.Contains(nameof(OutlineSubmissionStatus.Name), query.Search);
            
            var result = await _repository.QueryOutlineSubmissionStatus(query);
            
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
            if (reference is not OutlineSubmissionStatus entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string Name(object reference)
        {
            if (reference is not OutlineSubmissionStatus entity)
            throw new Exception($"Cannot get name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
