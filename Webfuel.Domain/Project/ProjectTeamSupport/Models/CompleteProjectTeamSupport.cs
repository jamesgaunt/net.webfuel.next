using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class CompleteProjectTeamSupport : IRequest<ProjectTeamSupport>
    {
        public required Guid Id { get; set; }

        public required string Notes { get; set; }

    }
}
