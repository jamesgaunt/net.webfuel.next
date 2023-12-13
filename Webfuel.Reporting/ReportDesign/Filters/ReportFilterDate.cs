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

        public ReportFilterDateCondition Condition { get; set; } = ReportFilterDateCondition.EqualTo;

        public string Value { get; set; } = String.Empty;

        public override bool ValidateFilter(ReportSchema schema)
        {
            if (!Enum.IsDefined(Condition))
                Condition = ReportFilterDateCondition.EqualTo;

            return  base.ValidateFilter(schema);
        }

        public override async Task<bool> Apply(object context, ReportBuilder builder)
        {
            var field = builder.Schema.Fields.FirstOrDefault(f => f.Id == FieldId);
            if (field == null)
                return false;

            var value = await field.Extract(context, builder);

            throw new NotImplementedException();
        }

        public override void Update(ReportFilter filter, ReportSchema schema)
        {
            if (filter is not ReportFilterDate typed)
                throw new Exception($"Cannot apply filter of type {filter.FilterType} to filter of type {FilterType}");

            Value = typed.Value;
            Condition = typed.Condition;
            base.Update(filter, schema);
        }

        public override string GenerateDescription(ReportSchema schema)
        {
            return $"{FieldName} {GetConditionDescription()} {Value}";
        }

        string GetConditionDescription()
        {
            return Condition switch
            {
                ReportFilterDateCondition.EqualTo => "=",
                ReportFilterDateCondition.LessThan => "<",
                ReportFilterDateCondition.LessThanOrEqualTo => "<=",
                ReportFilterDateCondition.GreaterThan => ">",
                ReportFilterDateCondition.GreaterThanOrEqualTo => ">=",
                _ => "=",
            };
        }

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(Condition), propertyName, true) == 0)
            {
                Condition = (ReportFilterDateCondition)reader.GetInt32();
                return reader.Read();
            }

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
            writer.WriteNumber("condition", (int)Condition);
            writer.WriteString("value", Value);
        }
    }
}
