using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateResearcherRole: IRequest<ResearcherRole>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateResearcherRoleHandler : IRequestHandler<UpdateResearcherRole, ResearcherRole>
    {
        private readonly IResearcherRoleRepository _researcherRoleRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateResearcherRoleHandler(IResearcherRoleRepository researcherRoleRepository, IStaticDataCache staticDataCache)
        {
            _researcherRoleRepository = researcherRoleRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ResearcherRole> Handle(UpdateResearcherRole request, CancellationToken cancellationToken)
        {
            var original = await _researcherRoleRepository.RequireResearcherRole(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            updated = await _researcherRoleRepository.UpdateResearcherRole(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

