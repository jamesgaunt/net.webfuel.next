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

        public override ReportPrimativeType PrimativeType => ReportPrimativeType.Number;

        public ReportFilterNumberCondition Condition { get; set; } = ReportFilterNumberCondition.EqualTo;

        public decimal? Value { get; set; }

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(Condition), propertyName, true) == 0)
            {
                Condition = (ReportFilterNumberCondition)reader.GetInt32();
                return true;
            }

            if (String.Compare(nameof(Value), propertyName, true) == 0)
            {
                if (reader.TokenType == JsonTokenType.Number)
                    Value = reader.GetDecimal();
                return true;
            }


            return base.ReadProperty(propertyName, ref reader);
        }
        
        public override void WriteProperties(Utf8JsonWriter writer)
        {
            base.WriteProperties(writer);
            writer.WriteNumber("condition", (int)Condition);

            if(Value == null)
                writer.WriteNull("value");
            else
                writer.WriteNumber("value", Value.Value);
        }

        // Validation

        public override void ValidateFilter(ReportSchema schema)
        {
            if (!Enum.IsDefined(Condition))
                Condition = ReportFilterNumberCondition.EqualTo;

            base.ValidateFilter(schema);
        }

        // Description

        public override string GenerateDescription(ReportSchema schema)
        {
            return $"{GenerateFieldName(schema)} {GenerateConditionDescription()} {GenerateValueDescription()}";
        }

        string GenerateFieldName(ReportSchema schema)
        {
            var field = schema.Fields.FirstOrDefault(f => f.Id == FieldId);
            if (field == null)
                return "Unknown field";
            return field.Name;
        }

        string GenerateConditionDescription()
        {
            return Condition switch
            {
                ReportFilterNumberCondition.EqualTo => "is equal to",
                ReportFilterNumberCondition.LessThan => "is less than",
                ReportFilterNumberCondition.LessThanOrEqualTo => "is less than or equal to",
                ReportFilterNumberCondition.GreaterThan => "is greater than",
                ReportFilterNumberCondition.GreaterThanOrEqualTo => "is greater than or equal to",
                _ => "is equal to"
            };
        }

        string GenerateValueDescription()
        {
            if (Value == null)
                return "null";
            return Value.Value.ToString("N2");
        }
    }
}
