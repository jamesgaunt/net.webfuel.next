using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    [ApiType]
    [JsonConverter(typeof(ReportFilterConverter))]
    public abstract class ReportFilter
    {
        public abstract ReportFilterType FilterType { get; }

        public abstract ReportPrimativeType PrimativeType { get; }

        public Guid Id { get; set; }

        public string Description { get; internal set; } = String.Empty;

        public virtual bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(Id), propertyName, true) == 0)
            {
                Id = reader.GetGuid();
                return true;
            }

            if (String.Compare(nameof(Description), propertyName, true) == 0)
            {
                Description = reader.GetString() ?? String.Empty;
                return true;
            }

            return false;
        }

        public virtual void WriteProperties(Utf8JsonWriter writer)
        {
            writer.WriteString("id", Id.ToString());
            writer.WriteString("description", Description);
        }

        public virtual void ValidateFilter(ReportSchema schema)
        {
            Description = GenerateDescription(schema);
        }

        public abstract string GenerateDescription(ReportSchema schema);
    }

    public class ReportFilterConverter : JsonConverter<ReportFilter>
    {
        public override ReportFilter Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName || reader.GetString() != "filterType")
                throw new JsonException();

            reader.Read();
            var propertyType = (ReportFilterType)reader.GetInt32();

            ReportFilter filter = propertyType switch
            {
                ReportFilterType.Group => new ReportFilterGroup(),
                ReportFilterType.String => new ReportFilterString(),
                ReportFilterType.Number => new ReportFilterNumber(),
                _ => throw new JsonException()
            };

            while (reader.Read())
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                    break;

                var propertyName = reader.GetString() ?? String.Empty;

                reader.Read();
                if (!filter.ReadProperty(propertyName, ref reader))
                    reader.Skip();
            }

            if (reader.TokenType != JsonTokenType.EndObject)
                throw new JsonException();

            return filter;
        }

        public override void Write(Utf8JsonWriter writer, ReportFilter filter, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            {
                writer.WriteNumber("filterType", (int)filter.FilterType);
                filter.WriteProperties(writer);
            }
            writer.WriteEndObject();
        }
    }
}
