using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateFileTag: IRequest<FileTag>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
    }
    internal class UpdateFileTagHandler : IRequestHandler<UpdateFileTag, FileTag>
    {
        private readonly IFileTagRepository _fileTagRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateFileTagHandler(IFileTagRepository fileTagRepository, IStaticDataCache staticDataCache)
        {
            _fileTagRepository = fileTagRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<FileTag> Handle(UpdateFileTag request, CancellationToken cancellationToken)
        {
            var original = await _fileTagRepository.RequireFileTag(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            
            updated = await _fileTagRepository.UpdateFileTag(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

