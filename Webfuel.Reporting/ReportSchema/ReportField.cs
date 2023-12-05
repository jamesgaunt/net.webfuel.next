using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    [ApiType]
    public class ReportField
    {
        public required Guid Id { get; init; }

        public required String Name { get; init; }

        public required ReportFieldType FieldType { get; init; }

        public bool Exportable { get; init; } = true;

        [JsonIgnore]
        public Func<object, object?>? Accessor { get; set; }

        [JsonIgnore]
        public Func<object, Task<object?>>? AsyncAccessor { get; set; }
    }
}
