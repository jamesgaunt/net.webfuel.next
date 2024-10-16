using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using Webfuel.Domain.StaticData;
using Webfuel.Terminal;

namespace Webfuel.Domain
{
    public class WidgetRefreshTask
    {
        public required Widget Widget { get; set; }

        public object? GeneratorState { get; set; } = null;

        internal DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;
        internal DateTimeOffset LastRefreshedAt { get; set; } = DateTimeOffset.UtcNow;
    }

    public class RefreshWidget : IRequest<Widget>
    {
        public required Guid Id { get; set; }
    }

    internal class RefreshWidgetHandler : IRequestHandler<RefreshWidget, Widget>
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

        public async Task<Widget> Handle(RefreshWidget request, CancellationToken cancellationToken)
        {
            var task = RetrieveTask(request.Id);

            if (task == null)
            {
                task = await RegisterTask(request.Id);
                return task.Widget;
            }

            // If the widgets data is current just return it
            if (task.Widget.DataCurrent)
                return task.Widget;

            // If the task was refresh too recently, just return the previous response
            if (task.LastRefreshedAt > DateTime.UtcNow.AddSeconds(-0.5))
                return task.Widget;

            await RefreshTask(task);
            return task.Widget;
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

            await RefreshTask(task);
            
            return task;
        }

        async Task RefreshTask(WidgetRefreshTask task)
        {
            var provider = _serviceProvider.GetRequiredKeyedService<IWidgetDataProvider>(task.Widget.WidgetTypeId);
            await provider.RefreshWidget(task);
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

            // Remove any tasks that have not been generated in the last 15 seconds

            foreach (var task in _tasks)
            {
                if (task.Value.LastRefreshedAt <= DateTimeOffset.UtcNow.AddSeconds(-15))
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
