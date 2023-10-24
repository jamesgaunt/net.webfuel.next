using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class QueryUserActivity : Query, IRequest<QueryResult<UserActivity>>
    {
        public required Guid UserId { get; set; }

        public Query ApplyCustomFilters()
        {
            this.Equal(nameof(UserActivity.UserId), UserId);
            return this;
        }
    }
}
