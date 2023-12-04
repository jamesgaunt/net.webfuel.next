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
        public required String Name { get; init; }

        public required ReportFieldType FieldType { get; init; }

        public String Label { get; init; } = String.Empty;

        public bool Default { get; init; } = true;
    }
}
