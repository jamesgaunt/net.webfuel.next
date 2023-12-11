using DocumentFormat.OpenXml.Spreadsheet;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    [ApiType]
    public abstract class ReportFilterField : ReportFilter
    {
        // Properties

        public Guid FieldId { get; set; }

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(FieldId), propertyName, true) == 0)
            {
                FieldId = reader.GetGuid();
                return true;
            }

            return base.ReadProperty(propertyName, ref reader);
        }
        
        public override void WriteProperties(Utf8JsonWriter writer)
        {
            base.WriteProperties(writer);
            writer.WriteString("fieldId", FieldId);
        }

        public override void ValidateFilter(ReportSchema schema)
        {
            DefaultName = GetFieldName(schema);
            base.ValidateFilter(schema);
        }

        protected string GetFieldName(ReportSchema schema)
        {
            var field = schema.Fields.FirstOrDefault(f => f.Id == FieldId);
            if (field == null)
                return "Unknown field";
            return field.Name;
        }
    }
}
