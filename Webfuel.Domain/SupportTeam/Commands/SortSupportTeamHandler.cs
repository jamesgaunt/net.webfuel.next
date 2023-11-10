using MediatR;

namespace Webfuel.Domain
{
    internal class SortSupportTeamHandler : IRequestHandler<SortSupportTeam>
    {
        private readonly ISupportTeamRepository _supportTeamRepository;
        
        
        public SortSupportTeamHandler(ISupportTeamRepository supportTeamRepository)
        {
            _supportTeamRepository = supportTeamRepository;
        }
        
        public async Task Handle(SortSupportTeam request, CancellationToken cancellationToken)
        {
            var items = await _supportTeamRepository.SelectSupportTeam();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _supportTeamRepository.UpdateSupportTeam(updated: updated, original: original);
                }
                index++;
            }
        }
    }
}

