using MediatR;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    internal class ListHeadReportHandler : IRequestHandler<ListHeadReport, List<Report>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IReportGroupRepository _reportGroupRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public ListHeadReportHandler(IReportRepository reportRepository, IReportGroupRepository reportGroupRepository, IIdentityAccessor identityAccessor)
        {
            _reportRepository = reportRepository;
            _reportGroupRepository = reportGroupRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task<List<Report>> Handle(ListHeadReport request, CancellationToken cancellationToken)
        {
            var reports = await _reportRepository.QueryReport(new Query
            {
                Projection = new List<string> { 
                    nameof(Report.Id),
                    nameof(Report.Name),
                    nameof(Report.ReportGroupId),
                    nameof(Report.ReportProviderId),
                    nameof(Report.OwnerUserId)
                }
            });
            return reports.Items;
        }
    }
}
