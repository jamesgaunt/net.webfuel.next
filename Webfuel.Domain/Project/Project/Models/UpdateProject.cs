using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class UpdateProject: IRequest<Project>
    {
        public required Guid Id { get; set; }

        public required string Title { get; set; }

        public Guid? FundingBodyId { get; set; }

        public Guid? ResearchMethodologyId { get; set; }

    }
}
