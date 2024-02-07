using MediatR;

namespace Webfuel.Domain
{
    internal class QueryReportHandler : IRequestHandler<QueryReport, QueryResult<Report>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IIdentityAccessor _identityAccessor;

        public QueryReportHandler(IReportRepository reportRepository, IIdentityAccessor identityAccessor)
        {
            _reportRepository = reportRepository;
            _identityAccessor = identityAccessor;
        }

        public async Task<QueryResult<Report>> Handle(QueryReport request, CancellationToken cancellationToken)
        {
            if (_identityAccessor.User == null)
                throw new UnauthorizedAccessException("User is not authenticated");

            var query = request.ApplyCustomFilters();

            query.Any(q =>
            {
                q.Equal(nameof(Report.OwnerUserId), _identityAccessor.User.Id);
                q.Equal(nameof(Report.IsPublic), true, request.OwnReportsOnly == "NO");
            });

            return await _reportRepository.QueryReport(query);
        }
    }
}
