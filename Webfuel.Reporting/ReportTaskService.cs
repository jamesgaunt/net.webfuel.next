using Microsoft.Identity.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    internal interface IReportTaskService
    {
        ReportTask RegisterTask(ReportBuilder builder);

        ReportTask? RetrieveTask(Guid taskId);

        void DeleteTask(Guid taskId);
    }

    [Service(typeof(IReportTaskService))]
    internal class ReportTaskService : IReportTaskService
    {
        private readonly IIdentityAccessor _identityAccessor;

        public ReportTaskService(IIdentityAccessor identityAccessor)
        {
            this._identityAccessor = identityAccessor;
        }

        public ReportTask RegisterTask(ReportBuilder builder)
        {
            // If this user already has a report task running then kill it
            {
                var taskId = CurrentUserTask();
                if (taskId != null)
                    DeleteTask(taskId.Value);
            }
            var task = new ReportTask
            {
                TaskId = Guid.NewGuid(),
                IdentityId = _identityAccessor.User?.Id,
                LastAccessedAt = DateTimeOffset.UtcNow,
                Builder = builder
            };

            _tasks[task.TaskId] = task;
            CleanupTasks();

            return task;
        }

        public void DeleteTask(Guid taskId)
        {
            if (!_tasks.TryGetValue(taskId, out var task))
                return;

            if (task is IDisposable disposable)
                disposable.Dispose();

            _tasks.Remove(task.TaskId, out var _);
        }

        public ReportTask? RetrieveTask(Guid taskId)
        {
            if (!_tasks.TryGetValue(taskId, out var task))
                return null;

            task.LastAccessedAt = DateTimeOffset.UtcNow;

            CleanupTasks();
            return task;
        }

        Guid? CurrentUserTask()
        {
            if (_identityAccessor.User == null)
                return null;

            foreach (var task in _tasks)
            {
                if (task.Value.IdentityId == _identityAccessor.User.Id)
                    return task.Key;
            }
            return null;
        }

        void CleanupTasks()
        {
            foreach (var task in _tasks)
            {
                if (task.Value.LastAccessedAt <= DateTimeOffset.UtcNow.AddMinutes(-10))
                {
                    DeleteTask(task.Key);
                    return; // Iterator is now invalid
                }
            }
        }

        static ConcurrentDictionary<Guid, ReportTask> _tasks = new ConcurrentDictionary<Guid, ReportTask>();
    }
}
