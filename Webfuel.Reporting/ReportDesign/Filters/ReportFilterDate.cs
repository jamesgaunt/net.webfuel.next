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

        IsSet = 1000,
        IsNotSet = 1001,
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

            new ReportFilterCondition { Value = (int)ReportFilterDateCondition.IsSet, Description = "is set", Unary = true },
            new ReportFilterCondition { Value = (int)ReportFilterDateCondition.IsNotSet, Description = "is not set", Unary = true },
        };

        public string Value { get; set; } = String.Empty;

        public override async Task<bool> Validate(ReportSchema schema, IServiceProvider services)
        {
            if (!await base.Validate(schema, services))
                return false;

            Description = Condition switch
            {
                (int)ReportFilterDateCondition.IsNotSet => $"{DisplayName} {ConditionDescription}",
                (int)ReportFilterDateCondition.IsSet => $"{DisplayName} {ConditionDescription}",
                _ => $"{DisplayName} {ConditionDescription} {Value}",
            };
            return true;
        }

        public override async Task<bool> Apply(object context, ReportBuilder builder)
        {
            var argument = builder.Request.Arguments.FirstOrDefault(a => a.FilterId == Id);
            var condition = argument?.Condition ?? Condition;

            var field = builder.Schema.Fields.FirstOrDefault(f => f.Id == FieldId);
            if (field == null)
                return false;

            var untyped = await field.GetValue(context, builder);

            if (untyped == null)
                return condition == (int)ReportFilterDateCondition.IsNotSet;
            if (condition == (int)ReportFilterDateCondition.IsSet)
                return true;
            if(condition == (int)ReportFilterDateCondition.IsNotSet)
                return false;   

            if (untyped is not DateOnly typed)
                return false;

            var value = argument == null ? ParseDate(Value) : argument.DateValue;

            switch (condition)
            {
                case (int)ReportFilterDateCondition.EqualTo:
                    return typed == value;
                case (int)ReportFilterDateCondition.LessThan:
                    return typed < value;
                case (int)ReportFilterDateCondition.LessThanOrEqualTo:
                    return typed <= value;
                case (int)ReportFilterDateCondition.GreaterThan:
                    return typed > value;
                case (int)ReportFilterDateCondition.GreaterThanOrEqualTo:
                    return typed >= value;
            }

            throw new InvalidOperationException($"Unknown condition {condition}");
        }

        public override void Update(ReportFilter filter, ReportSchema schema)
        {
            if (filter is not ReportFilterDate typed)
                throw new Exception($"Cannot apply filter of type {filter.FilterType} to filter of type {FilterType}");

            Value = typed.Value;
            base.Update(filter, schema);
        }

        public override Task<ReportArgument?> GenerateArgument(ReportDesign design, IServiceProvider services)
        {
            return Task.FromResult<ReportArgument?>(new ReportArgument
            {
                Name = DisplayName,
                FilterId = Id,
                FieldId = FieldId,
                FieldType = ReportFieldType.Date,
                Condition = Condition,
                Conditions = Conditions.ToList(),
                DateValue = ParseDate(Value),
                ReportProviderId = design.ReportProviderId,
                FilterType = FilterType,
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

        // Date Parser

        DateOnly? ParseDate(string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            var dsl = value.ToLower().Trim();

            switch (dsl)
            {
                case "today":
                    return DateOnly.FromDateTime(DateTime.Today);
                case "yesterday":
                    return DateOnly.FromDateTime(DateTime.Today.AddDays(-1));
                case "start of month":
                    return DateOnly.FromDateTime(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1));
                case "end of month":
                    return DateOnly.FromDateTime(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1));
                case "start of year":
                    return DateOnly.FromDateTime(new DateTime(DateTime.Today.Year, 1, 1));
                case "end of year":
                    return DateOnly.FromDateTime(new DateTime(DateTime.Today.Year, 12, 31));
            }

            if (DateTime.TryParse(value, out DateTime date))
                return DateOnly.FromDateTime(date);

            throw new InvalidOperationException("Unable to parse date string '{value}' into a valid date.");
        }

    }
}
