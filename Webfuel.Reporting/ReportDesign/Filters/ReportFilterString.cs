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
    public enum ReportFilterStringCondition
    {
        Contains = 10,
        StartsWith = 20,
        EndsWith = 30,
        EqualTo = 40,

        IsEmpty = 100,
        IsNotEmpty = 200,
    }

    [ApiType]
    public class ReportFilterString : ReportFilterField
    {
        public override ReportFilterType FilterType => ReportFilterType.String;

        public ReportFilterStringCondition Condition { get; set; } = ReportFilterStringCondition.Contains;

        public string Value { get; set; } = String.Empty;

        public override async Task<bool> Validate(ReportSchema schema, IServiceProvider services)
        {
            if (!await base.Validate(schema, services))
                return false;

            if (!Enum.IsDefined(Condition))
                Condition = ReportFilterStringCondition.Contains;

            Description = Condition switch
            {
                ReportFilterStringCondition.IsEmpty => $"{DisplayName} is empty",
                ReportFilterStringCondition.IsNotEmpty => $"{DisplayName} is not empty",
                _ => $"{DisplayName} {GetConditionDescription()} {(String.IsNullOrEmpty(Value) ? "(empty string)" : Value)}",
            };
            return true;
        }

        public override async Task<bool> Apply(object context, ReportBuilder builder)
        {
            var field = builder.Schema.Fields.FirstOrDefault(f => f.Id == FieldId);
            if (field == null)
                return false;

            var value = await field.Evaluate(context, builder);

            var typed = value?.ToString() ?? String.Empty;

            switch (Condition)
            {               
                case ReportFilterStringCondition.Contains:
                    return typed.Contains(Value);
                case ReportFilterStringCondition.StartsWith:
                    return typed.StartsWith(Value);
                case ReportFilterStringCondition.EndsWith:
                    return typed.EndsWith(Value);
                case ReportFilterStringCondition.EqualTo:
                    return typed == Value;
                case ReportFilterStringCondition.IsEmpty:
                    return typed == String.Empty;
                case ReportFilterStringCondition.IsNotEmpty:
                    return typed != String.Empty;
            }

            throw new InvalidOperationException($"Unknown condition {Condition}");
        }

        public override void Update(ReportFilter filter, ReportSchema schema)
        {
            if (filter is not ReportFilterString typed)
                throw new Exception($"Cannot apply filter of type {filter.FilterType} to filter of type {FilterType}");

            Value = typed.Value;
            Condition = typed.Condition;
            base.Update(filter, schema);
        }

        string GetConditionDescription()
        {
            return Condition switch
            {
                ReportFilterStringCondition.Contains => "contains",
                ReportFilterStringCondition.StartsWith => "starts with",
                ReportFilterStringCondition.EndsWith => "ends with",
                ReportFilterStringCondition.EqualTo => "is equal to",
                ReportFilterStringCondition.IsEmpty => "is empty",
                ReportFilterStringCondition.IsNotEmpty => "is not empty",
                _ => "contains",
            };
        }

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(Condition), propertyName, true) == 0)
            {
                Condition = (ReportFilterStringCondition)reader.GetInt32();
                return reader.Read();
            }

            if (String.Compare(nameof(Value), propertyName, true) == 0)
            {
                Value = reader.GetString() ?? String.Empty;
                return reader.Read();
            }

            return base.ReadProperty(propertyName, ref reader);
        }

        public override void WriteProperties(Utf8JsonWriter writer)
        {
            base.WriteProperties(writer);
            writer.WriteNumber("condition", (int)Condition);
            writer.WriteString("value", Value);
        }
    }
}
