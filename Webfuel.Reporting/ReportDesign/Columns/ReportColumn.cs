
namespace Webfuel.Reporting
{
    [ApiType]
    public class ReportColumn
    {
        public Guid Id { get; set; }

        public Guid FieldId { get; set; }

        public string FieldName { get; set; } = String.Empty;

        public ReportFieldType FieldType { get; set; } = ReportFieldType.Unspecified;

        public bool MultiValued { get; set; }

        public string Title { get; set; } = String.Empty;

        public double? Width { get; set; }

        public bool Bold { get; set; }

        public string Expression { get; set; } = String.Empty;

        public ReportColumnCollection Collection { get; set; } = ReportColumnCollection.Default;

        internal Task<bool> Validate(ReportSchema schema, IServiceProvider services)
        {
            if (FieldId == ReportColumnTypeIdentifiers.Expression)
            {
                // TODO: Validate the expression
                return Task.FromResult(true);
            }

            var field = schema.GetField(FieldId);
            if (field == null)
                return Task.FromResult(false);

            FieldName = field.Name;
            FieldType = field.FieldType;
            MultiValued = field.MultiValued;

            if (!Enum.IsDefined<ReportColumnCollection>(Collection))
                Collection = ReportColumnCollection.Default;

            if(Width < 0)
                Width = null;
            if (Width > 200)
                Width = 200;

            return Task.FromResult(true);
        }
    }
}
