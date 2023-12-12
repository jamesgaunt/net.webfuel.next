using DocumentFormat.OpenXml.Office2010.Excel;

namespace Webfuel.Reporting
{
    [ApiType]
    public class ReportColumn
    {
        public Guid Id { get; set; }

        public Guid FieldId { get; set; }

        public string FieldName { get; set; } = String.Empty;

        public string Title { get; set; } = String.Empty;

        // TODO:

        public double? Width { get; set; }

        public string Format { get; set; } = String.Empty;

        public string? Expression { get; set; }

        internal bool ValidateColumn(ReportSchema schema)
        {
            var field = schema.GetField(FieldId);
            if (field == null)
                return false;

            FieldName = field.Name;
            return true;
        }
    }
}
