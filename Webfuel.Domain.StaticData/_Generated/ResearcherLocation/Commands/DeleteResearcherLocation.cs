using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteResearcherLocation: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteResearcherLocationHandler : IRequestHandler<DeleteResearcherLocation>
    {
        private readonly IResearcherLocationRepository _researcherLocationRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteResearcherLocationHandler(IResearcherLocationRepository researcherLocationRepository, IStaticDataCache staticDataCache)
        {
            _researcherLocationRepository = researcherLocationRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteResearcherLocation request, CancellationToken cancellationToken)
        {
            await _researcherLocationRepository.DeleteResearcherLocation(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

