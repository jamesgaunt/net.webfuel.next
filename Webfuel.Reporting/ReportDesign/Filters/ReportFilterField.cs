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

        public string FieldName { get; set; } = String.Empty;

        public bool MultiValued { get; set; } = false;

        public override Task<bool> Validate(ReportSchema schema, IServiceProvider services)
        {
            var field = schema.GetField(FieldId);
            if (field == null)
                return Task.FromResult(false);

            FieldName = field.Name;
            MultiValued = field.MultiValued;

            return base.Validate(schema, services);
        }

        public override string DisplayName => String.IsNullOrWhiteSpace(Name) ? FieldName : Name;

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(FieldId), propertyName, true) == 0)
            {
                FieldId = reader.GetGuid();
                return reader.Read();
            }

            if (String.Compare(nameof(FieldName), propertyName, true) == 0)
            {
                FieldName = reader.GetString() ?? String.Empty;
                return reader.Read();
            }

            return base.ReadProperty(propertyName, ref reader);
        }

        public override void WriteProperties(Utf8JsonWriter writer)
        {
            base.WriteProperties(writer);
            writer.WriteString("fieldId", FieldId);
            writer.WriteString("fieldName", FieldName);
        }
    }
}
