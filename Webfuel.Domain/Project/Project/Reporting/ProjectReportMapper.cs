using Microsoft.Extensions.Caching.Memory;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    [Service(typeof(IReportMapper<Project>))]
    internal class ProjectReportMapper : IReportMapper<Project>
    {
        private readonly IProjectRepository _repository;
        
        public ProjectReportMapper(IProjectRepository repository)
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
                SlidingExpiration = TimeSpan.FromMinutes(5)
            });

            return value;
        }

        public async Task<QueryResult<ReferenceLookup>> Lookup(Query query)
        {
            query.Contains(nameof(Project.PrefixedNumber), query.Search);

            var result = await _repository.QueryProject(query);

            return new QueryResult<ReferenceLookup>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.Select(p => new ReferenceLookup
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
