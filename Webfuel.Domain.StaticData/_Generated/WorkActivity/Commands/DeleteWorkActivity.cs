using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteWorkActivity: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteWorkActivityHandler : IRequestHandler<DeleteWorkActivity>
    {
        private readonly IWorkActivityRepository _workActivityRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteWorkActivityHandler(IWorkActivityRepository workActivityRepository, IStaticDataCache staticDataCache)
        {
            _workActivityRepository = workActivityRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteWorkActivity request, CancellationToken cancellationToken)
        {
            await _workActivityRepository.DeleteWorkActivity(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

