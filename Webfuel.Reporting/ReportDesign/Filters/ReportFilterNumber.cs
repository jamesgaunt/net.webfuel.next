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
    public enum ReportFilterNumberCondition
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
    public class ReportFilterNumber : ReportFilterField
    {
        public override ReportFilterType FilterType => ReportFilterType.Number;

        public override IEnumerable<ReportFilterCondition> Conditions => new List<ReportFilterCondition>()
        {
            new ReportFilterCondition { Value = (int)ReportFilterNumberCondition.EqualTo, Description = "is equal to", Unary = false },
            new ReportFilterCondition { Value = (int)ReportFilterNumberCondition.LessThan, Description = "is less than", Unary = false },
            new ReportFilterCondition { Value = (int)ReportFilterNumberCondition.LessThanOrEqualTo, Description = "is less than or equal to", Unary = false },
            new ReportFilterCondition { Value = (int)ReportFilterNumberCondition.GreaterThan, Description = "is greater than", Unary = false },
            new ReportFilterCondition { Value = (int)ReportFilterNumberCondition.GreaterThanOrEqualTo, Description = "is greater than or equal to", Unary = false },
            
            new ReportFilterCondition { Value = (int)ReportFilterNumberCondition.IsSet, Description = "is set", Unary = true },
            new ReportFilterCondition { Value = (int)ReportFilterNumberCondition.IsNotSet, Description = "is not set", Unary = true },
        };

        public double Value { get; set; }

        public override async Task<bool> Validate(ReportSchema schema, IServiceProvider services)
        {
            if (!await base.Validate(schema, services))
                return false;

            Description = Condition switch
            {
                (int)ReportFilterNumberCondition.IsNotSet => $"{DisplayName} {ConditionDescription}",
                (int)ReportFilterNumberCondition.IsSet => $"{DisplayName} {ConditionDescription}",
                _ => $"{DisplayName} {ConditionDescription} {Value}",
            };
            return true;
        }

        public override async Task<bool> Apply(object context, ReportBuilder builder)
        {
            var argument = builder.Request.Arguments.FirstOrDefault(a => a.FilterId == Id);
            var condition = argument?.Condition ?? Condition;
            var value = argument == null ? Value : argument.DoubleValue;

            var field = builder.Schema.Fields.FirstOrDefault(f => f.Id == FieldId);
            if (field == null)
                return false;

            var untyped = await field.Evaluate(context, builder);

            if (untyped == null)
                return condition == (int)ReportFilterNumberCondition.IsNotSet;
            if (condition == (int)ReportFilterNumberCondition.IsSet)
                return true;
            if (condition == (int)ReportFilterNumberCondition.IsNotSet)
                return false;

            var typed = ToDouble(untyped);
            if (typed == null)
                return false;

            switch(condition)
            {
                case (int)ReportFilterNumberCondition.EqualTo:
                    if (value == null)
                        return true;
                    return value == typed;
                case (int)ReportFilterNumberCondition.LessThan:
                    if (value == null)
                        return true;
                    return value > typed;
                case (int)ReportFilterNumberCondition.LessThanOrEqualTo:
                    if (value == null)
                        return true;
                    return value >= typed;
                case (int)ReportFilterNumberCondition.GreaterThan:
                    if (value == null)
                        return true;
                    return value < typed;
                case (int)ReportFilterNumberCondition.GreaterThanOrEqualTo:
                    if (value == null)
                        return true;
                    return value <= typed;
            }

            throw new InvalidOperationException($"Unknown condition {condition}");
        }

        public override void Update(ReportFilter filter, ReportSchema schema)
        {
            if (filter is not ReportFilterNumber typed)
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
                FieldType = ReportFieldType.Number,
                Condition = Condition,
                Conditions = Conditions.ToList(),
                DoubleValue = Value,
                ReportProviderId = design.ReportProviderId,
                FilterType = FilterType,
            });
        }

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(Value), propertyName, true) == 0)
            {
                if (reader.TokenType == JsonTokenType.Number)
                    Value = reader.GetDouble();
                else
                    throw new JsonException();

                return reader.Read();
            }

            return base.ReadProperty(propertyName, ref reader);
        }

        public override void WriteProperties(Utf8JsonWriter writer)
        {
            base.WriteProperties(writer);
            writer.WriteNumber("value", Value);
        }

        // Helpers  

        static double? ToDouble(object? value)
        {
            if(value == null || !IsNumericType(value.GetType()))
                return null;    
            return Convert.ToDouble(value);
        }

        static bool IsNumericType(Type type)
        {
            return
                type == typeof(Byte) ||
                type == typeof(SByte) ||
                type == typeof(UInt16) ||
                type == typeof(UInt32) ||
                type == typeof(UInt64) ||
                type == typeof(Int16) ||
                type == typeof(Int32) ||
                type == typeof(Int64) ||
                type == typeof(Decimal) ||
                type == typeof(Double) ||
                type == typeof(Single);
        }
    }
}
