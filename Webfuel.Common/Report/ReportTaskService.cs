using Microsoft.Identity.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    internal interface IReportTaskService
    {
        void StoreTask(ReportTask task);

        ReportTask? RetrieveTask(Guid taskId);
    }

    [Service(typeof(IReportTaskService))]
    internal class ReportTaskService: IReportTaskService
    {
        private readonly IIdentityAccessor _identityAccessor;

        public ReportTaskService(IIdentityAccessor identityAccessor)
        {
            this._identityAccessor = identityAccessor;
        }

        public void StoreTask(ReportTask task)
        {
            if (_identityAccessor.User == null)
                throw new InvalidOperationException("Unable to initialise a report task without an identity context");

            var currentTaskId = CurrentTask();
            if(currentTaskId != null)
            {
                // For now just dump it
                _tasks.Remove(currentTaskId.Value, out var _);
            }

            task.TaskId = Guid.NewGuid();
            task.IdentityId = _identityAccessor.User.Id;
            task.LastAccessedAt = DateTimeOffset.UtcNow;

            _tasks[task.TaskId] = task;

            CleanupTasks();
        }

        public ReportTask? RetrieveTask(Guid taskId)
        {
            if (_identityAccessor.User == null)
                throw new InvalidOperationException("Unable to retrieve a report task without an identity context");

            if (!_tasks.TryGetValue(taskId, out var task))
                return null;

            if (task.IdentityId != _identityAccessor.User.Id)
                return null;

            task.LastAccessedAt = DateTimeOffset.UtcNow;

            CleanupTasks();
            return task;
        }

        Guid? CurrentTask()
        {
            if (_identityAccessor.User == null)
                return null;

            foreach(var task in _tasks)
            {
                if (task.Value.IdentityId == _identityAccessor.User.Id)
                    return task.Key;
            }
            return null;
        }

        void CleanupTasks()
        {
            foreach(var task in _tasks)
            {
                if(task.Value.LastAccessedAt <= DateTimeOffset.UtcNow.AddMinutes(-5))
                {
                    _tasks.Remove(task.Key, out var _);
                }
            }
        }

        static ConcurrentDictionary<Guid, ReportTask> _tasks = new ConcurrentDictionary<Guid, ReportTask>();
    }
}
