using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateGender: IRequest<Gender>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateGenderHandler : IRequestHandler<CreateGender, Gender>
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateGenderHandler(IGenderRepository genderRepository, IStaticDataCache staticDataCache)
        {
            _genderRepository = genderRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<Gender> Handle(CreateGender request, CancellationToken cancellationToken)
        {
            var updated = await _genderRepository.InsertGender(new Gender {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _genderRepository.CountGender(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

