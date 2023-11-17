using MediatR;

namespace Webfuel.Domain
{
    public class QuerySupportRequestChangeLog : Query, IRequest<QueryResult<SupportRequestChangeLog>>
    {
        public Query ApplyCustomFilters()
        {
            this.Equal(nameof(SupportRequestChangeLog.SupportRequestId), SupportRequestId);
            return this;
        }

        public required Guid SupportRequestId { get; set; }
    }
}
