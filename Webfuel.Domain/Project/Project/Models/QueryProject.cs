using MediatR;

namespace Webfuel.Domain
{
    public class QueryProject : Query, IRequest<QueryResult<Project>>
    {
        public Query ApplyCustomFilters()
        {
            var number = FilterUtility.ExtractInt32(Number);

            this.Contains(nameof(Project.Title), Title);
            this.Equal(nameof(Project.Number), number, number != null);
            this.GreaterThanOrEqual(nameof(Project.DateOfRequest), FromDate, FromDate != null);
            this.LessThanOrEqual(nameof(Project.DateOfRequest), ToDate, ToDate != null);
            this.Equal(nameof(Project.StatusId), StatusId, StatusId != null);
            this.Equal(nameof(Project.SubmittedFundingStreamId), FundingStreamId, FundingStreamId != null);
            this.Equal(nameof(Project.LeadAdviserUserId), LeadAdviserUserId, LeadAdviserUserId != null);

            return this;
        }

        public string Number { get; set; } = String.Empty;

        public string Title { get; set; } = String.Empty;

        public DateOnly? FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        public Guid? StatusId { get; set; }

        public Guid? FundingStreamId { get; set; }

        public Guid? LeadAdviserUserId { get; set; }
    }
}
