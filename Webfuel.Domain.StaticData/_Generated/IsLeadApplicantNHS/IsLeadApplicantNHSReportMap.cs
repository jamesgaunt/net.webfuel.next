using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMap<IsLeadApplicantNHS>))]
    internal class IsLeadApplicantNHSReportMap : IReportMap<IsLeadApplicantNHS>
    {
        private readonly IIsLeadApplicantNHSRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public IsLeadApplicantNHSReportMap(IIsLeadApplicantNHSRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.IsLeadApplicantNHS.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<ReportMapEntity>> Query(Query query)
        {
            query.Contains(nameof(IsLeadApplicantNHS.Name), query.Search);
            
            var result = await _repository.QueryIsLeadApplicantNHS(query);
            
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
            if (reference is not IsLeadApplicantNHS entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string Name(object reference)
        {
            if (reference is not IsLeadApplicantNHS entity)
            throw new Exception($"Cannot get name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
