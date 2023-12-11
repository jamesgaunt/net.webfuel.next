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
        public required ReportBuilderBase Builder { get; init; }


        internal Guid? IdentityId { get; set; }
        internal DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;
        internal DateTimeOffset LastAccessedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
