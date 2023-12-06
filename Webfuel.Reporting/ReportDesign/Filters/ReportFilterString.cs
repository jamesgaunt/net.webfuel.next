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
    public enum ReportFilterStringCondition
    {
        Contains = 10,
        StartsWith = 20,
        EndsWith = 30,

        IsEmpty = 100,
        IsNotEmpty = 200,
    }

    [ApiType]
    public class ReportFilterString : ReportFilter
    {
        public override ReportFilterType FilterType => ReportFilterType.String;

        // Properties

        public Guid FieldId { get; set; }

        public ReportFilterStringCondition Condition { get; set; } = ReportFilterStringCondition.Contains;

        public string Value { get; set; } = String.Empty;

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
                        Condition = (ReportFilterStringCondition)reader.GetInt32();
                        break;

                    case "value":
                        Value = reader.GetString() ?? String.Empty;
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
            writer.WriteString("value", Value);
        }
    }
}
