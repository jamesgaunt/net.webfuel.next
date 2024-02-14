using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateProfessionalBackground: IRequest<ProfessionalBackground>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateProfessionalBackgroundHandler : IRequestHandler<UpdateProfessionalBackground, ProfessionalBackground>
    {
        private readonly IProfessionalBackgroundRepository _professionalBackgroundRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateProfessionalBackgroundHandler(IProfessionalBackgroundRepository professionalBackgroundRepository, IStaticDataCache staticDataCache)
        {
            _professionalBackgroundRepository = professionalBackgroundRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ProfessionalBackground> Handle(UpdateProfessionalBackground request, CancellationToken cancellationToken)
        {
            var original = await _professionalBackgroundRepository.RequireProfessionalBackground(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            updated = await _professionalBackgroundRepository.UpdateProfessionalBackground(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

