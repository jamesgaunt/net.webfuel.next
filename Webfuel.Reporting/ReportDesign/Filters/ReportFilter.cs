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
    [JsonConverter(typeof(ReportFilterConverter))]
    public abstract class ReportFilter
    {
        public abstract ReportFilterType FilterType { get; }

        public abstract void ReadProperties(ref Utf8JsonReader reader, JsonSerializerOptions options);

        public abstract void WriteProperties(Utf8JsonWriter writer, JsonSerializerOptions options);
    }

    public class ReportFilterConverter : JsonConverter<ReportFilter>
    {
        public override ReportFilter Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException();

            var propertyName = reader.GetString();
            if(propertyName != "filterType")
                throw new JsonException();

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
                throw new JsonException();

            var propertyType = (ReportFilterType)reader.GetInt32();

            ReportFilter filter = propertyType switch
            {
                ReportFilterType.Group => new ReportFilterGroup(),
                ReportFilterType.String => new ReportFilterString(),
                ReportFilterType.Number => new ReportFilterNumber(),
                _ => throw new JsonException()
            };

            filter.ReadProperties(ref reader, options);

            if (reader.TokenType != JsonTokenType.EndObject)
                throw new JsonException();

            return filter;
        }

        public override void Write(Utf8JsonWriter writer, ReportFilter filter, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            {
                writer.WriteNumber("filterType", (int)filter.FilterType);
                filter.WriteProperties(writer, options);
            }
            writer.WriteEndObject();
        }
    }
}
