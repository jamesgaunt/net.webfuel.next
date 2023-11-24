using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel
{
    public class ReportingField<TContext> where TContext : class
    {
        public required string Name { get; init; }

        public required string Identifier { get; init; }

        public required ReportingFieldType FieldType { get; init; }

        public string DataSource { get; init; } = String.Empty;

        public bool Nullable { get; init; }

        public bool Default { get; init; }

        public required Func<TContext, Task<object?>> Accessor { get; init; }

        public Func<object?, Task<string>>? Formatter { get; init; } = null; // If null use the default formatter for this field type
    }
}
