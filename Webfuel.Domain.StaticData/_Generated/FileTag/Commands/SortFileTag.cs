using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class SortFileTag: IRequest
    {
        public required IEnumerable<Guid> Ids { get; set; }
    }
    internal class SortFileTagHandler : IRequestHandler<SortFileTag>
    {
        private readonly IFileTagRepository _fileTagRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public SortFileTagHandler(IFileTagRepository fileTagRepository, IStaticDataCache staticDataCache)
        {
            _fileTagRepository = fileTagRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(SortFileTag request, CancellationToken cancellationToken)
        {
            var items = await _fileTagRepository.SelectFileTag();
            
            var index = 0;
            foreach (var id in request.Ids)
            {
                var original = items.FirstOrDefault(p => p.Id == id);
                if (original != null && original.SortOrder != index)
                {
                    var updated = original.Copy();
                    updated.SortOrder = index;
                    await _fileTagRepository.UpdateFileTag(updated: updated, original: original);
                }
                index++;
            }
            _staticDataCache.FlushStaticData();
        }
    }
}

