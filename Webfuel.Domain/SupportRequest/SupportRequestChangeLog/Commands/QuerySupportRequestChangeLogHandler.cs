using MediatR;

namespace Webfuel.Domain
{
    internal class QuerySupportRequestChangeLogHandler : IRequestHandler<QuerySupportRequestChangeLog, QueryResult<SupportRequestChangeLog>>
    {
        private readonly ISupportRequestChangeLogRepository _supportRequestChangeLogRepository;

        public QuerySupportRequestChangeLogHandler(ISupportRequestChangeLogRepository supportRequestChangeLogRepository)
        {
            _supportRequestChangeLogRepository = supportRequestChangeLogRepository;
        }

        public async Task<QueryResult<SupportRequestChangeLog>> Handle(QuerySupportRequestChangeLog request, CancellationToken cancellationToken)
        {
            return await _supportRequestChangeLogRepository.QuerySupportRequestChangeLog(request.ApplyCustomFilters());
        }
    }
}
