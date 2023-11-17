using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateSupportProvided: IRequest<SupportProvided>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateSupportProvidedHandler : IRequestHandler<UpdateSupportProvided, SupportProvided>
    {
        private readonly ISupportProvidedRepository _supportProvidedRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateSupportProvidedHandler(ISupportProvidedRepository supportProvidedRepository, IStaticDataCache staticDataCache)
        {
            _supportProvidedRepository = supportProvidedRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<SupportProvided> Handle(UpdateSupportProvided request, CancellationToken cancellationToken)
        {
            var original = await _supportProvidedRepository.RequireSupportProvided(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            updated = await _supportProvidedRepository.UpdateSupportProvided(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

