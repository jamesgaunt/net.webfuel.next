using MediatR;

namespace Webfuel.Domain
{
    internal class QueryReportGroupHandler : IRequestHandler<QueryReportGroup, QueryResult<ReportGroup>>
    {
        private readonly IReportGroupRepository _reportGroupRepository;

        public QueryReportGroupHandler(IReportGroupRepository reportGroupRepository)
        {
            _reportGroupRepository = reportGroupRepository;
        }

        public async Task<QueryResult<ReportGroup>> Handle(QueryReportGroup request, CancellationToken cancellationToken)
        {
            return await _reportGroupRepository.QueryReportGroup(request.ApplyCustomFilters());
        }
    }
}
