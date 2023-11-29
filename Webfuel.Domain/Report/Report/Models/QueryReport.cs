using Azure.Core;
using MediatR;
using Serilog;

namespace Webfuel.Domain
{
    public class QueryReport : Query, IRequest<QueryResult<Report>>
    {
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(Report.Name), Search);
            return this;
        }
    }
}
