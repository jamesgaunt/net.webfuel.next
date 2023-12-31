﻿using System;
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

        Task<Project> UpdateProjectStatus(UpdateProjectStatus request);
    }

    [Service(typeof(IUpdateProjectService))]
    internal class UpdateProjectService : IUpdateProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IStaticDataService _staticDataService;
        private readonly IProjectChangeLogService _projectChangeLogService;

        public UpdateProjectService(
            IProjectRepository projectRepository, 
            IStaticDataService staticDataService,
            IProjectChangeLogService projectChangeLogService)
        {
            _projectRepository = projectRepository;
            _staticDataService = staticDataService;
            _projectChangeLogService = projectChangeLogService;
        }

        public async Task<Project> UpdateProject(UpdateProject request)
        {
            var original = await _projectRepository.RequireProject(request.Id);

            var staticData = await _staticDataService.GetStaticData();

            var updated = original.Copy();

            updated.ProjectStartDate = request.ProjectStartDate;
            updated.RecruitmentTarget = request.RecruitmentTarget;
            updated.NumberOfProjectSites = request.NumberOfProjectSites;
            updated.IsInternationalMultiSiteStudyId = request.IsInternationalMultiSiteStudyId;

            updated.LeadAdviserUserId = request.LeadAdviserUserId;

            updated.SubmittedFundingStreamId = request.SubmittedFundingStreamId;
            updated.SubmittedFundingStreamFreeText = request.SubmittedFundingStreamFreeText;
            updated.SubmittedFundingStreamName = request.SubmittedFundingStreamName;

            updated = await _projectRepository.UpdateProject(original: original, updated: updated);

            if (updated.StatusId != request.StatusId)
            {
                updated = await UpdateProjectStatus(updated, new UpdateProjectStatus
                {
                    Id = updated.Id,
                    StatusId = request.StatusId
                });
            }

            await _projectChangeLogService.InsertChangeLog(original: original, updated: updated);
            return updated;
        }

        public async Task<Project> UpdateProjectStatus(UpdateProjectStatus request)
        {
            var original = await _projectRepository.RequireProject(request.Id);
            var updated = original.Copy();

            updated = await UpdateProjectStatus(updated, request);

            await _projectChangeLogService.InsertChangeLog(original: original, updated: updated);
            return updated;
        }

        // Implementation

        async Task<Project> UpdateProjectStatus(Project project, UpdateProjectStatus request)
        {
            if (project.StatusId == request.StatusId)
                return project;

            var oldStatus = await _staticDataService.RequireProjectStatus(project.StatusId);
            var newStatus = await _staticDataService.RequireProjectStatus(request.StatusId);

            var updated = project.Copy();

            updated.StatusId = newStatus.Id;
            updated.Locked = newStatus.Locked;
            updated.Discarded = newStatus.Discarded;

            if (newStatus.Id == ProjectStatusEnum.Closed && updated.ClosureDate == null)
                updated.ClosureDate = DateOnly.FromDateTime(DateTime.Today);

            updated = await _projectRepository.UpdateProject(original: project, updated: updated);
            return updated;
        }
    }
}
