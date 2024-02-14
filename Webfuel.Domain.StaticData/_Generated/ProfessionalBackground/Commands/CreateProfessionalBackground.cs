using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateProfessionalBackground: IRequest<ProfessionalBackground>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateProfessionalBackgroundHandler : IRequestHandler<CreateProfessionalBackground, ProfessionalBackground>
    {
        private readonly IProfessionalBackgroundRepository _professionalBackgroundRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateProfessionalBackgroundHandler(IProfessionalBackgroundRepository professionalBackgroundRepository, IStaticDataCache staticDataCache)
        {
            _professionalBackgroundRepository = professionalBackgroundRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<ProfessionalBackground> Handle(CreateProfessionalBackground request, CancellationToken cancellationToken)
        {
            var updated = await _professionalBackgroundRepository.InsertProfessionalBackground(new ProfessionalBackground {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _professionalBackgroundRepository.CountProfessionalBackground(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

