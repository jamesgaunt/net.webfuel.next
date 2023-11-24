using MediatR;

namespace Webfuel.Domain
{
    public class DeleteReportGroup : IRequest
    {
        public Guid Id { get; set; }
    }
}
