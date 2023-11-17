using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteDisability: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteDisabilityHandler : IRequestHandler<DeleteDisability>
    {
        private readonly IDisabilityRepository _disabilityRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteDisabilityHandler(IDisabilityRepository disabilityRepository, IStaticDataCache staticDataCache)
        {
            _disabilityRepository = disabilityRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteDisability request, CancellationToken cancellationToken)
        {
            await _disabilityRepository.DeleteDisability(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

