using Webfuel.Domain.StaticData;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public interface ISupportRequestReportProvider: IReportProvider
    {
    }

    [Service(typeof(ISupportRequestReportProvider), typeof(IReportProvider))]
    internal class SupportRequestReportProvider : ISupportRequestReportProvider
    {
        public Guid Id => ReportProviderEnum.SupportRequest;

        public Task<ReportSchema> GetReportSchema()
        {
            return Task.FromResult(SupportRequestReportSchema.Schema);
        }

        public Task<ReportBuilder> InitialiseReport(ReportRequest request)
        {
            return Task.FromResult<ReportBuilder>(new SupportRequestReportBuilder(request));
        }
    }
}
