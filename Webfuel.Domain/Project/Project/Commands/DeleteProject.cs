using MediatR;

namespace Webfuel.Domain
{
    public class DeleteProject : IRequest
    {
        public required Guid Id { get; set; }
    }

    internal class DeleteProjectHandler : IRequestHandler<DeleteProject>
    {
        private readonly IProjectRepository _projectRepository;

        public DeleteProjectHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task Handle(DeleteProject request, CancellationToken cancellationToken)
        {
            await _projectRepository.DeleteProject(request.Id);

            ProjectSummaryProvider.FlushProjectMetrics();
        }
    }
}
