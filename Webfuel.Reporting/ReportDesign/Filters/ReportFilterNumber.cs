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

        public double? Value { get; set; }

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
                    Value = reader.GetDouble();
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

        public override void ValidateFilter(ReportSchema schema)
        {
            if (!Enum.IsDefined(Condition))
                Condition = ReportFilterNumberCondition.EqualTo;

            base.ValidateFilter(schema);
        }
        public override void Apply(ReportFilter filter, ReportSchema schema)
        {
            if (filter is not ReportFilterNumber typed)
                throw new Exception($"Cannot apply filter of type {filter.FilterType} to filter of type {FilterType}");

            Value = typed.Value;
            Condition = typed.Condition;
            base.Apply(filter, schema);
        }
    }
}
