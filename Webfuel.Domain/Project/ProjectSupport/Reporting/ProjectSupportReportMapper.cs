using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Extensions.Caching.Memory;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public interface IProjectSupportReportMapper : IReportMapper<ProjectSupport>
    {
        Task<List<Guid>> MapByProjectId(Guid projectId);
    }

    [Service(typeof(IProjectSupportReportMapper), typeof(IReportMapper<ProjectSupport>))]
    internal class ProjectSupportReportMapper : IProjectSupportReportMapper
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
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1)
            });

            return value;
        }

        public async Task<List<Guid>> MapByProjectId(Guid projectId)
        {
            var items = await _repository.SelectProjectSupportByProjectId(projectId);
            foreach(var item in items)
            {
                _getCache.Set(item.Id, item, new MemoryCacheEntryOptions
                {
                    Size = 1,
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1)
                });
            }
            return items.Select(x => x.Id).ToList();
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
