using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    [ApiType]
    public abstract class ReportField
    {
        public required Guid Id { get; init; }

        public required String Name { get; init; }

        [JsonIgnore]
        public IReportMapping? Mapping { get; init; }

        public required ReportFieldType FieldType { get; init; }

        public abstract Task<object?> Evaluate(object context, IServiceProvider services);
    }
}
