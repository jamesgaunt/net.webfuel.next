using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class CreateSite: IRequest<Site>
    {
        public required string Name { get; set; }
        public bool Default { get; set; } = false;
    }
    internal class CreateSiteHandler : IRequestHandler<CreateSite, Site>
    {
        private readonly ISiteRepository _siteRepository;
        
        
        public CreateSiteHandler(ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository;
        }
        
        public async Task<Site> Handle(CreateSite request, CancellationToken cancellationToken)
        {
            return await _siteRepository.InsertSite(new Site {
                    Name = request.Name,
                    Default = request.Default,
                    SortOrder = await _siteRepository.CountSite(),
                });
        }
    }
}

