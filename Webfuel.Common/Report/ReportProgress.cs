using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    public class ReportProgress
    {
        public required Guid TaskId { get; init; }
        public required int ProgressPercentage { get; init; }
        public required bool Complete { get; init; }

        public static ReportProgress FromTask(ReportTask task)
        {
            var progressPercentage = 0;
            if (task.TotalCount > 0)
                progressPercentage = task.ProgressCount * 100 / task.TotalCount;

            var progress = new ReportProgress
            {
                TaskId = task.TaskId,
                ProgressPercentage = progressPercentage,
                Complete = task.Complete
            };

            return progress;
        }
    }
}
