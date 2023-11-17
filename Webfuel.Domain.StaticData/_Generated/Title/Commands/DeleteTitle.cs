using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteTitle: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteTitleHandler : IRequestHandler<DeleteTitle>
    {
        private readonly ITitleRepository _titleRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteTitleHandler(ITitleRepository titleRepository, IStaticDataCache staticDataCache)
        {
            _titleRepository = titleRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteTitle request, CancellationToken cancellationToken)
        {
            await _titleRepository.DeleteTitle(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

