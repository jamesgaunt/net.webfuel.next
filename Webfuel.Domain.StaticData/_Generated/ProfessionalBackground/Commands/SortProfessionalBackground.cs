using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortProfessionalBackground: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortProfessionalBackgroundHandler : IRequestHandler<SortProfessionalBackground>
    {
        private readonly IProfessionalBackgroundRepository _professionalBackgroundRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortProfessionalBackgroundHandler(IProfessionalBackgroundRepository professionalBackgroundRepository, IStaticDataCache staticDataCache)
        {
            _professionalBackgroundRepository = professionalBackgroundRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortProfessionalBackground request, CancellationToken cancellationToken)
        {
            var items = await _professionalBackgroundRepository.SelectProfessionalBackground();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _professionalBackgroundRepository.UpdateProfessionalBackground(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

