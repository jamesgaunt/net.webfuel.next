using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateHowDidYouFindUs: IRequest<HowDidYouFindUs>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class CreateHowDidYouFindUsHandler : IRequestHandler<CreateHowDidYouFindUs, HowDidYouFindUs>
    {
        private readonly IHowDidYouFindUsRepository _howDidYouFindUsRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateHowDidYouFindUsHandler(IHowDidYouFindUsRepository howDidYouFindUsRepository, IStaticDataCache staticDataCache)
        {
            _howDidYouFindUsRepository = howDidYouFindUsRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<HowDidYouFindUs> Handle(CreateHowDidYouFindUs request, CancellationToken cancellationToken)
        {
            var updated = await _howDidYouFindUsRepository.InsertHowDidYouFindUs(new HowDidYouFindUs {
                    Name = request.Name,
                    Default = request.Default,
                    Hidden = request.Hidden,
                    FreeText = request.FreeText,
                    SortOrder = await _howDidYouFindUsRepository.CountHowDidYouFindUs(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

