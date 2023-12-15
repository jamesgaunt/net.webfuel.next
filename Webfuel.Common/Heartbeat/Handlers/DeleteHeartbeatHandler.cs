using MediatR;

namespace Webfuel.Common
{
    internal class DeleteHeartbeatHandler : IRequestHandler<DeleteHeartbeat>
    {
        private readonly IHeartbeatRepository _heartbeatRepository;

        public DeleteHeartbeatHandler(IHeartbeatRepository heartbeatRepository)
        {
            _heartbeatRepository = heartbeatRepository;
        }

        public async Task Handle(DeleteHeartbeat request, CancellationToken cancellationToken)
        {
            await _heartbeatRepository.DeleteHeartbeat(request.Id);
        }
    }
}
