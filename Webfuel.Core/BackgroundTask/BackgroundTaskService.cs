using Microsoft.Identity.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public interface IBackgroundTaskService
    {
        void InitialiseTask(BackgroundTask task);

        T? RetrieveTask<T>(Guid taskId) where T : BackgroundTask;
    }

    [Service(typeof(BackgroundTaskService))]
    internal class BackgroundTaskService: IBackgroundTaskService
    {
        private readonly IIdentityAccessor _identityAccessor;

        public BackgroundTaskService(IIdentityAccessor identityAccessor)
        {
            this._identityAccessor = identityAccessor;
        }

        public void InitialiseTask(BackgroundTask task)
        {
            if (_identityAccessor.User == null)
                throw new InvalidOperationException("Unable to initialise a background task without an identity context");

            var currentTaskId = CurrentIdentityTask();
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

        public T? RetrieveTask<T>(Guid taskId) where T : BackgroundTask
        {
            if (_identityAccessor.User == null)
                throw new InvalidOperationException("Unable to retrieve a background task without an identity context");

            if (!_tasks.TryGetValue(taskId, out var task))
                return null;

            if (task.IdentityId != _identityAccessor.User.Id)
                return null;

            if (!(task is T))
                return null;

            task.LastAccessedAt = DateTimeOffset.UtcNow;

            CleanupTasks();
            return task as T;
        }

        Guid? CurrentIdentityTask()
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

        static ConcurrentDictionary<Guid, BackgroundTask> _tasks = new ConcurrentDictionary<Guid, BackgroundTask>();
    }
}
