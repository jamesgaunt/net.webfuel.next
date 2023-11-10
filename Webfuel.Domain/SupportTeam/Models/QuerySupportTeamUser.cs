using Azure.Core;
using MediatR;
using Serilog;

namespace Webfuel.Domain
{
    public class QuerySupportTeamUser : Query, IRequest<QueryResult<SupportTeamUser>>
    {
        public Guid? UserId { get; set; }

        public Guid? SupportTeamId { get; set; }

        public Query ApplyCustomFilters()
        {
            this.Equal(nameof(SupportTeamUser.UserId), UserId, UserId != null);
            this.Equal(nameof(SupportTeamUser.SupportTeamId), SupportTeamId, SupportTeamId != null);
            return this;
        }
    }
}
