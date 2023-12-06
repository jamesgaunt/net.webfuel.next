using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    [ApiEnum]
    public enum ReportFilterNumberCondition
    {
        EqualTo = 10,
    }

    [ApiType]
    public class ReportFilterNumber : ReportFilter
    {
        public override ReportFilterType FilterType => ReportFilterType.Number;

        // Properties

        public Guid FieldId { get; set; }

        public ReportFilterNumberCondition Condition { get; set; } = ReportFilterNumberCondition.EqualTo;

        public decimal? Value { get; set; }

        // Serialization

        public override void ReadProperties(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            while (reader.Read())
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                    break;

                var propertyName = reader.GetString();
                reader.Read();

                switch (propertyName)
                {
                    case "fieldId":
                        FieldId = reader.GetGuid();
                        break;

                    case "condition":
                        Condition = (ReportFilterNumberCondition)reader.GetInt32();
                        break;

                    case "value":
                        if (reader.TokenType == JsonTokenType.Number)
                            Value = reader.GetDecimal();
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }
        }

        public override void WriteProperties(Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            writer.WriteString("fieldId", FieldId);
            writer.WriteNumber("condition", (int)Condition);

            if (Value == null)
                writer.WriteNull("value");
            else
                writer.WriteNumber("value", Value.Value);
        }
    }
}
