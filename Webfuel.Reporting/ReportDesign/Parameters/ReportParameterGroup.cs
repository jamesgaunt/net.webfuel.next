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
    [ApiEnum]
    public enum ReportParameterGroupCondition
    {
        All = 10,
        Any = 20,
        None = 30,
    }

    [ApiType]
    public class ReportParameterGroup : ReportParameter
    {
        public override ReportParameterType ParameterType => ReportParameterType.Group;

        // Conditions

        public ReportParameterGroupCondition Condition { get; set; } = ReportParameterGroupCondition.All;

        public List<ReportParameter> Parameters { get; set; } = new List<ReportParameter>();

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
                            Condition = (ReportParameterGroupCondition)reader.GetInt32();
                            break;

                        case nameof(Parameters):

                            if (reader.TokenType != JsonTokenType.StartArray)
                                throw new JsonException();
                            reader.Read();

                            while(reader.TokenType != JsonTokenType.EndArray)
                            {
                                var parameter = JsonSerializer.Deserialize<ReportParameter>(ref reader, options);
                                if(parameter != null)
                                    Parameters.Add(parameter);
                            }
                            reader.Read();
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

            writer.WriteStartArray(nameof(Parameters));
            {
                foreach(var parameter in Parameters)
                {
                    JsonSerializer.Serialize(writer, parameter, options);
                }
            }
            writer.WriteEndArray();
        }
    }
}
