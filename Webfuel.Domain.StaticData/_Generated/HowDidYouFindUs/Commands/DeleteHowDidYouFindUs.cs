using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteHowDidYouFindUs: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteHowDidYouFindUsHandler : IRequestHandler<DeleteHowDidYouFindUs>
    {
        private readonly IHowDidYouFindUsRepository _howDidYouFindUsRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteHowDidYouFindUsHandler(IHowDidYouFindUsRepository howDidYouFindUsRepository, IStaticDataCache staticDataCache)
        {
            _howDidYouFindUsRepository = howDidYouFindUsRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteHowDidYouFindUs request, CancellationToken cancellationToken)
        {
            await _howDidYouFindUsRepository.DeleteHowDidYouFindUs(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

