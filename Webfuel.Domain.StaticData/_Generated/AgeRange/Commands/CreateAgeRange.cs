using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateAgeRange: IRequest<AgeRange>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
    }
    internal class CreateAgeRangeHandler : IRequestHandler<CreateAgeRange, AgeRange>
    {
        private readonly IAgeRangeRepository _ageRangeRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateAgeRangeHandler(IAgeRangeRepository ageRangeRepository, IStaticDataCache staticDataCache)
        {
            _ageRangeRepository = ageRangeRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<AgeRange> Handle(CreateAgeRange request, CancellationToken cancellationToken)
        {
            var updated = await _ageRangeRepository.InsertAgeRange(new AgeRange {
                    Name = request.Name,
                    Default = request.Default,
                    SortOrder = await _ageRangeRepository.CountAgeRange(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

