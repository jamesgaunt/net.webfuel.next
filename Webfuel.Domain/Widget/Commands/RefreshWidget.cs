using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Webfuel.Domain
{
    public class RefreshWidgetResult
    {
        public required Widget Widget { get; set; }
        public required bool Complete { get; set; }
    }

    public class WidgetRefreshTask
    {
        public required Widget Widget { get; set; }
        
        public object? State { get; set; }
        public bool Complete { get; set; }

        internal DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;
        internal DateTimeOffset LastRefreshedAt { get; set; } = DateTimeOffset.MinValue;
    }

    public class RefreshWidget : IRequest<RefreshWidgetResult>
    {
        public required Guid Id { get; set; }
    }

    internal class RefreshWidgetHandler : IRequestHandler<RefreshWidget, RefreshWidgetResult>
    {
        private readonly IWidgetRepository _widgetRepository;
        private readonly IWidgetTypeRepository _widgetTypeRepository;
        private readonly IServiceProvider _serviceProvider;

        public RefreshWidgetHandler(
            IWidgetRepository widgetRepository,
            IWidgetTypeRepository widgetTypeRepository,
            IServiceProvider serviceProvider)
        {
            _widgetRepository = widgetRepository;
            _widgetTypeRepository = widgetTypeRepository;
            _serviceProvider = serviceProvider;
        }

        public async Task<RefreshWidgetResult> Handle(RefreshWidget request, CancellationToken cancellationToken)
        {
            var task = RetrieveTask(request.Id);
            if (task == null)
                task = await RegisterTask(request.Id);

            // If we have a task and it is complete then just return it (prevents running a new task too soon)
            if (task.Complete)
                return new RefreshWidgetResult { Complete = task.Complete, Widget = task.Widget };

            // If the task was refreshed too recently, just return the current state response
            if (task.LastRefreshedAt > DateTime.UtcNow.AddSeconds(-0.5))
                return new RefreshWidgetResult { Complete = task.Complete, Widget = task.Widget };

            // Refresh the task processing
            await RefreshTask(task);

            // If the task is now complete update the database 
            if (task.Complete)
                await _widgetRepository.UpdateWidget(task.Widget);

            return new RefreshWidgetResult { Complete = task.Complete, Widget = task.Widget };
        }

        async Task<WidgetRefreshTask> RegisterTask(Guid widgetId)
        {
            var widget = await _widgetRepository.GetWidget(widgetId);
            if (widget == null)
                throw new InvalidOperationException("The specified widget does not exist");

            var task = _tasks[widget.Id] = new WidgetRefreshTask
            {
                Widget = widget,
            };

            return task;
        }

        async Task RefreshTask(WidgetRefreshTask task)
        {
            var provider = _serviceProvider.GetRequiredKeyedService<IWidgetDataProvider>(task.Widget.WidgetTypeId);
            await provider.RefreshTask(task);
            task.LastRefreshedAt = DateTime.UtcNow;
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

        WidgetRefreshTask? RetrieveTask(Guid widgetId)
        {
            if (!_tasks.TryGetValue(widgetId, out var task))
                return null;

            CleanupTasks();
            return task;
        }

        void CleanupTasks()
        {
            List<Guid> toRemove = new List<Guid>();

            // Remove any tasks that have not been generated in the last 30 seconds

            foreach (var task in _tasks)
            {
                if (task.Value.LastRefreshedAt <= DateTimeOffset.UtcNow.AddSeconds(-30))
                    toRemove.Add(task.Key);
            }

            foreach (var widgetId in toRemove)
            {
                DeleteTask(widgetId);
            }
        }

        static ConcurrentDictionary<Guid, WidgetRefreshTask> _tasks = new ConcurrentDictionary<Guid, WidgetRefreshTask>();
    }
}
