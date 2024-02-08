using Webfuel.Reporting;

namespace Webfuel.Domain.StaticData
{
    [Service(typeof(IReportMap<FundingBody>))]
    internal class FundingBodyReportMap : IReportMap<FundingBody>
    {
        private readonly IFundingBodyRepository _repository;
        private readonly IStaticDataCache _staticDataCache;
        
        public FundingBodyReportMap(IFundingBodyRepository repository, IStaticDataCache staticDataCache)
        {
            _repository = repository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<object?> Get(Guid id)
        {
            var staticData = await _staticDataCache.GetStaticData();
            return staticData.FundingBody.FirstOrDefault(x => x.Id == id);
        }
        
        public async Task<QueryResult<ReportMapEntity>> Query(Query query)
        {
            query.Contains(nameof(FundingBody.Name), query.Search);
            
            var result = await _repository.QueryFundingBody(query);
            
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
            if (reference is not FundingBody entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string Name(object reference)
        {
            if (reference is not FundingBody entity)
            throw new Exception($"Cannot get name of type {reference.GetType()}");
            return entity.Name;
        }
    }
}
