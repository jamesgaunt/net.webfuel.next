using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateFileTag: IRequest<FileTag>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
    }
    internal class CreateFileTagHandler : IRequestHandler<CreateFileTag, FileTag>
    {
        private readonly IFileTagRepository _fileTagRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateFileTagHandler(IFileTagRepository fileTagRepository, IStaticDataCache staticDataCache)
        {
            _fileTagRepository = fileTagRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<FileTag> Handle(CreateFileTag request, CancellationToken cancellationToken)
        {
            var updated = await _fileTagRepository.InsertFileTag(new FileTag {
                    Name = request.Name,
                    Default = request.Default,
                    SortOrder = await _fileTagRepository.CountFileTag(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

