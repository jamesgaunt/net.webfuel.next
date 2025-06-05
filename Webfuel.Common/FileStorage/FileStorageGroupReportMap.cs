using Microsoft.Extensions.Caching.Memory;
using Webfuel.Reporting;

namespace Webfuel.Common;

[Service(typeof(IReportMap<FileStorageGroup>))]
internal class FileStorageGroupReportMap : IReportMap<FileStorageGroup>
{
    private readonly IFileStorageGroupRepository _repository;

    public FileStorageGroupReportMap(IFileStorageGroupRepository repository)
    {
        _repository = repository;
    }

    public async Task<object?> Get(Guid id)
    {
        if (_getCache.TryGetValue(id, out var cached))
            return cached;

        var value = await _repository.GetFileStorageGroup(id);

        _getCache.Set(id, value, new MemoryCacheEntryOptions
        {
            Size = 1,
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1)
        });

        return value;
    }

    public Task<QueryResult<ReportMapEntity>> Query(Query query)
    {
        throw new NotImplementedException();
    }

    public Guid Id(object reference)
    {
        if (reference is not FileStorageGroup entity)
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
