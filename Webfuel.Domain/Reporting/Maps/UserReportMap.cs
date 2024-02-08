using Microsoft.Extensions.Caching.Memory;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    [Service(typeof(IReportMap<User>))]
    internal class UserReportMap : IReportMap<User>
    {
        private readonly IUserRepository _repository;

        public UserReportMap(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<object?> Get(Guid id)
        {
            if(_getCache.TryGetValue(id, out var cached))
                return cached;

            var value = await _repository.GetUser(id);

            _getCache.Set(id, value, new MemoryCacheEntryOptions
            {
                Size = 1,
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1)
            });
            
            return value;
        }

        public async Task<QueryResult<ReferenceLookup>> Query(Query query)
        {
            query.Contains(nameof(User.FullName), query.Search);

            var result = await _repository.QueryUser(query);

            return new QueryResult<ReferenceLookup>
            {
                TotalCount = result.TotalCount,
                Items = result.Items.Select(p => new ReferenceLookup
                {
                    Id = p.Id,
                    Name = p.FullName
                }).ToList()
            };
        }

        public Guid Id(object reference)
        {
            if (reference is not User entity)
                throw new Exception($"Cannot get id of type {reference.GetType()}");
            return entity.Id;
        }

        public string Name(object reference)
        {
            if (reference is not User entity)
                throw new Exception($"Cannot get name of type {reference.GetType()}");
            return entity.FullName;
        }

        static MemoryCache _getCache = new MemoryCache(new MemoryCacheOptions
        {
            SizeLimit = 100,
        });
    }
}
