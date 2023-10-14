using MediatR;

namespace Webfuel.Domain
{
    internal class DeleteProjectHandler : IRequestHandler<DeleteProject>
    {
        private readonly IDeleteProjectService _projectDeleteService;

        public DeleteProjectHandler(IDeleteProjectService projectDeleteService)
        {
            _projectDeleteService = projectDeleteService;
        }

        public async Task Handle(DeleteProject request, CancellationToken cancellationToken)
        {
            await _projectDeleteService.DeleteProject(request);
        }
    }
}
