using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateSupportProvided: IRequest<SupportProvided>
    {
        public required string Name { get; set; }
        public string Alias { get; set; } = String.Empty;
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateSupportProvidedHandler : IRequestHandler<CreateSupportProvided, SupportProvided>
    {
        private readonly ISupportProvidedRepository _supportProvidedRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateSupportProvidedHandler(ISupportProvidedRepository supportProvidedRepository, IStaticDataCache staticDataCache)
        {
            _supportProvidedRepository = supportProvidedRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<SupportProvided> Handle(CreateSupportProvided request, CancellationToken cancellationToken)
        {
            var updated = await _supportProvidedRepository.InsertSupportProvided(new SupportProvided {
                    Name = request.Name,
                    Alias = request.Alias,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _supportProvidedRepository.CountSupportProvided(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

