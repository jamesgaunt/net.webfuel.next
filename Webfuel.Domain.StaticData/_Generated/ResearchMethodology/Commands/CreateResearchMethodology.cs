using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateResearchMethodology: IRequest<ResearchMethodology>
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public bool Hidden { get; set; }
        public bool Default { get; set; }
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
                    Code = request.Code,
                    Hidden = request.Hidden,
                    Default = request.Default,
                    SortOrder = await _researchMethodologyRepository.CountResearchMethodology()
                });
        }
    }
}
