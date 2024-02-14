using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateRSSHub: IRequest<RSSHub>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
    }
    internal class CreateRSSHubHandler : IRequestHandler<CreateRSSHub, RSSHub>
    {
        private readonly IRSSHubRepository _rsshubRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateRSSHubHandler(IRSSHubRepository rsshubRepository, IStaticDataCache staticDataCache)
        {
            _rsshubRepository = rsshubRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<RSSHub> Handle(CreateRSSHub request, CancellationToken cancellationToken)
        {
            var updated = await _rsshubRepository.InsertRSSHub(new RSSHub {
                    Name = request.Name,
                    Default = request.Default,
                    SortOrder = await _rsshubRepository.CountRSSHub(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

