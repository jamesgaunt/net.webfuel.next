using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteSite: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteSiteHandler : IRequestHandler<DeleteSite>
    {
        private readonly ISiteRepository _siteRepository;
        
        
        public DeleteSiteHandler(ISiteRepository siteRepository)
        {
            _siteRepository = siteRepository;
        }
        
        public async Task Handle(DeleteSite request, CancellationToken cancellationToken)
        {
            await _siteRepository.DeleteSite(request.Id);
        }
    }
}

