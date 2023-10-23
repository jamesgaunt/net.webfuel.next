using MediatR;

namespace Webfuel.Domain
{
    internal class DeleteProjectSupportHandler : IRequestHandler<DeleteProjectSupport>
    {
        private readonly IProjectSupportRepository _projectSupportRepository;

        public DeleteProjectSupportHandler(IProjectSupportRepository projectSupportRepository)
        {
            _projectSupportRepository = projectSupportRepository;
        }

        public async Task Handle(DeleteProjectSupport request, CancellationToken cancellationToken)
        {
            await _projectSupportRepository.DeleteProjectSupport(request.Id);
        }
    }
}
