using DocumentFormat.OpenXml.Presentation;
using Microsoft.Identity.Client;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Reporting
{
    [ApiType]
    [JsonConverter(typeof(ReportFilterConverter))]
    public abstract class ReportFilter
    {
        public abstract ReportFilterType FilterType { get; }

        // Server + Client Side

        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public abstract string DisplayName { get; }

        public string Description { get; set; } = String.Empty;

        public bool Editable { get; set; } = false;

        // Server Side

        public abstract Task<bool> Apply(object context, ReportBuilder builder);

        public virtual void Update(ReportFilter filter, ReportSchema schema)
        {
            Name = filter.Name;
            Editable = filter.Editable;
            Condition = filter.Condition;
        }

        public virtual Task<bool> Validate(ReportSchema schema, IServiceProvider services)
        {
            if (Conditions.Count() > 0)
            {
                var condition = Conditions.FirstOrDefault(p => p.Value == Condition);
                if (condition == null)
                {
                    condition = Conditions.First();
                    Condition = condition.Value;
                }
                ConditionDescription = condition.Description;
            }
            else
            {
                Condition = 0;
                ConditionDescription = String.Empty;
            }

            return Task.FromResult(true);
        }

        public virtual Task<ReportArgument?> GenerateArgument(IServiceProvider services)
        {
            return Task.FromResult<ReportArgument?>(null);
        }

        // Conditions

        public abstract IEnumerable<ReportFilterCondition> Conditions { get; }

        public int Condition { get; set; } = 0;

        public string ConditionDescription { get; set; } = String.Empty;

        // Serialization

        public virtual bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(Id), propertyName, true) == 0)
            {
                Id = reader.GetGuid();
                return reader.Read();
            }

            if (String.Compare(nameof(Name), propertyName, true) == 0)
            {
                Name = reader.GetString() ?? String.Empty;
                return reader.Read();
            }

            if (String.Compare(nameof(Description), propertyName, true) == 0)
            {
                Description = reader.GetString() ?? String.Empty;
                return reader.Read();
            }

            if (String.Compare(nameof(Editable), propertyName, true) == 0)
            {
                Editable = reader.TokenType == JsonTokenType.True;
                return reader.Read();
            }

            if (String.Compare(nameof(Condition), propertyName, true) == 0)
            {
                Condition = reader.GetInt32();
                return reader.Read();
            }

            if (String.Compare(nameof(ConditionDescription), propertyName, true) == 0)
            {
                ConditionDescription = reader.GetString() ?? String.Empty;
                return reader.Read();
            }

            return false;
        }

        public virtual void WriteProperties(Utf8JsonWriter writer)
        {
            writer.WriteString("id", Id.ToString());
            writer.WriteString("name", Name);
            writer.WriteString("description", Description);
            writer.WriteString("displayName", DisplayName);
            writer.WriteBoolean("editable", Editable);

            writer.WriteNumber("condition", Condition);
            writer.WriteString("conditionDescription", ConditionDescription);
            writer.WritePropertyName("conditions");
            JsonSerializer.Serialize(writer, Conditions, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
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
            reader.Read();

            ReportFilter filter = propertyType switch
            {
                ReportFilterType.Date => new ReportFilterDate(),
                ReportFilterType.Group => new ReportFilterGroup(),
                ReportFilterType.String => new ReportFilterString(),
                ReportFilterType.Number => new ReportFilterNumber(),
                ReportFilterType.Boolean => new ReportFilterBoolean(),
                ReportFilterType.Reference => new ReportFilterReference(),
                ReportFilterType.Expression => new ReportFilterExpression(),
                _ => throw new JsonException()
            };

            while (reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException();

                var propertyName = reader.GetString() ?? String.Empty;
                reader.Read();

                if (!filter.ReadProperty(propertyName, ref reader))
                {
                    reader.Skip();
                    reader.Read();
                }
            }

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
