using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateResearchMethodology: IRequest<ResearchMethodology>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateResearchMethodologyHandler : IRequestHandler<CreateResearchMethodology, ResearchMethodology>
    {
        private readonly IResearchMethodologyRepository _researchMethodologyRepository;
        
        
        public CreateResearchMethodologyHandler(IResearchMethodologyRepository researchMethodologyRepository)
        {
            _researchMethodologyRepository = researchMethodologyRepository;
        }
        
        public async Task<ResearchMethodology> Handle(CreateResearchMethodology request, CancellationToken cancellationToken)
        {
            return await _researchMethodologyRepository.InsertResearchMethodology(new ResearchMethodology {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _researchMethodologyRepository.CountResearchMethodology(),
                });
        }
    }
}

