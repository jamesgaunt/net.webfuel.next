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
        
        
        public CreateResearcherRoleHandler(IResearcherRoleRepository researcherRoleRepository)
        {
            _researcherRoleRepository = researcherRoleRepository;
        }
        
        public async Task<ResearcherRole> Handle(CreateResearcherRole request, CancellationToken cancellationToken)
        {
            return await _researcherRoleRepository.InsertResearcherRole(new ResearcherRole {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _researcherRoleRepository.CountResearcherRole(),
                });
        }
    }
}

