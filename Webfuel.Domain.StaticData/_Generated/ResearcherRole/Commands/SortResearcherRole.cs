using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortResearcherRole: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortResearcherRoleHandler : IRequestHandler<SortResearcherRole>
    {
        private readonly IResearcherRoleRepository _researcherRoleRepository;
        
        
        public SortResearcherRoleHandler(IResearcherRoleRepository researcherRoleRepository)
        {
            _researcherRoleRepository = researcherRoleRepository;
        }
        
        public async Task Handle(SortResearcherRole request, CancellationToken cancellationToken)
        {
            var items = await _researcherRoleRepository.SelectResearcherRole();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _researcherRoleRepository.UpdateResearcherRole(updated: updated, original: original);
                }
                index++;
            }
        }
    }
}

