using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public abstract class ReportTask: IDisposable
    {
        public abstract Type ReportGeneratorType { get; }
        public virtual ReportResult GenerateResult() { throw new NotImplementedException(); }
        public abstract void Dispose();

        public Guid TaskId { get; internal set; }

        public int ProgressCount { get; set; }
        public int TotalCount { get; set; }
        public bool Complete { get; set; }

        internal Guid? IdentityId { get; set; }
        internal DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;
        internal DateTimeOffset LastAccessedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
