using FluentValidation;
using MediatR;

namespace Webfuel.Domain
{
    public class GetProjectSubmission : IRequest<ProjectSubmission?>
    {
        public Guid Id { get; set; }
    }

    internal class GetProjectSubmissionHandler : IRequestHandler<GetProjectSubmission, ProjectSubmission?>
    {
        private readonly IProjectSubmissionRepository _projectSubmissionRepository;

        public GetProjectSubmissionHandler(IProjectSubmissionRepository projectSubmissionRepository)
        {
            _projectSubmissionRepository = projectSubmissionRepository;
        }

        public async Task<ProjectSubmission?> Handle(GetProjectSubmission request, CancellationToken cancellationToken)
        {
            return await _projectSubmissionRepository.GetProjectSubmission(request.Id);
        }
    }
}
