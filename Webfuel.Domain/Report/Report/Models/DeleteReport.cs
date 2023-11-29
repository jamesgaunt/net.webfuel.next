using MediatR;

namespace Webfuel.Domain
{
    public class DeleteReport : IRequest
    {
        public Guid Id { get; set; }
    }
}
