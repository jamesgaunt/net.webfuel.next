using MediatR;

namespace Webfuel.Domain
{
    public class QuerySupportRequest : Query, IRequest<QueryResult<SupportRequest>>
    {
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(SupportRequest.Title), Title);
            this.Contains(nameof(SupportRequest.TeamContactFullName), TeamContactFullName);
            this.GreaterThanOrEqual(nameof(SupportRequest.DateOfRequest), FromDate, FromDate != null);
            this.LessThanOrEqual(nameof(SupportRequest.DateOfRequest), ToDate, ToDate != null);
            this.Equal(nameof(SupportRequest.StatusId), StatusId, StatusId != null);
            this.Equal(nameof(SupportRequest.ProposedFundingStreamId), ProposedFundingStreamId, ProposedFundingStreamId != null);
            return this;
        }

        public string Title { get; set; } = System.String.Empty;

        public string TeamContactFullName { get; set; } = String.Empty;

        public DateOnly? FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        public Guid? StatusId { get; set; }

        public Guid? ProposedFundingStreamId { get; set; }
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
            request.ApplyCustomFilters();

            if (request.Sort.Count == 1 && String.Compare(request.Sort[0].Field, nameof(SupportRequest.TeamContactFullName), true) == 0)
            {
                request.Sort[0].Field = nameof(SupportRequest.TeamContactLastName);
                request.Sort.Add(new QuerySort
                {
                    Field = nameof(SupportRequest.TeamContactFirstName),
                    Direction = request.Sort[0].Direction
                });
            }

            return await _supportRequestRepository.QuerySupportRequest(request);
        }
    }
}
