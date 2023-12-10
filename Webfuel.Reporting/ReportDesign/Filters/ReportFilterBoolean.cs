using DocumentFormat.OpenXml.Spreadsheet;
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
    public class ReportFilterBoolean : ReportFilterField
    {
        public override ReportFilterType FilterType => ReportFilterType.Boolean;

        public bool? Value { get; set; }

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(Value), propertyName, true) == 0)
            {
                if (reader.TokenType == JsonTokenType.True)
                    Value = true;
                else if (reader.TokenType == JsonTokenType.False)
                    Value = false;
                return true;
            }


            return base.ReadProperty(propertyName, ref reader);
        }
        
        public override void WriteProperties(Utf8JsonWriter writer)
        {
            base.WriteProperties(writer);

            if(Value == null)
                writer.WriteNull("value");
            else
                writer.WriteBoolean("value", Value.Value);
        }

        // Validation

        public override void ValidateFilter(ReportSchema schema)
        {
            base.ValidateFilter(schema);
        }
    }
}
