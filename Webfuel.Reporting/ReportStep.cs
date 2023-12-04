using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class ReportStep
    {
        public required Guid TaskId { get; init; }
        public required int ProgressPercentage { get; init; }
        public required bool Complete { get; init; }

        internal static ReportStep FromTask(ReportTask task)
        {
            var progressPercentage = 0;
            if (task.Builder.TotalCount > 0)
                progressPercentage = task.Builder.ProgressCount * 100 / task.Builder.TotalCount;

            var progress = new ReportStep
            {
                TaskId = task.TaskId,
                ProgressPercentage = progressPercentage,
                Complete = task.Builder.Complete
            };

            return progress;
        }
    }
}
