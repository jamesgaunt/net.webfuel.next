using Azure.Core;
using MediatR;
using Serilog;

namespace Webfuel.Domain
{
    public class QueryReportGroup : Query, IRequest<QueryResult<ReportGroup>>
    {
        public Query ApplyCustomFilters()
        {
            this.Contains(nameof(ReportGroup.Name), Search);
            return this;
        }
    }
}
