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
    }

    [ApiType]
    public class ReportFilterNumber : ReportFilterField
    {
        public override ReportFilterType FilterType => ReportFilterType.Number;

        public ReportFilterNumberCondition Condition { get; set; } = ReportFilterNumberCondition.EqualTo;

        public double Value { get; set; }

        public override async Task<bool> Validate(ReportSchema schema, IServiceProvider services)
        {
            if (!await base.Validate(schema, services))
                return false;

            if (!Enum.IsDefined(Condition))
                Condition = ReportFilterNumberCondition.EqualTo;

            Description = $"{DisplayName} is {GetConditionDescription()} {Value}";
            return true;
        }

        public override async Task<bool> Apply(object context, ReportBuilder builder)
        {
            var field = builder.Schema.Fields.FirstOrDefault(f => f.Id == FieldId);
            if (field == null)
                return false;

            var value = await field.Evaluate(context, builder);

            var typed = ToDouble(value);
            if (typed == null)
                return false;

            switch(Condition)
            {
                case ReportFilterNumberCondition.EqualTo:
                    return Value == typed;
                case ReportFilterNumberCondition.LessThan:
                    return Value > typed;
                case ReportFilterNumberCondition.LessThanOrEqualTo:
                    return Value >= typed;
                case ReportFilterNumberCondition.GreaterThan:
                    return Value < typed;
                case ReportFilterNumberCondition.GreaterThanOrEqualTo:
                    return Value <= typed;
            }

            throw new InvalidOperationException($"Unknown condition {Condition}");
        }

        public override void Update(ReportFilter filter, ReportSchema schema)
        {
            if (filter is not ReportFilterNumber typed)
                throw new Exception($"Cannot apply filter of type {filter.FilterType} to filter of type {FilterType}");

            Value = typed.Value;
            Condition = typed.Condition;
            base.Update(filter, schema);
        }

        string GetConditionDescription()
        {
            return Condition switch
            {
                ReportFilterNumberCondition.EqualTo => "=",
                ReportFilterNumberCondition.LessThan => "<",
                ReportFilterNumberCondition.LessThanOrEqualTo => "<=",
                ReportFilterNumberCondition.GreaterThan => ">",
                ReportFilterNumberCondition.GreaterThanOrEqualTo => ">=",
                _ => "INVALID",
            };
        }

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(Condition), propertyName, true) == 0)
            {
                Condition = (ReportFilterNumberCondition)reader.GetInt32();
                return reader.Read();
            }

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
            writer.WriteNumber("condition", (int)Condition);
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
