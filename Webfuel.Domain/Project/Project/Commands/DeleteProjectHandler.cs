using MediatR;

namespace Webfuel.Domain
{
    internal class DeleteProjectHandler : IRequestHandler<DeleteProject>
    {
        private readonly IDeleteProjectService _deleteProjectService;

        public DeleteProjectHandler(IDeleteProjectService deleteProjectService)
        {
            _deleteProjectService = deleteProjectService;
        }

        public async Task Handle(DeleteProject request, CancellationToken cancellationToken)
        {
            await _deleteProjectService.DeleteProject(request);
        }
    }
}
