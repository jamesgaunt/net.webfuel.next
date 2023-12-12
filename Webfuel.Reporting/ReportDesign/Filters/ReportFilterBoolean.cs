using DocumentFormat.OpenXml.Presentation;
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
    [ApiType]
    public class ReportFilterBoolean : ReportFilterField
    {
        public override ReportFilterType FilterType => ReportFilterType.Boolean;

        public bool Value { get; set; } = true;

        public override bool ValidateFilter(ReportSchema schema)
        {
            return base.ValidateFilter(schema);
        }

        public override async Task<bool> Apply(object context, ReportBuilder builder)
        {
            var field = builder.Schema.Fields.FirstOrDefault(f => f.Id == FieldId);
            if (field == null)
                return false;

            var value = await field.Extract(context, builder);

            if (value is not bool typed)
                return false;

            return Value == typed;
        }

        public override void Update(ReportFilter filter, ReportSchema schema)
        {
            if (filter is not ReportFilterBoolean typed)
                throw new Exception($"Cannot apply filter of type {filter.FilterType} to filter of type {FilterType}");

            Value = typed.Value;
            base.Update(filter, schema);
        }

        public override string GenerateDescription(ReportSchema schema)
        {
            return $"{FieldName} is {(Value ? "True" : "False")}";
        }

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(Value), propertyName, true) == 0)
            {
                if (reader.TokenType == JsonTokenType.True)
                    Value = true;
                else if (reader.TokenType == JsonTokenType.False)
                    Value = false;
                else
                    throw new JsonException();

                return reader.Read();
            }

            return base.ReadProperty(propertyName, ref reader);
        }

        public override void WriteProperties(Utf8JsonWriter writer)
        {
            base.WriteProperties(writer);
            writer.WriteBoolean("value", Value);
        }
    }
}
