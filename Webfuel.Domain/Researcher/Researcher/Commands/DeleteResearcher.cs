using MediatR;

namespace Webfuel.Domain
{
    public class DeleteResearcher : IRequest
    {
        public Guid Id { get; set; }
    }

    internal class DeleteResearcherHandler : IRequestHandler<DeleteResearcher>
    {
        private readonly IResearcherRepository _researcherRepository;

        public DeleteResearcherHandler(IResearcherRepository researcherRepository)
        {
            _researcherRepository = researcherRepository;
        }

        public async Task Handle(DeleteResearcher request, CancellationToken cancellationToken)
        {
            await _researcherRepository.DeleteResearcher(request.Id);
        }
    }
}
