using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public interface ICreateProjectService
    {
        Task<Project> CreateProject(CreateProject command);

        Task<Project> CreateProject(SupportRequest supportRequest);
    }

    [Service(typeof(ICreateProjectService))]
    internal class CreateProjectService : ICreateProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IConfigurationService _configurationService;

        public CreateProjectService(IProjectRepository projectRepository, IConfigurationService configurationService)
        {
            _projectRepository = projectRepository;
            _configurationService = configurationService;
        }

        public Task<Project> CreateProject(CreateProject request)
        {
            throw new NotImplementedException();
        }

        public async Task<Project> CreateProject(SupportRequest supportRequest)
        {
            var project = new Project();
            project.Number = await GetNextProjectNumber();
            project.PrefixedNumber = FormatPrefixedNumber(project);
            project.StatusId = ProjectStatusEnum.Active;
            project.CreatedAt = DateTimeOffset.UtcNow;

            // Replicate Support Request Values
            project.SupportRequestId = supportRequest.Id;
            project.DateOfRequest = supportRequest.DateOfRequest;
            project.Title = supportRequest.Title;
            project.BriefDescription = supportRequest.BriefDescription;
            project.ApplicationStageId = supportRequest.ApplicationStageId;
            project.FundingStreamId = supportRequest.FundingStreamId;
            project.FundingStreamName = supportRequest.FundingStreamName;
            project.TargetSubmissionDate = supportRequest.TargetSubmissionDate;
            project.ExperienceOfResearchAwards = supportRequest.ExperienceOfResearchAwards;
            project.IsFellowshipId = supportRequest.IsFellowshipId;
            project.IsTeamMembersConsultedId = supportRequest.IsTeamMembersConsultedId;
            project.IsResubmissionId = supportRequest.IsResubmissionId;
            project.IsLeadApplicantNHSId = supportRequest.IsLeadApplicantNHSId;
            project.SupportRequested = supportRequest.SupportRequested;
            project.HowDidYouFindUsId = supportRequest.HowDidYouFindUsId;

            return await _projectRepository.InsertProject(project);
        }

        // Implementation

        async Task<int> GetNextProjectNumber()
        {
            return await _configurationService.AllocateNextProjectNumber();
        }

        string FormatPrefixedNumber(Project project)
        {
            return "IC" + project.Number.ToString("D5");
        }
    }
}
