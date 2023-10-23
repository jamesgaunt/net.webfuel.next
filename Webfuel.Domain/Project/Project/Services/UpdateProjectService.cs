using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public interface IUpdateProjectService
    {
        Task<Project> UpdateProject(UpdateProject request);
    }

    [Service(typeof(IUpdateProjectService))]
    internal class UpdateProjectService : IUpdateProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> UpdateProject(UpdateProject request)
        {
            var original = await _projectRepository.RequireProject(request.Id);

            var updated = original.Copy();

            updated.IsQuantativeTeamContributionId = request.IsQuantativeTeamContributionId;
            updated.IsCTUTeamContributionId = request.IsCTUTeamContributionId;
            updated.IsPPIEAndEDIContributionId = request.IsPPIEAndEDIContributionId;
            updated.SubmittedFundingStreamId = request.SubmittedFundingStreamId;
            updated.SubmittedFundingStreamName = request.SubmittedFundingStreamName;

            updated.ProjectStartDate = request.ProjectStartDate;
            updated.RecruitmentTarget = request.RecruitmentTarget;
            updated.NumberOfProjectSites = request.NumberOfProjectSites;
            updated.IsInternationalMultiSiteStudyId = request.IsInternationalMultiSiteStudyId;

            return await _projectRepository.UpdateProject(original: original, updated: updated);
        }
    }
}
