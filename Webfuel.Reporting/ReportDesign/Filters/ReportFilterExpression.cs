using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    [ApiType]
    public class ReportFilterExpression : ReportFilter
    {
        public override ReportFilterType FilterType => ReportFilterType.Expression;

        // Properties

        public string Expression { get; set; } = String.Empty;

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(Expression), propertyName, true) == 0)
            {
                Expression = reader.GetString() ?? String.Empty;
                return true;
            }

            return base.ReadProperty(propertyName, ref reader);
        }

        public override void WriteProperties(Utf8JsonWriter writer)
        {
            base.WriteProperties(writer);
            writer.WriteString("expression", Expression);

        }

        // Validation

        public override void ValidateFilter(ReportSchema schema)
        {
            base.ValidateFilter(schema);
        }

        // Description

        public override string GenerateDescription(ReportSchema schema)
        {
            return "Custom Expression";
        }
    }
}
