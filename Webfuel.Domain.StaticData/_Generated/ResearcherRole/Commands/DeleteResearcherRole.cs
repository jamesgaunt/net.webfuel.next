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
        
        
        public DeleteResearcherRoleHandler(IResearcherRoleRepository researcherRoleRepository)
        {
            _researcherRoleRepository = researcherRoleRepository;
        }
        
        public async Task Handle(DeleteResearcherRole request, CancellationToken cancellationToken)
        {
            await _researcherRoleRepository.DeleteResearcherRole(request.Id);
        }
    }
}

