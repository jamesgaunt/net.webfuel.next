using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class UpdateSupportRequestStatus : IRequest<SupportRequest>
    {
        public required Guid Id { get; set; }

        public required Guid StatusId { get; set; }

        public required List<Guid> SupportProvidedIds { get; set; }

        public string Description { get; set; } = String.Empty;

        public Decimal? WorkTimeInHours { get; set; }
    }
}
