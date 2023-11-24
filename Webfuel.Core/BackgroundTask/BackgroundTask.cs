using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public class BackgroundTask
    {
        public required string Name { get; init; }

        public Guid TaskId { get; internal set; }

        public Guid ProcessorId { get; set; }

        public Guid IdentityId { get; internal set; }

        public DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;

        public DateTimeOffset LastAccessedAt { get; internal set; } = DateTimeOffset.UtcNow;

        public int ProgressPercentage { get; set; }

        public bool Complete { get; set; }
    }
}
