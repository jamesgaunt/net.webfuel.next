using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    [ApiType]
    public interface IReportField
    {
        string Id { get; }

        string Name { get; }

        ReportFieldType FieldType { get; }

        string ReferenceType { get; }

        bool Nullable { get; }

        bool Default { get; }
    }

    public class ReportField<TContext>: IReportField where TContext : class
    {
        public ReportField(string id)
        {
            Id = id;
            Name = id;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public ReportFieldType FieldType { get; set; }

        public string ReferenceType { get; set; } = String.Empty;

        public bool Nullable { get; set; }

        public bool Default { get; set; } = true;

        [JsonIgnore]
        public Func<TContext, object?>? Accessor { get; init; }

        [JsonIgnore]
        public Func<TContext, Task<object?>>? AsyncAccessor { get; init; }
    }
}
