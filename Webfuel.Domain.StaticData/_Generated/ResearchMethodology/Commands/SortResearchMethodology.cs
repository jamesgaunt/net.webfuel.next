using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortResearchMethodology: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortResearchMethodologyHandler : IRequestHandler<SortResearchMethodology>
    {
        private readonly IResearchMethodologyRepository _researchMethodologyRepository;
        
        
        public SortResearchMethodologyHandler(IResearchMethodologyRepository researchMethodologyRepository)
        {
            _researchMethodologyRepository = researchMethodologyRepository;
        }
        
        public async Task Handle(SortResearchMethodology request, CancellationToken cancellationToken)
        {
            var items = await _researchMethodologyRepository.SelectResearchMethodology();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _researchMethodologyRepository.UpdateResearchMethodology(updated: updated, original: original);
                }
                index++;
            }
        }
    }
}

