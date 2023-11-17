using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteSite: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteSiteHandler : IRequestHandler<DeleteSite>
    {
        private readonly ISiteRepository _siteRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteSiteHandler(ISiteRepository siteRepository, IStaticDataCache staticDataCache)
        {
            _siteRepository = siteRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteSite request, CancellationToken cancellationToken)
        {
            await _siteRepository.DeleteSite(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

