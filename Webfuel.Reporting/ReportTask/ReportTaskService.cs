﻿using Microsoft.Identity.Client;
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
        ReportTask RegisterTask(ReportBuilderBase builder);

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

        public ReportTask RegisterTask(ReportBuilderBase builder)
        {
            CleanupTasks();

            // If this user already has a report task running then kill it
            {
                var taskId = CurrentUserTask();
                if (taskId != null)
                    DeleteTask(taskId.Value);
            }

            if (_tasks.Count() > 10)
                throw new InvalidOperationException("Report generation capacity reached. Please try again later.");

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
            {
                try
                {
                    disposable.Dispose();
                }
                catch { /* GULP */ }
            }

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
            var identity = _identityAccessor.User!;

            foreach (var task in _tasks)
            {
                if (task.Value.IdentityId == identity.Id)
                    return task.Key;
            }
            return null;
        }

        void CleanupTasks()
        {
            List<Guid> toRemove = new List<Guid>();

            // Remove any tasks that have not been accessed in the last 2 minutes
            foreach (var task in _tasks)
            {
                if (task.Value.LastAccessedAt <= DateTimeOffset.UtcNow.AddMinutes(-2))
                    toRemove.Add(task.Key);
            }

            foreach (var taskId in toRemove)
            {
                DeleteTask(taskId);
            }
        }

        static ConcurrentDictionary<Guid, ReportTask> _tasks = new ConcurrentDictionary<Guid, ReportTask>();
    }
}
