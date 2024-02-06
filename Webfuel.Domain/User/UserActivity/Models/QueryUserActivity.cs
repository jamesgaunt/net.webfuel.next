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
        public required Guid? UserId { get; set; }

        public DateOnly? FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        public string Description { get; set; } = String.Empty;

        public Query ApplyCustomFilters()
        {
            this.Equal(nameof(UserActivity.UserId), UserId, UserId != null);
            this.GreaterThanOrEqual(nameof(UserActivity.Date), FromDate, FromDate != null);
            this.LessThanOrEqual(nameof(UserActivity.Date), ToDate, ToDate != null);
            this.Contains(nameof(UserActivity.Description), Description);
            return this;
        }
    }
}
