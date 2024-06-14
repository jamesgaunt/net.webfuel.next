using DocumentFormat.OpenXml.Drawing.Charts;

namespace Webfuel.Domain
{
    internal class AnnualReportColumn
    {
        public required int Index { get; set; }

        public required string Title { get; set; }

        public bool IsBoolean { get; set; }

        public bool IsOptional { get; set; }

        public List<string>? Values { get; set; }

        public AnnualReportColumn WithValues(params string[] values)
        {
            Values = values.ToList();
            return this;
        }
    }
}
