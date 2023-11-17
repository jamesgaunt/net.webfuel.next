using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateHowDidYouFindUs: IRequest<HowDidYouFindUs>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
        public bool Hidden { get; set; } = false;
        public bool FreeText { get; set; } = false;
    }
    internal class UpdateHowDidYouFindUsHandler : IRequestHandler<UpdateHowDidYouFindUs, HowDidYouFindUs>
    {
        private readonly IHowDidYouFindUsRepository _howDidYouFindUsRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateHowDidYouFindUsHandler(IHowDidYouFindUsRepository howDidYouFindUsRepository, IStaticDataCache staticDataCache)
        {
            _howDidYouFindUsRepository = howDidYouFindUsRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<HowDidYouFindUs> Handle(UpdateHowDidYouFindUs request, CancellationToken cancellationToken)
        {
            var original = await _howDidYouFindUsRepository.RequireHowDidYouFindUs(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            updated.Hidden = request.Hidden;
            updated.FreeText = request.FreeText;
            
            updated = await _howDidYouFindUsRepository.UpdateHowDidYouFindUs(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

