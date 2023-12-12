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

        public override bool ValidateFilter(ReportSchema schema)
        {
            if (!Enum.IsDefined(Condition))
                Condition = ReportFilterReferenceCondition.OneOf;

            return base.ValidateFilter(schema);
        }

        public override async Task<bool> Apply(object context, ReportBuilder builder)
        {
            var field = builder.Schema.Fields.FirstOrDefault(f => f.Id == FieldId);
            if (field == null)
                return false;

            var value = await field.Extract(context, builder);
            if (value is not ReportReference reference)
                return false;

            switch (Condition)
            {
                case ReportFilterReferenceCondition.OneOf:
                    return Value.Contains(reference.Id);
                case ReportFilterReferenceCondition.NotOneOf:
                    return !Value.Contains(reference.Id);
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

        public override string GenerateDescription(ReportSchema schema)
        {
            return $"{FieldName} {GetConditionDescription()} ...";
        }

        string GetConditionDescription()
        {
            return Condition switch
            {
                ReportFilterReferenceCondition.OneOf => "One of",
                ReportFilterReferenceCondition.NotOneOf => "Not one of",
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
                return true;
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
