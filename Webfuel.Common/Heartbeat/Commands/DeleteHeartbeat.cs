using MediatR;

namespace Webfuel.Common
{
    public class DeleteHeartbeat : IRequest
    {
        public Guid Id { get; set; }
    }
}
