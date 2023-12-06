using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    internal class ReportTask
    {
        public required Guid TaskId { get; init; }
        public required ReportBuilder Builder { get; init; }


        internal Guid? IdentityId { get; set; }
        internal DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;
        internal DateTimeOffset LastAccessedAt { get; set; } = DateTimeOffset.UtcNow;

        // Metrics (TODO)

        public int IterationCount { get; set; }
        public long TotalMicroseconds { get; set; }
    }
}
