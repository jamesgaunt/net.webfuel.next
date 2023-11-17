using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteResearchMethodology: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteResearchMethodologyHandler : IRequestHandler<DeleteResearchMethodology>
    {
        private readonly IResearchMethodologyRepository _researchMethodologyRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteResearchMethodologyHandler(IResearchMethodologyRepository researchMethodologyRepository, IStaticDataCache staticDataCache)
        {
            _researchMethodologyRepository = researchMethodologyRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteResearchMethodology request, CancellationToken cancellationToken)
        {
            await _researchMethodologyRepository.DeleteResearchMethodology(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

