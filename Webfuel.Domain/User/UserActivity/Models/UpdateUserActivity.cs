using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class UpdateUserActivity : IRequest<UserActivity>
    {
        public required Guid Id { get; set; }

        public required DateOnly Date { get; set; }

        public required Guid WorkActivityId { get; set; }

        public required Decimal WorkTimeInHours { get; set; }

        public required string Description { get; set; }
    }
}
