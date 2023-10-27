using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain.StaticData;

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
        private readonly IStaticDataService _staticDataService;

        public UpdateProjectService(IProjectRepository projectRepository, IStaticDataService staticDataService)
        {
            _projectRepository = projectRepository;
            _staticDataService = staticDataService;
        }

        public async Task<Project> UpdateProject(UpdateProject request)
        {
            var original = await _projectRepository.RequireProject(request.Id);

            var staticData = await _staticDataService.GetStaticData();

            var updated = original.Copy();

            updated.IsQuantativeTeamContributionId = request.IsQuantativeTeamContributionId;
            updated.IsCTUTeamContributionId = request.IsCTUTeamContributionId;
            updated.IsPPIEAndEDIContributionId = request.IsPPIEAndEDIContributionId;
            updated.SubmittedFundingStreamName = request.SubmittedFundingStreamName;

            updated.ProjectStartDate = request.ProjectStartDate;
            updated.RecruitmentTarget = request.RecruitmentTarget;
            updated.NumberOfProjectSites = request.NumberOfProjectSites;
            updated.IsInternationalMultiSiteStudyId = request.IsInternationalMultiSiteStudyId;

            updated.SubmittedFundingStreamId = request.SubmittedFundingStreamId;
            updated.SubmittedFundingStreamFreeText = request.SubmittedFundingStreamFreeText;

            if (!staticData.FundingStream.HasFreeText(updated.SubmittedFundingStreamId))
                updated.SubmittedFundingStreamFreeText = String.Empty;

            return await _projectRepository.UpdateProject(original: original, updated: updated);
        }
    }
}
