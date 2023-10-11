using MediatR;

namespace Webfuel.Domain.StaticData
{
    public class DeleteResearchMethodology: IRequest
    {
        public required Guid Id { get; set; }
    }
    internal class DeleteResearchMethodologyHandler : IRequestHandler<DeleteResearchMethodology>
    {
        private readonly IResearchMethodologyRepository _researchMethodologyRepository;
        
        
        public DeleteResearchMethodologyHandler(IResearchMethodologyRepository researchMethodologyRepository)
        {
            _researchMethodologyRepository = researchMethodologyRepository;
        }
        
        public async Task Handle(DeleteResearchMethodology request, CancellationToken cancellationToken)
        {
            await _researchMethodologyRepository.DeleteResearchMethodology(request.Id);
        }
    }
}

