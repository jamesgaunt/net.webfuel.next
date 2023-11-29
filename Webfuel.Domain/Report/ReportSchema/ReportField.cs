using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    [ApiType]
    public interface IReportField
    {
        string FieldId { get; }

        string Name { get; }

        ReportFieldType FieldType { get; }

        string ReferenceType { get; }

        bool Nullable { get; }

        bool Default { get; }
    }

    public class ReportField<TContext>: IReportField where TContext : class
    {
        public required string FieldId { get; init; }

        public required string Name { get; init; }

        public required ReportFieldType FieldType { get; init; }

        public string ReferenceType { get; init; } = String.Empty;

        public bool Nullable { get; init; }

        public bool Default { get; init; } = true;

        public Func<TContext, object?>? Accessor { get; init; }

        public Func<TContext, Task<object?>>? AsyncAccessor { get; init; }

        public Func<object?, Task<string>>? Formatter { get; init; } = null; // If null use the default formatter for this field type
    }
}
