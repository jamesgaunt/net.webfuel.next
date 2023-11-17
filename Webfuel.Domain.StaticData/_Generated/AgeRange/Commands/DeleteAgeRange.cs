using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteAgeRange: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteAgeRangeHandler : IRequestHandler<DeleteAgeRange>
    {
        private readonly IAgeRangeRepository _ageRangeRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteAgeRangeHandler(IAgeRangeRepository ageRangeRepository, IStaticDataCache staticDataCache)
        {
            _ageRangeRepository = ageRangeRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteAgeRange request, CancellationToken cancellationToken)
        {
            await _ageRangeRepository.DeleteAgeRange(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

