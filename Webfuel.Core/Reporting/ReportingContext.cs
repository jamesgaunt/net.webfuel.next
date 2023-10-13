using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public class ReportingContext<TContext> where TContext: class
    {
        public required Guid Id { get; init; }

        public required string Name { get; init; }

        public required string Description { get; init; }

        public required IReadOnlyList<ReportingField<TContext>> Fields { get; init; }
    }
}
