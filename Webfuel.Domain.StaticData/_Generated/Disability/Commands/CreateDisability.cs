using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateDisability: IRequest<Disability>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateDisabilityHandler : IRequestHandler<CreateDisability, Disability>
    {
        private readonly IDisabilityRepository _disabilityRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateDisabilityHandler(IDisabilityRepository disabilityRepository, IStaticDataCache staticDataCache)
        {
            _disabilityRepository = disabilityRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<Disability> Handle(CreateDisability request, CancellationToken cancellationToken)
        {
            var updated = await _disabilityRepository.InsertDisability(new Disability {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _disabilityRepository.CountDisability(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

