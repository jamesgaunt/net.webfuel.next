using MediatR;

namespace Webfuel.Common
{
    internal class UpdateHeartbeatHandler : IRequestHandler<UpdateHeartbeat, Heartbeat>
    {
        private readonly IHeartbeatRepository _reportRepository;

        public UpdateHeartbeatHandler(IHeartbeatRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<Heartbeat> Handle(UpdateHeartbeat request, CancellationToken cancellationToken)
        {
            var original = await _reportRepository.RequireHeartbeat(request.Id);

            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Live = request.Live;
            updated.LogSuccessfulExecutions = request.LogSuccessfulExecutions;

            updated.ProviderName = request.ProviderName;
            updated.ProviderParameter = request.ProviderParameter;

            updated.MinTime = request.MinTime;
            updated.MaxTime = request.MaxTime;
            updated.Schedule = request.Schedule;

            return await _reportRepository.UpdateHeartbeat(original: original, updated: updated); 
        }
    }
}
