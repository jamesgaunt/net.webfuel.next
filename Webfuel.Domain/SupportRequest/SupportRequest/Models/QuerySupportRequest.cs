using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Webfuel.Domain.StaticData;

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
}
