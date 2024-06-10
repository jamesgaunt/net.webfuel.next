using MediatR;

namespace Webfuel.Domain
{
    public class DeleteProjectSubmission : IRequest
    {
        public required Guid Id { get; set; }
    }

    internal class DeleteProjectSubmissionHandler : IRequestHandler<DeleteProjectSubmission>
    {
        private readonly IProjectSubmissionRepository _projectSubmissionRepository;

        public DeleteProjectSubmissionHandler(IProjectSubmissionRepository projectSubmissionRepository)
        {
            _projectSubmissionRepository = projectSubmissionRepository;
        }

        public async Task Handle(DeleteProjectSubmission request, CancellationToken cancellationToken)
        {
            await _projectSubmissionRepository.DeleteProjectSubmission(request.Id);
        }
    }
}
