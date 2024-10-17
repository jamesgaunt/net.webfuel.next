using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Webfuel.Domain;

[ApiEnum]
public enum WidgetTaskStatus
{
    Deferred = 0,
    Processing = 10,
    Complete = 20,
    Cancelled = 999
}

public class WidgetTaskResult
{
    public Widget? Widget { get; set; }

    public required WidgetTaskStatus Status { get; set; }
}

public class WidgetTask
{
    public required Widget Widget { get; set; }

    public object? State { get; set; }

    internal bool Processing { get; set; }

    internal DateTimeOffset ProcessedAt { get; set; } = DateTimeOffset.MinValue;
}

public interface IWidgetTaskService
{
    Task<WidgetTaskResult> BeginProcessing(Guid widgetId);

    Task<WidgetTaskResult> ContinueProcessing(Guid widgetId);
}

[Service(typeof(IWidgetTaskService))]
internal class WidgetTaskService : IWidgetTaskService
{
    private readonly IWidgetRepository _widgetRepository;
    private readonly IWidgetTypeRepository _widgetTypeRepository;
    private readonly IIdentityAccessor _identityAccessor;
    private readonly IServiceProvider _serviceProvider;

    public WidgetTaskService(
        IWidgetRepository widgetRepository,
        IWidgetTypeRepository widgetTypeRepository,
        IIdentityAccessor identityAccessor,
        IServiceProvider serviceProvider)
    {
        _widgetRepository = widgetRepository;
        _widgetTypeRepository = widgetTypeRepository;
        _identityAccessor = identityAccessor;
        _serviceProvider = serviceProvider;
    }

    public async Task<WidgetTaskResult> BeginProcessing(Guid widgetId)
    {
        var widget = await _widgetRepository.GetWidget(widgetId);
        if (widget == null || widget.UserId != _identityAccessor.User?.Id)
            throw new InvalidOperationException("The specified widget does not exist");

        CleanupTasks();

        // If user already has a task running then defer this one
        if (_tasks.Values.Any(p => p.Widget.UserId == widget.UserId))
            return new WidgetTaskResult { Status = WidgetTaskStatus.Deferred };

        // If too many tasks are already running then defer this one
        if (_tasks.Count > 10)
            return new WidgetTaskResult { Status = WidgetTaskStatus.Deferred };

        // Register this as a new task
        var task = _tasks[widget.Id] = new WidgetTask
        {
            Widget = widget,
        };

        return await ContinueProcessing(widgetId);
    }

    public async Task<WidgetTaskResult> ContinueProcessing(Guid widgetId)
    {
        var task = RetrieveTask(widgetId);
        if (task == null)
            return new WidgetTaskResult { Status = WidgetTaskStatus.Cancelled };

        try
        {
            // If the task was process too recently, just return a processing response
            if (task.ProcessedAt > DateTime.UtcNow.AddSeconds(-0.1))
                return new WidgetTaskResult { Status = WidgetTaskStatus.Processing };

            // If the task is still actively processing, just return a processing response
            if(task.Processing)
                return new WidgetTaskResult { Status = WidgetTaskStatus.Processing };

            task.Processing = true;
            var provider = _serviceProvider.GetRequiredKeyedService<IWidgetProvider>(task.Widget.WidgetTypeId);

            task.ProcessedAt = DateTimeOffset.UtcNow;
            var status = await provider.ProcessTask(task);

            if (status != WidgetTaskStatus.Processing)
                DeleteTask(widgetId);

            return new WidgetTaskResult
            {
                Status = status,
                Widget = status == WidgetTaskStatus.Complete ? task.Widget : null
            };
        }
        catch
        {
            DeleteTask(widgetId);
            throw;
        }
        finally
        {
            task.Processing = false;
        }
    }

    void DeleteTask(Guid widgetId)
    {
        _tasks.Remove(widgetId, out var _);
    }

    WidgetTask? RetrieveTask(Guid widgetId)
    {
        if (!_tasks.TryGetValue(widgetId, out var task))
            return null;
        return task;
    }

    void CleanupTasks()
    {
        List<Guid> toRemove = new List<Guid>();

        // Remove any tasks that have not been processed in the last 10 seconds

        foreach (var task in _tasks)
        {
            if (task.Value.ProcessedAt <= DateTimeOffset.UtcNow.AddSeconds(-10))
                toRemove.Add(task.Key);
        }

        foreach (var widgetId in toRemove)
        {
            DeleteTask(widgetId);
        }
    }

    static ConcurrentDictionary<Guid, WidgetTask> _tasks = new ConcurrentDictionary<Guid, WidgetTask>();
}
