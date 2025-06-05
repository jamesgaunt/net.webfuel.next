using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteFileTag: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteFileTagHandler : IRequestHandler<DeleteFileTag>
    {
        private readonly IFileTagRepository _fileTagRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteFileTagHandler(IFileTagRepository fileTagRepository, IStaticDataCache staticDataCache)
        {
            _fileTagRepository = fileTagRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteFileTag request, CancellationToken cancellationToken)
        {
            await _fileTagRepository.DeleteFileTag(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

