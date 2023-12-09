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

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(Condition), propertyName, true) == 0)
            {
                Condition = (ReportFilterStringCondition)reader.GetInt32();
                return true;
            }

            if (String.Compare(nameof(Value), propertyName, true) == 0)
            {
                Value = reader.GetString() ?? String.Empty;
                return true;
            }

            return base.ReadProperty(propertyName, ref reader);
        }

        public override void WriteProperties(Utf8JsonWriter writer)
        {
            base.WriteProperties(writer);
            writer.WriteNumber("condition", (int)Condition);
            writer.WriteString("value", Value);
        }

        // Validation

        public override void ValidateFilter(ReportSchema schema)
        {
            if (!Enum.IsDefined(Condition))
                Condition = ReportFilterStringCondition.Contains;

            base.ValidateFilter(schema);
        }

        // Description

        public override string GenerateDescription(ReportSchema schema)
        {
            return GenerateFieldName(schema) + " " + Condition switch
            {
                ReportFilterStringCondition.StartsWith => $"starts with '{Value}'",
                ReportFilterStringCondition.EndsWith => $"ends with '{Value}'",
                ReportFilterStringCondition.IsEmpty => "is empty",
                ReportFilterStringCondition.IsNotEmpty => "is not empty",
                _ => $"contains '{Value}'"
            };
        }

        string GenerateFieldName(ReportSchema schema)
        {
            var field = schema.Fields.FirstOrDefault(f => f.Id == FieldId);
            if (field == null)
                return "Unknown field";
            return field.Name;
        }
    }
}
