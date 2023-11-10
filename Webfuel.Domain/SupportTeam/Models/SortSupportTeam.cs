using MediatR;

namespace Webfuel.Domain
{
    public class SortSupportTeam: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
}

