using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateResearchMethodology: IRequest<ResearchMethodology>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public bool Hidden { get; set; }
        public bool Default { get; set; }
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
            updated.Code = request.Code;
            updated.Hidden = request.Hidden;
            updated.Default = request.Default;
            
            return await _researchMethodologyRepository.UpdateResearchMethodology(original: original, updated: updated);
        }
    }
}
