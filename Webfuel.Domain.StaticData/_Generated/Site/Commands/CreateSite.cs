using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateSite: IRequest<Site>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
    }
    internal class CreateSiteHandler : IRequestHandler<CreateSite, Site>
    {
        private readonly ISiteRepository _siteRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateSiteHandler(ISiteRepository siteRepository, IStaticDataCache staticDataCache)
        {
            _siteRepository = siteRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<Site> Handle(CreateSite request, CancellationToken cancellationToken)
        {
            var updated = await _siteRepository.InsertSite(new Site {
                    Name = request.Name,
                    Default = request.Default,
                    SortOrder = await _siteRepository.CountSite(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

