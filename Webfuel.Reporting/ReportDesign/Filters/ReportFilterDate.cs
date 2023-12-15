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
    public enum ReportFilterDateCondition
    {
        EqualTo = 10,
        LessThan = 20,
        LessThanOrEqualTo = 30,
        GreaterThan = 40,
        GreaterThanOrEqualTo = 50,
    }

    [ApiType]
    public class ReportFilterDate : ReportFilterField
    {
        public override ReportFilterType FilterType => ReportFilterType.Date;

        public override IEnumerable<ReportFilterCondition> Conditions => new List<ReportFilterCondition>()
        {
            new ReportFilterCondition { Value = (int)ReportFilterDateCondition.EqualTo, Description = "is equal to", Unary = false },
            new ReportFilterCondition { Value = (int)ReportFilterDateCondition.LessThan, Description = "is less than", Unary = false },
            new ReportFilterCondition { Value = (int)ReportFilterDateCondition.LessThanOrEqualTo, Description = "is less than or equal to", Unary = false },
            new ReportFilterCondition { Value = (int)ReportFilterDateCondition.GreaterThan, Description = "is greater than", Unary = false },
            new ReportFilterCondition { Value = (int)ReportFilterDateCondition.GreaterThanOrEqualTo, Description = "is greater than or equal to", Unary = false },
        };

        public string Value { get; set; } = String.Empty;

        public override async Task<bool> Validate(ReportSchema schema, IServiceProvider services)
        {
            if (!await base.Validate(schema, services))
                return false;

            Description = $"{DisplayName} {ConditionDescription} {Value}";
            return true;
        }

        public override async Task<bool> Apply(object context, ReportBuilder builder)
        {
            var field = builder.Schema.Fields.FirstOrDefault(f => f.Id == FieldId);
            if (field == null)
                return false;

            var value = await field.Evaluate(context, builder);

            throw new NotImplementedException();
        }

        public override void Update(ReportFilter filter, ReportSchema schema)
        {
            if (filter is not ReportFilterDate typed)
                throw new Exception($"Cannot apply filter of type {filter.FilterType} to filter of type {FilterType}");

            Value = typed.Value;
            base.Update(filter, schema);
        }

        public override Task<ReportArgument?> GenerateArgument(IServiceProvider services)
        {
            return Task.FromResult<ReportArgument?>(new ReportArgument
            {
                Name = DisplayName,
                FilterId = Id,
                FieldId = FieldId,
                FieldType = ReportFieldType.Date,
                Condition = Condition,
                Conditions = Conditions.ToList(),
                DateValue = DateOnly.FromDateTime(DateTime.Today) // TODO: Date generation DSL
            });
        }

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(Value), propertyName, true) == 0)
            {
                if (reader.TokenType == JsonTokenType.String)
                    Value = reader.GetString() ?? String.Empty;
                else
                    throw new JsonException();

                return reader.Read();
            }

            return base.ReadProperty(propertyName, ref reader);
        }

        public override void WriteProperties(Utf8JsonWriter writer)
        {
            base.WriteProperties(writer);
            writer.WriteString("value", Value);
        }
    }
}
