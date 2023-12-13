
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

        public string Expression { get; set; } = String.Empty;

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
            return Task.FromResult(true);
        }
    }
}
