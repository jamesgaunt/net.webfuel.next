using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateRSSHub: IRequest<RSSHub>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
    }
    internal class UpdateRSSHubHandler : IRequestHandler<UpdateRSSHub, RSSHub>
    {
        private readonly IRSSHubRepository _rsshubRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateRSSHubHandler(IRSSHubRepository rsshubRepository, IStaticDataCache staticDataCache)
        {
            _rsshubRepository = rsshubRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<RSSHub> Handle(UpdateRSSHub request, CancellationToken cancellationToken)
        {
            var original = await _rsshubRepository.RequireRSSHub(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            
            updated = await _rsshubRepository.UpdateRSSHub(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

