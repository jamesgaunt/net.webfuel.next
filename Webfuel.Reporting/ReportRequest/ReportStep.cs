using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class ReportStep
    {
        public required Guid TaskId { get; init; }

        public string Stage { get; init; } = String.Empty;
        public int StageCount { get; init; }
        public int StageTotal { get; init; }
        public bool Complete { get; init; }
        public ReportBuilderMetrics Metrics { get; init; } = new ReportBuilderMetrics();

        internal static ReportStep FromTask(ReportTask task)
        {
            var progress = new ReportStep
            {
                TaskId = task.TaskId,
                Stage = task.Builder.Stage,
                StageCount = task.Builder.StageCount,
                StageTotal = task.Builder.StageTotal,
                Complete = task.Builder.Complete,
                Metrics = task.Builder.Metrics
            };

            return progress;
        }
    }
}
