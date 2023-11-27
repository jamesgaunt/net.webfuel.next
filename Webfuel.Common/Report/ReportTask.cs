using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    public abstract class ReportTask
    {
        public abstract Type ReportGenerator { get; }

        public Guid TaskId { get; internal set; }
        public int ProgressPercentage { get; set; }
        public bool Complete { get; set; }

        internal Guid IdentityId { get; set; }
        internal DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;
        internal DateTimeOffset LastAccessedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
