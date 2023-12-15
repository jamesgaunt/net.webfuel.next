using FluentValidation;
using MediatR;

namespace Webfuel.Common
{

    internal class GetHeartbeatHandler : IRequestHandler<GetHeartbeat, Heartbeat?>
    {
        private readonly IHeartbeatRepository _reportRepository;

        public GetHeartbeatHandler(IHeartbeatRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<Heartbeat?> Handle(GetHeartbeat request, CancellationToken cancellationToken)
        {
            return await _reportRepository.GetHeartbeat(request.Id);
        }
    }
}
