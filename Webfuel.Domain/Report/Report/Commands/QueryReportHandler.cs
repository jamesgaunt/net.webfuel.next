using MediatR;

namespace Webfuel.Domain
{
    internal class QueryReportHandler : IRequestHandler<QueryReport, QueryResult<Report>>
    {
        private readonly IReportRepository _reportRepository;

        public QueryReportHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<QueryResult<Report>> Handle(QueryReport request, CancellationToken cancellationToken)
        {
            return await _reportRepository.QueryReport(request.ApplyCustomFilters());
        }
    }
}
