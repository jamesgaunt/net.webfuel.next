using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteRSSHub: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteRSSHubHandler : IRequestHandler<DeleteRSSHub>
    {
        private readonly IRSSHubRepository _rsshubRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteRSSHubHandler(IRSSHubRepository rsshubRepository, IStaticDataCache staticDataCache)
        {
            _rsshubRepository = rsshubRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteRSSHub request, CancellationToken cancellationToken)
        {
            await _rsshubRepository.DeleteRSSHub(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

