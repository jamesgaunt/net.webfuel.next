using MediatR;

namespace Webfuel.Domain
{
    public class UpdateProjectSubmission : IRequest<ProjectSubmission>
    {
        public required Guid Id { get; set; }

        public DateOnly SubmissionDate { get; set; }

        public required string NIHRReference { get; set; }

        public required Guid SubmissionStageId { get; set; }

        public Guid? SubmissionOutcomeId { get; set; }

        public int FundingAmountOnSubmission { get; set; }
    }

    internal class UpdateProjectSubmissionHandler : IRequestHandler<UpdateProjectSubmission, ProjectSubmission>
    {
        private readonly IProjectSubmissionRepository _projectSubmissionRepository;

        public UpdateProjectSubmissionHandler(IProjectSubmissionRepository projectSubmissionRepository)
        {
            _projectSubmissionRepository = projectSubmissionRepository;
        }

        public async Task<ProjectSubmission> Handle(UpdateProjectSubmission request, CancellationToken cancellationToken)
        {
            var projectSubmission = await _projectSubmissionRepository.RequireProjectSubmission(request.Id);

            var updated = projectSubmission.Copy();
            updated.SubmissionDate = request.SubmissionDate;
            updated.NIHRReference = request.NIHRReference;
            updated.SubmissionStageId = request.SubmissionStageId;
            updated.SubmissionOutcomeId = request.SubmissionOutcomeId;
            updated.FundingAmountOnSubmission = request.FundingAmountOnSubmission;

            return await _projectSubmissionRepository.UpdateProjectSubmission(updated: updated, original: projectSubmission);
        }
    }
}