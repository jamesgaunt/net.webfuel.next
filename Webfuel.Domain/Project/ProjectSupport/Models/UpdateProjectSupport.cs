using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class UpdateProjectSupport : IRequest<ProjectSupport>
    {
        public required Guid Id { get; set; }

        public DateOnly Date { get; set; }

        public required List<Guid> TeamIds { get; set; }

        public required List<Guid> AdviserIds { get; set; }

        public required List<Guid> SupportProvidedIds { get; set; }

        public string Description { get; set; } = String.Empty;
    }
}
