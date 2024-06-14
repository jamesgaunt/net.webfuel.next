namespace Webfuel.Domain
{
    public class AnnualReportSettings
    {
        public required DateOnly? FromDate { get; set; }

        public required DateOnly? ToDate { get; set; }

        public required bool HighlightInvalidCells { get; set; }
    }
}
