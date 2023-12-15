using MediatR;

namespace Webfuel.Common
{
    internal class CreateHeartbeatHandler : IRequestHandler<CreateHeartbeat, Heartbeat>
    {
        private readonly IHeartbeatRepository _heartbeatRepository;

        public CreateHeartbeatHandler(IHeartbeatRepository heartbeatRepository)
        {
            _heartbeatRepository = heartbeatRepository;
        }

        public async Task<Heartbeat> Handle(CreateHeartbeat request, CancellationToken cancellationToken)
        {
            var heartbeat = new Heartbeat
            {
                Name = request.Name,
            };

            return await _heartbeatRepository.InsertHeartbeat(heartbeat);
        }
    }
}
