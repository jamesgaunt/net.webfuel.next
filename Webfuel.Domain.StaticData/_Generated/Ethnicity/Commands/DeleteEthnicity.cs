using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteEthnicity: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteEthnicityHandler : IRequestHandler<DeleteEthnicity>
    {
        private readonly IEthnicityRepository _ethnicityRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteEthnicityHandler(IEthnicityRepository ethnicityRepository, IStaticDataCache staticDataCache)
        {
            _ethnicityRepository = ethnicityRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteEthnicity request, CancellationToken cancellationToken)
        {
            await _ethnicityRepository.DeleteEthnicity(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

