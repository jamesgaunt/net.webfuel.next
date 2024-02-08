using Microsoft.Extensions.Caching.Memory;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    [Service(typeof(IReportMap<Project>))]
    internal class ProjectReportMap : IReportMap<Project>
    {
        private readonly IProjectRepository _repository;
        
        public ProjectReportMap(IProjectRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<object?> Get(Guid id)
        {
            if (_getCache.TryGetValue(id, out var cached))
                return cached;

            var value = await _repository.GetProject(id);

            _getCache.Set(id, value, new MemoryCacheEntryOptions
            {
                Size = 1,
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1)
            });

            return value;
        }

        public async Task<QueryResult<ReportMapEntity>> Query(Query query)
        {
            query.Contains(nameof(Project.PrefixedNumber), query.Search);

            var result = await _repository.QueryProject(query);

            return new QueryResult<ReportMapEntity>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.Select(p => new ReportMapEntity
                {
                    Id = p.Id,
                    Name = p.PrefixedNumber
                }).ToList()
            };
        }

        public Guid Id(object reference)
        {
            if (reference is not Project entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string Name(object reference)
        {
            if (reference is not Project entity)
            throw new Exception($"Cannot get name of type {reference.GetType()}");
            return entity.PrefixedNumber;
        }

        static MemoryCache _getCache = new MemoryCache(new MemoryCacheOptions
        {
            SizeLimit = 100,
        });
    }
}
