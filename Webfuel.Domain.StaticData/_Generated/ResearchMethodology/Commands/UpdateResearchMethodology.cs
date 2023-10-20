using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateResearchMethodology: IRequest<ResearchMethodology>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateResearchMethodologyHandler : IRequestHandler<UpdateResearchMethodology, ResearchMethodology>
    {
        private readonly IResearchMethodologyRepository _researchMethodologyRepository;
        
        
        public UpdateResearchMethodologyHandler(IResearchMethodologyRepository researchMethodologyRepository)
        {
            _researchMethodologyRepository = researchMethodologyRepository;
        }
        
        public async Task<ResearchMethodology> Handle(UpdateResearchMethodology request, CancellationToken cancellationToken)
        {
            var original = await _researchMethodologyRepository.RequireResearchMethodology(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            return await _researchMethodologyRepository.UpdateResearchMethodology(original: original, updated: updated);
        }
    }
}

