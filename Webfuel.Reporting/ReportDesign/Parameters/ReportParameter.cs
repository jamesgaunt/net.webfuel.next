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
    [JsonConverter(typeof(ReportParameterConverter))]
    public abstract class ReportParameter
    {
        public abstract ReportParameterType ParameterType { get; }

        public abstract void Read(ref Utf8JsonReader reader, JsonSerializerOptions options);

        public abstract void Write(Utf8JsonWriter writer, JsonSerializerOptions options);
    }

    public class ReportParameterConverter : JsonConverter<ReportParameter>
    {
        public override ReportParameter Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName || reader.GetString() != nameof(ReportParameter.ParameterType))
                throw new JsonException();

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
                throw new JsonException();

            var propertyType = (ReportParameterType)reader.GetInt32();

            ReportParameter parameter = propertyType switch
            {
                ReportParameterType.Group => new ReportParameterGroup(),
                ReportParameterType.String => new ReportParameterString(),
                _ => throw new JsonException()
            };
            parameter.Read(ref reader, options);
            reader.Read();

            return parameter;
        }

        public override void Write(Utf8JsonWriter writer, ReportParameter parameter, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            {
                writer.WriteNumber(nameof(ReportParameter.ParameterType), (int)parameter.ParameterType);
                parameter.Write(writer, options);
            }
            writer.WriteEndObject();
        }
    }
}
