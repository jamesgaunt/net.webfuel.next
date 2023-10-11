using MediatR;

namespace Webfuel.Domain
{
    internal class DeleteProjectHandler : IRequestHandler<DeleteProject>
    {
        private readonly IProjectDeleteService _projectDeleteService;

        public DeleteProjectHandler(IProjectDeleteService projectDeleteService)
        {
            _projectDeleteService = projectDeleteService;
        }

        public async Task Handle(DeleteProject request, CancellationToken cancellationToken)
        {
            await _projectDeleteService.DeleteProject(request);
        }
    }
}
