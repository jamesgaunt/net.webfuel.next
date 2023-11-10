using Azure.Core;
using MediatR;
using Serilog;

namespace Webfuel.Domain
{
    public class QuerySupportTeam : Query, IRequest<QueryResult<SupportTeam>>
    {
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(SupportTeam.Name), Search);
            return this;
        }
    }
}
