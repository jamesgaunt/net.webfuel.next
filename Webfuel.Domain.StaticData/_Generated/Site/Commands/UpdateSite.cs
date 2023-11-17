using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateSite: IRequest<Site>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
    }
    internal class UpdateSiteHandler : IRequestHandler<UpdateSite, Site>
    {
        private readonly ISiteRepository _siteRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateSiteHandler(ISiteRepository siteRepository, IStaticDataCache staticDataCache)
        {
            _siteRepository = siteRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<Site> Handle(UpdateSite request, CancellationToken cancellationToken)
        {
            var original = await _siteRepository.RequireSite(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            
            updated = await _siteRepository.UpdateSite(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

