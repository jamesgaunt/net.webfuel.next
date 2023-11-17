using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateDisability: IRequest<Disability>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateDisabilityHandler : IRequestHandler<UpdateDisability, Disability>
    {
        private readonly IDisabilityRepository _disabilityRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateDisabilityHandler(IDisabilityRepository disabilityRepository, IStaticDataCache staticDataCache)
        {
            _disabilityRepository = disabilityRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<Disability> Handle(UpdateDisability request, CancellationToken cancellationToken)
        {
            var original = await _disabilityRepository.RequireDisability(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            updated = await _disabilityRepository.UpdateDisability(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

