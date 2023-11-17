using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateEthnicity: IRequest<Ethnicity>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateEthnicityHandler : IRequestHandler<CreateEthnicity, Ethnicity>
    {
        private readonly IEthnicityRepository _ethnicityRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateEthnicityHandler(IEthnicityRepository ethnicityRepository, IStaticDataCache staticDataCache)
        {
            _ethnicityRepository = ethnicityRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<Ethnicity> Handle(CreateEthnicity request, CancellationToken cancellationToken)
        {
            var updated = await _ethnicityRepository.InsertEthnicity(new Ethnicity {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _ethnicityRepository.CountEthnicity(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

