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
    public enum ReportParameterStringCondition
    {
        Contains = 10,
        StartsWith = 20,
        EndsWith = 30,

        IsEmpty = 100,
        IsNotEmpty = 200,
    }

    [ApiType]
    public class ReportParameterString: ReportParameter
    {
        public override ReportParameterType ParameterType => ReportParameterType.String;

        // Conditions

        public ReportParameterStringCondition Condition { get; set; } = ReportParameterStringCondition.Contains;

        public string Value { get; set; } = String.Empty;

        // Serialization

        public override void Read(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }
                else if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();

                    switch (propertyName)
                    {
                        case nameof(Condition):
                            Condition = (ReportParameterStringCondition)reader.GetInt32();
                            break;

                        case nameof(Value):
                            Value = reader.GetString() ?? String.Empty;
                            break;

                        default:
                            reader.Skip();
                            break;
                    }
                }
                else
                {
                    throw new JsonException();
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, JsonSerializerOptions options)
        {
            writer.WriteNumber(nameof(Condition), (int)Condition);
            writer.WriteString(nameof(Value), Value);
        }
    }
}
