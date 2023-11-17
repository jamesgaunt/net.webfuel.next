using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class UpdateTitle: IRequest<Title>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
    }
    internal class UpdateTitleHandler : IRequestHandler<UpdateTitle, Title>
    {
        private readonly ITitleRepository _titleRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public UpdateTitleHandler(ITitleRepository titleRepository, IStaticDataCache staticDataCache)
        {
            _titleRepository = titleRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<Title> Handle(UpdateTitle request, CancellationToken cancellationToken)
        {
            var original = await _titleRepository.RequireTitle(request.Id);
            
            var updated = original.Copy();
            updated.Name = request.Name;
            updated.Default = request.Default;
            
            updated = await _titleRepository.UpdateTitle(original: original, updated: updated);
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

