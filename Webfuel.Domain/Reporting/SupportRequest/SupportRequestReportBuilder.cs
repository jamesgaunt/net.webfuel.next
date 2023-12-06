using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class SupportRequestReportBuilder : StandardReportBuilder
    {
        public SupportRequestReportBuilder(ReportRequest request):
            base(SupportRequestReportSchema.Schema, request, new SupportRequestReportQuery())
        {
        }
    }
}
