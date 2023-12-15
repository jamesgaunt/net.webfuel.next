using Azure.Core;
using MediatR;
using Serilog;

namespace Webfuel.Common
{
    public class QueryHeartbeat : Query, IRequest<QueryResult<Heartbeat>>
    {
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(Heartbeat.Name), Search);
            return this;
        }
    }
}
