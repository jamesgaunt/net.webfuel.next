using MediatR;

namespace Webfuel.Domain
{
    public class QuerySupportRequest : Query, IRequest<QueryResult<SupportRequest>>
    {
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(SupportRequest.Title), Title);
            this.GreaterThanOrEqual(nameof(SupportRequest.DateOfRequest), FromDate, FromDate != null);
            this.LessThanOrEqual(nameof(SupportRequest.DateOfRequest), ToDate, ToDate != null);
            this.Equal(nameof(SupportRequest.StatusId), StatusId, StatusId != null);

            return this;
        }

        public string Title { get; set; } = System.String.Empty;

        public DateOnly? FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        public Guid? StatusId { get; set; }
    }

    internal class QuerySupportRequestHandler : IRequestHandler<QuerySupportRequest, QueryResult<SupportRequest>>
    {
        private readonly ISupportRequestRepository _supportRequestRepository;

        public QuerySupportRequestHandler(ISupportRequestRepository supportRequestRepository)
        {
            _supportRequestRepository = supportRequestRepository;
        }

        public async Task<QueryResult<SupportRequest>> Handle(QuerySupportRequest request, CancellationToken cancellationToken)
        {
            return await _supportRequestRepository.QuerySupportRequest(request.ApplyCustomFilters());
        }
    }
}
