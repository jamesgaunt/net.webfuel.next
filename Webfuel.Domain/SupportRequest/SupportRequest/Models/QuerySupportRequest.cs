using MediatR;

namespace Webfuel.Domain
{
    public class QuerySupportRequest : Query, IRequest<QueryResult<SupportRequest>>
    {
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(SupportRequest.Title), Search);
            return this;
        }
    }
}
