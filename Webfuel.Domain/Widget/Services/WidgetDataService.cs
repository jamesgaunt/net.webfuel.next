using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using Webfuel.Domain.StaticData;
using Webfuel.Terminal;

namespace Webfuel.Domain;

public class WidgetDataResponse
{
    public bool Complete { get; set; }

    public string Data { get; set; } = String.Empty;
}

public class WidgetDataTask
{
    public required Widget Widget { get; set; }
    public required WidgetDataResponse Response { get; set; }

    public object? GeneratorState { get; set; } = null;

    internal DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;
    internal DateTimeOffset LastGeneratedAt { get; set; } = DateTimeOffset.UtcNow;
}

public interface IWidgetDataService
{
    Task<WidgetDataResponse> GenerateData(Guid widgetId);
}

[Service(typeof(IWidgetDataService))]
internal class WidgetDataService : IWidgetDataService
{
    private readonly IWidgetRepository _widgetRepository;
    private readonly IStaticDataService _staticDataService;
    private readonly IIdentityAccessor _identityAccessor;
    private readonly IServiceProvider _serviceProvider;

    public WidgetDataService(
        IWidgetRepository widgetRepository,
        IStaticDataService staticDataService,
        IIdentityAccessor identityAccessor,
        IServiceProvider serviceProvider)
    {
        _widgetRepository = widgetRepository;
        _staticDataService = staticDataService;
        _identityAccessor = identityAccessor;
        _serviceProvider = serviceProvider;
    }

    public async Task<WidgetDataResponse> GenerateData(Guid widgetId)
    {
        var task = RetrieveTask(widgetId);

        if(task == null)
        {
            task = await RegisterTask(widgetId);
            return task.Response;
        }

        // If the task is complete just return the response
        if (task.Response.Complete == true)
            return task.Response;

        // If the task response was generated too recently, just return the previous response
        if (task.LastGeneratedAt > DateTime.UtcNow.AddSeconds(-0.5))
            return task.Response;

        await GenerateTask(task);
        return task.Response;
    }

    // Widget Provider

    IWidgetDataProvider GetProvider(Widget widget)
    {
        if (widget.WidgetTypeId == WidgetTypeEnum.ProjectSummary)
            return _serviceProvider.GetRequiredService<IProjectSummaryProvider>();

        if (widget.WidgetTypeId == WidgetTypeEnum.TeamSupport)
            return _serviceProvider.GetRequiredService<ITeamSupportProvider>();

        throw new InvalidOperationException("Unrecognised widget type");
    }

    // Widget Task Cache

    async Task<WidgetDataTask> RegisterTask(Guid widgetId)
    {
        var widget = await _widgetRepository.GetWidget(widgetId);
        if (widget == null)
            throw new InvalidOperationException("The specified widget does not exist");

        var task = _tasks[widget.Id] = new WidgetDataTask
        {
            Widget = widget,
            Response = new WidgetDataResponse()
        };

        task.Response = await GetProvider(widget).GenerateData(task);
        return task;
    }

    async Task GenerateTask(WidgetDataTask task)
    {
        task.LastGeneratedAt = DateTime.UtcNow;
        task.Response = await GetProvider(task.Widget).GenerateData(task);
    }

    void DeleteTask(Guid widgetId)
    {
        if (!_tasks.TryGetValue(widgetId, out var task))
            return;

        if (task is IDisposable disposable)
        {
            try
            {
                disposable.Dispose();
            }
            catch { /* GULP */ }
        }

        _tasks.Remove(task.Widget.Id, out var _);
    }

    WidgetDataTask? RetrieveTask(Guid widgetId)
    {
        if (!_tasks.TryGetValue(widgetId, out var task))
            return null;

        CleanupTasks();
        return task;
    }

    void CleanupTasks()
    {
        List<Guid> toRemove = new List<Guid>();

        // Remove any tasks that have not been generated in the last 15 seconds

        foreach (var task in _tasks)
        {
            if (task.Value.LastGeneratedAt <= DateTimeOffset.UtcNow.AddSeconds(-15))
                toRemove.Add(task.Key);
        }

        foreach (var widgetId in toRemove)
        {
            DeleteTask(widgetId);
        }
    }

    static ConcurrentDictionary<Guid, WidgetDataTask> _tasks = new ConcurrentDictionary<Guid, WidgetDataTask>();
}
