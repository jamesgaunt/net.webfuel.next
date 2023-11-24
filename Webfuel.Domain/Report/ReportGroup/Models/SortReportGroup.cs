using MediatR;

namespace Webfuel.Domain
{
    public class SortReportGroup: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
}

