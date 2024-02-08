using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMap<SubmissionOutcome>))]
    internal class SubmissionOutcomeReportMap : IReportMap<SubmissionOutcome>
    {
        private readonly ISubmissionOutcomeRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public SubmissionOutcomeReportMap(ISubmissionOutcomeRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.SubmissionOutcome.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<ReportMapEntity>> Query(Query query)
        {
            query.Contains(nameof(SubmissionOutcome.Name), query.Search);
            
            var result = await _repository.QuerySubmissionOutcome(query);
            
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
            if (reference is not SubmissionOutcome entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string Name(object reference)
        {
            if (reference is not SubmissionOutcome entity)
            throw new Exception($"Cannot get name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
