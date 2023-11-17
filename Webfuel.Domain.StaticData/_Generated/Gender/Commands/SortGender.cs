using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortGender: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortGenderHandler : IRequestHandler<SortGender>
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortGenderHandler(IGenderRepository genderRepository, IStaticDataCache staticDataCache)
        {
            _genderRepository = genderRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortGender request, CancellationToken cancellationToken)
        {
            var items = await _genderRepository.SelectGender();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _genderRepository.UpdateGender(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

