using DocumentFormat.OpenXml.Office2010.Excel;
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
    public enum ReportFilterReferenceCondition
    {
        OneOf = 10,
        NotOneOf = 20,
    }

    [ApiType]
    public class ReportFilterReference : ReportFilterField
    {
        public override ReportFilterType FilterType => ReportFilterType.Reference;

        public ReportFilterReferenceCondition Condition { get; set; } = ReportFilterReferenceCondition.OneOf;

        public List<Guid> Value { get; set; } = new List<Guid>();

        public override async Task<bool> Validate(ReportSchema schema, IServiceProvider services)
        {
            if (!await base.Validate(schema, services))
                return false;

            if (!Enum.IsDefined(Condition))
                Condition = ReportFilterReferenceCondition.OneOf;

            var valueDescription = new StringBuilder();
            if (Value.Count > 0)
            {
                var field = schema.GetField(FieldId);
                if (field == null)
                    return false;

                var mapper = field.GetMapper(services);
                if (mapper == null)
                    return false;

                for (var i = 0; i < Value.Count; i++)
                {
                    var entity = await mapper.Get(Value[i]);
                    if (entity == null)
                    {
                        Value[i] = Guid.Empty; // flag for deletion
                        continue;
                    }

                    var name = mapper.DisplayName(entity);
                    if (valueDescription.Length > 0)
                        valueDescription.Append(", ");

                    valueDescription.Append(name);
                }
                Value.RemoveAll(v => v == Guid.Empty);
            }
            if (Value.Count == 0)
            {
                valueDescription.Append("(empty list)");
            }

            Description = $"{DisplayName} is {GetConditionDescription()} {valueDescription}";
            return true;
        }

        public override async Task<bool> Apply(object context, ReportBuilder builder)
        {
            var field = builder.Schema.Fields.FirstOrDefault(f => f.Id == FieldId);
            if (field == null)
                return false;

            if (field is not ReportReferenceField referenceField)
                throw new InvalidOperationException($"Field {field.Name} is not a reference field");

            var entities = await field.MapContextToEntities(context, builder);
            if (entities.Count == 0)
                return false;

            if (entities.Count > 1)
                throw new InvalidOperationException("Multi-value references not supported by this filter");

            var id = referenceField.GetMapper(builder.ServiceProvider).Id(entities[0]);

            switch (Condition)
            {
                case ReportFilterReferenceCondition.OneOf:
                    return Value.Contains(id);
                case ReportFilterReferenceCondition.NotOneOf:
                    return !Value.Contains(id);
            }

            throw new InvalidOperationException($"Unknown condition {Condition}");
        }

        public override void Update(ReportFilter filter, ReportSchema schema)
        {
            if (filter is not ReportFilterReference typed)
                throw new Exception($"Cannot apply filter of type {filter.FilterType} to filter of type {FilterType}");

            Value = typed.Value;
            Condition = typed.Condition;
            base.Update(filter, schema);
        }

        string GetConditionDescription()
        {
            return Condition switch
            {
                ReportFilterReferenceCondition.OneOf => "one of",
                ReportFilterReferenceCondition.NotOneOf => "not one of",
                _ => "One of",
            };
        }

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(Condition), propertyName, true) == 0)
            {
                Condition = (ReportFilterReferenceCondition)reader.GetInt32();
                return reader.Read();
            }

            if (String.Compare(nameof(Value), propertyName, true) == 0)
            {
                Value = JsonSerializer.Deserialize<List<Guid>>(ref reader, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                                       ?? new List<Guid>();
                return reader.Read();
            }

            return base.ReadProperty(propertyName, ref reader);
        }

        public override void WriteProperties(Utf8JsonWriter writer)
        {
            base.WriteProperties(writer);
            writer.WriteNumber("condition", (int)Condition);

            writer.WritePropertyName("value");
            JsonSerializer.Serialize(writer, Value, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
