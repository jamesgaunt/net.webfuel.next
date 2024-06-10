using MediatR;

namespace Webfuel.Domain
{
    public class CreateProjectSubmission : IRequest<ProjectSubmission>
    {
        public required Guid ProjectId { get; set; }

        public DateOnly SubmissionDate { get; set; }

        public required string NIHRReference { get; set; }

        public required Guid SubmissionStageId { get; set; }

        public Guid? SubmissionOutcomeId { get; set; }

        public int FundingAmountOnSubmission { get; set; }
    }

    internal class CreateProjectSubmissionHandler : IRequestHandler<CreateProjectSubmission, ProjectSubmission>
    {
        private readonly IProjectSubmissionRepository _projectSubmissionRepository;

        public CreateProjectSubmissionHandler(IProjectSubmissionRepository projectSubmissionRepository)
        {
            _projectSubmissionRepository = projectSubmissionRepository;
        }

        public async Task<ProjectSubmission> Handle(CreateProjectSubmission request, CancellationToken cancellationToken)
        {
            var projectSubmission = new ProjectSubmission();

            projectSubmission.ProjectId = request.ProjectId;
            projectSubmission.SubmissionDate = request.SubmissionDate;
            projectSubmission.NIHRReference = request.NIHRReference;
            projectSubmission.SubmissionStageId = request.SubmissionStageId;
            projectSubmission.SubmissionOutcomeId = request.SubmissionOutcomeId;
            projectSubmission.FundingAmountOnSubmission = request.FundingAmountOnSubmission;

            return await _projectSubmissionRepository.InsertProjectSubmission(projectSubmission);
        }
    }
}