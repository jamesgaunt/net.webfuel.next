namespace Webfuel.Reporting
{
    [ApiType]
    public class ReportColumn
    {
        public Guid FieldId { get; set; }

        public string Title { get; set; } = String.Empty;
    }
}
