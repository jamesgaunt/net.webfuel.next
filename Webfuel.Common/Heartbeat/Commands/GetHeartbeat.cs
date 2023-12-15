using MediatR;

namespace Webfuel.Common
{
    public class GetHeartbeat : IRequest<Heartbeat?>
    {
        public Guid Id { get; set; }
    }
}
