using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteResearcherRole: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteResearcherRoleHandler : IRequestHandler<DeleteResearcherRole>
    {
        private readonly IResearcherRoleRepository _researcherRoleRepository;
        private readonly IStaticDataCache _staticDataCache;
        
        
        public DeleteResearcherRoleHandler(IResearcherRoleRepository researcherRoleRepository, IStaticDataCache staticDataCache)
        {
            _researcherRoleRepository = researcherRoleRepository;
            _staticDataCache = staticDataCache;
        }
        
        public async Task Handle(DeleteResearcherRole request, CancellationToken cancellationToken)
        {
            await _researcherRoleRepository.DeleteResearcherRole(request.Id);
            _staticDataCache.FlushStaticData();
        }
    }
}

