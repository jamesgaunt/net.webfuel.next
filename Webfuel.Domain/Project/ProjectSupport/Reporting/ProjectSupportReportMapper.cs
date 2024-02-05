using Microsoft.Extensions.Caching.Memory;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    [Service(typeof(IReportMapper<ProjectSupport>))]
    internal class ProjectSupportReportMapper : IReportMapper<ProjectSupport>
    {
        private readonly IProjectSupportRepository _repository;
        
        public ProjectSupportReportMapper(IProjectSupportRepository repository)
        {
            _repository = repository;
        }

        public async Task<object?> Get(Guid id)
        {
            if (_getCache.TryGetValue(id, out var cached))
                return cached;

            var value = await _repository.GetProjectSupport(id);

            _getCache.Set(id, value, new MemoryCacheEntryOptions
            {
                Size = 1,
                SlidingExpiration = TimeSpan.FromMinutes(5)
            });

            return value;
        }

        public Task<QueryResult<ReferenceLookup>> Lookup(Query query)
        {
            throw new NotImplementedException();
        }
        
        public Guid Id(object reference)
        {
            if (reference is not ProjectSupport entity)
            throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }
        
        public string Name(object reference)
        {
            throw new NotImplementedException();
        }

        static MemoryCache _getCache = new MemoryCache(new MemoryCacheOptions
        {
            SizeLimit = 100,
        });
    }
}
