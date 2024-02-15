using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain.Dashboard;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public interface IUpdateProjectService
    {
        Task<Project> UpdateProject(UpdateProject request);

        Task<Project> UpdateProjectRequest(UpdateProjectRequest request);

        Task<Project> UpdateProjectResearcher(UpdateProjectResearcher request);

        Task<Project> UpdateProjectStatus(UpdateProjectStatus request);
    }

    [Service(typeof(IUpdateProjectService))]
    internal class UpdateProjectService : IUpdateProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IStaticDataService _staticDataService;
        private readonly IEnrichProjectService _enrichProjectService;
        private readonly IProjectAdviserService _projectAdviserService;
        private readonly IProjectChangeLogService _projectChangeLogService;

        public UpdateProjectService(
            IProjectRepository projectRepository,
            IStaticDataService staticDataService,
            IEnrichProjectService enrichProjectService,
            IProjectAdviserService projectAdviserService,
            IProjectChangeLogService projectChangeLogService)
        {
            _projectRepository = projectRepository;
            _staticDataService = staticDataService;
            _enrichProjectService = enrichProjectService;
            _projectAdviserService = projectAdviserService;
            _projectChangeLogService = projectChangeLogService;
        }

        public async Task<Project> UpdateProject(UpdateProject request)
        {
            var original = await _projectRepository.RequireProject(request.Id);
            var updated = original.Copy();
            new ProjectMapper().Apply(request, updated);
            updated = await _projectRepository.UpdateProject(original: original, updated: updated);

            if (updated.StatusId != request.StatusId)
            {
                updated = await UpdateProjectStatus(updated, new UpdateProjectStatus
                {
                    Id = updated.Id,
                    StatusId = request.StatusId,
                    ClosureDate = updated.ClosureDate
                });
            }

            await _projectChangeLogService.InsertChangeLog(original: original, updated: updated);

            await _projectAdviserService.UpdateProjectAdvisers(request.Id, request.ProjectAdviserUserIds);

            return await _enrichProjectService.EnrichProject(updated);
        }

        public async Task<Project> UpdateProjectRequest(UpdateProjectRequest request)
        {
            var original = await _projectRepository.RequireProject(request.Id);
            var updated = original.Copy();
            new ProjectMapper().Apply(request, updated);
            updated = await _projectRepository.UpdateProject(original: original, updated: updated);

            await _projectChangeLogService.InsertChangeLog(original: original, updated: updated);
            return updated;
        }

        public async Task<Project> UpdateProjectResearcher(UpdateProjectResearcher request)
        {
            var original = await _projectRepository.RequireProject(request.Id);
            var updated = original.Copy();
            new ProjectMapper().Apply(request, updated);
            updated = await _projectRepository.UpdateProject(original: original, updated: updated);

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

            var newStatus = await _staticDataService.RequireProjectStatus(request.StatusId);

            var updated = project.Copy();

            updated.StatusId = newStatus.Id;
            updated.Locked = newStatus.Locked;
            updated.Discarded = newStatus.Discarded;

            if (newStatus.Id == ProjectStatusEnum.Closed)
                updated.ClosureDate = request.ClosureDate ?? updated.ClosureDate ?? DateOnly.FromDateTime(DateTime.Today);

            updated = await _projectRepository.UpdateProject(original: project, updated: updated);

            DashboardService.FlushProjectMetrics();
            return updated;
        }
    }
}
