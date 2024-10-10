using MediatR;
using System.Security.Principal;

namespace Webfuel.Domain
{
    public class QueryReport : Query, IRequest<QueryResult<Report>>
    {
        public string Name { get; set; } = String.Empty;

        public bool PublicReports { get; set; }

        public bool AllReports { get; set; }

        public Query ApplyCustomFilters(IIdentityAccessor identityAccessor)
        {
            if (identityAccessor.User == null)
                throw new InvalidOperationException();

            this.Contains(nameof(Report.Name), Name);

            if (AllReports && identityAccessor.Claims.Developer)
            {
            }
            else if (PublicReports)
            {
                this.Equal(nameof(Report.IsPublic), true, true);
            }
            else
            {
                this.Equal(nameof(Report.OwnerUserId), identityAccessor.User.Id);
            }


            return this;
        }
    }

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

            var query = request.ApplyCustomFilters(_identityAccessor);
            return await _reportRepository.QueryReport(query);
        }
    }
}
