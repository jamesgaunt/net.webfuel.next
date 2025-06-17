using Microsoft.Extensions.Caching.Memory;
using Webfuel.Reporting;

namespace Webfuel.Domain;


[Service(typeof(ProjectSupportReportMap), typeof(IReportMap<ProjectSupport>))]
internal class ProjectSupportReportMap : IReportMap<ProjectSupport>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectSupportRepository _repository;

    public ProjectSupportReportMap(IProjectRepository projectRepository, IProjectSupportRepository repository)
    {
        _projectRepository = projectRepository;
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
        var project = await _projectRepository.GetProject(projectId);
        if (project == null)
            return new List<Guid>();

        var items = await _repository.SelectProjectSupportByProjectSupportGroupId(project.ProjectSupportGroupId);
        foreach (var item in items)
        {
            _getCache.Set(item.Id, item, new MemoryCacheEntryOptions
            {
                Size = 1,
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1)
            });
        }
        return items.Select(x => x.Id).ToList();
    }

    public async Task<List<Guid>> MapOpenByProjectId(Guid projectId)
    {
        var project = await _projectRepository.GetProject(projectId);
        if (project == null)
            return new List<Guid>();

        var items = await _repository.SelectProjectSupportByProjectSupportGroupId(project.ProjectSupportGroupId);
        foreach (var item in items)
        {
            _getCache.Set(item.Id, item, new MemoryCacheEntryOptions
            {
                Size = 1,
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1)
            });
        }
        return items.Where(x => x.SupportRequestedCompletedAt == null).Select(x => x.Id).ToList();
    }

    public Task<QueryResult<ReportMapEntity>> Query(Query query)
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
