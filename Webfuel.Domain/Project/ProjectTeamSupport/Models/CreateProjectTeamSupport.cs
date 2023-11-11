using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class CreateProjectTeamSupport : IRequest<ProjectTeamSupport>
    {
        public required Guid ProjectId { get; set; }

        public required Guid SupportTeamId { get; set; }

        public required string CreatedNotes { get; set; }

    }
}
