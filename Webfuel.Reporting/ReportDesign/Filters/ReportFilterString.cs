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

        public override IEnumerable<ReportFilterCondition> Conditions => new List<ReportFilterCondition>()
        {
            new ReportFilterCondition { Value = (int)ReportFilterStringCondition.Contains, Description = "contains", Unary = false },
            new ReportFilterCondition { Value = (int)ReportFilterStringCondition.StartsWith, Description = "starts with", Unary = false },
            new ReportFilterCondition { Value = (int)ReportFilterStringCondition.EndsWith, Description = "ends with", Unary = false },
            new ReportFilterCondition { Value = (int)ReportFilterStringCondition.EqualTo, Description = "is equal to", Unary = false },

            new ReportFilterCondition { Value = (int)ReportFilterStringCondition.IsEmpty, Description = "is empty", Unary = true },
            new ReportFilterCondition { Value = (int)ReportFilterStringCondition.IsNotEmpty, Description = "is not empty", Unary = true },
        };

        public string Value { get; set; } = String.Empty;

        public override async Task<bool> Validate(ReportSchema schema, IServiceProvider services)
        {
            if (!await base.Validate(schema, services))
                return false;

            Description = Condition switch
            {
                (int)ReportFilterStringCondition.IsEmpty => $"{DisplayName} {ConditionDescription}",
                (int)ReportFilterStringCondition.IsNotEmpty => $"{DisplayName} {ConditionDescription}",
                _ => $"{DisplayName} {ConditionDescription} {(String.IsNullOrEmpty(Value) ? "(empty string)" : Value)}",
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
                case (int)ReportFilterStringCondition.Contains:
                    return typed.Contains(Value);
                case (int)ReportFilterStringCondition.StartsWith:
                    return typed.StartsWith(Value);
                case (int)ReportFilterStringCondition.EndsWith:
                    return typed.EndsWith(Value);
                case (int)ReportFilterStringCondition.EqualTo:
                    return typed == Value;
                case (int)ReportFilterStringCondition.IsEmpty:
                    return typed == String.Empty;
                case (int)ReportFilterStringCondition.IsNotEmpty:
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

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
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
            writer.WriteString("value", Value);
        }
    }
}
