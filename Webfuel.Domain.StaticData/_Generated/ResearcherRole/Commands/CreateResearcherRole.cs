using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateResearcherRole: IRequest<ResearcherRole>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateResearcherRoleHandler : IRequestHandler<CreateResearcherRole, ResearcherRole>
    {
        private readonly IResearcherRoleRepository _researcherRoleRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateResearcherRoleHandler(IResearcherRoleRepository researcherRoleRepository, IStaticDataCache staticDataCache)
        {
            _researcherRoleRepository = researcherRoleRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ResearcherRole> Handle(CreateResearcherRole request, CancellationToken cancellationToken)
        {
            var updated = await _researcherRoleRepository.InsertResearcherRole(new ResearcherRole {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _researcherRoleRepository.CountResearcherRole(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

