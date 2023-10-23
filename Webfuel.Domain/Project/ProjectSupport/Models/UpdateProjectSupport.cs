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

        public List<Guid> AdviserIds { get; } = new List<Guid>();

        public List<Guid> SupportProvidedIds { get; } = new List<Guid>();

        public string Description { get; set; } = String.Empty;
    }
}
