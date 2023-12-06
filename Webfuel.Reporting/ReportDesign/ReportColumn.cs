namespace Webfuel.Reporting
{
    [ApiType]
    public class ReportColumn
    {
        public Guid FieldId { get; set; }

        public string Title { get; set; } = String.Empty;


        // TODO:

        public double? Width { get; set; }

        public string Format { get; set; } = String.Empty;
    }
}
