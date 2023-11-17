using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateTitle: IRequest<Title>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
    }
    internal class CreateTitleHandler : IRequestHandler<CreateTitle, Title>
    {
        private readonly ITitleRepository _titleRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public CreateTitleHandler(ITitleRepository titleRepository, IStaticDataCache staticDataCache)
        {
            _titleRepository = titleRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task<Title> Handle(CreateTitle request, CancellationToken cancellationToken)
        {
            var updated = await _titleRepository.InsertTitle(new Title {
                    Name = request.Name,
                    Default = request.Default,
                    SortOrder = await _titleRepository.CountTitle(),
                });
            
            _staticDataCache.FlushStaticData();
            return updated;
        }
    }
}

