using MediatR;
using Microsoft.Identity.Client;

namespace Webfuel.Domain
{
    public class CreateProjectSupport : IRequest<ProjectSupport>
    {
        public required Guid ProjectId { get; set; }

        public DateOnly? Date { get; set; }

        public required List<Guid> TeamIds { get; set; }

        public required List<Guid> AdviserIds { get; set; }

        public required List<Guid> SupportProvidedIds { get; set; }

        public string Description { get; set; } = String.Empty;

        public required Decimal WorkTimeInHours { get; set; }

        public Guid? SupportRequestedTeamId { get; set; }

        public required Guid IsPrePostAwardId { get; set; }
    }

    internal class CreateProjectSupportHandler : IRequestHandler<CreateProjectSupport, ProjectSupport>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectSupportRepository _projectSupportRepository;
        private readonly IProjectEnrichmentService _projectEnrichmentService;
        private readonly IProjectAdviserService _projectAdviserService;
        private readonly IUserActivityRepository _userActivityRepository;
        private readonly IUserSortService _userSortService;

        public CreateProjectSupportHandler(
            IProjectRepository projectRepository,
            IProjectSupportRepository projectSupportRepository,
            IProjectEnrichmentService projectEnrichmentService,
            IProjectAdviserService projectAdviserService,
            IUserActivityRepository userActivityRepository,
            IUserSortService userSortService)
        {
            _projectRepository = projectRepository;
            _projectSupportRepository = projectSupportRepository;
            _projectEnrichmentService = projectEnrichmentService;
            _projectAdviserService = projectAdviserService;
            _userActivityRepository = userActivityRepository;
            _userSortService = userSortService;
        }

        public async Task<ProjectSupport> Handle(CreateProjectSupport request, CancellationToken cancellationToken)
        {
            await Sanitize(request);

            var project = await _projectRepository.RequireProject(request.ProjectId);
            if (project.Locked)
                throw new InvalidOperationException("Unable to edit a locked project");

            var projectSupport = new ProjectSupport();
            projectSupport.ProjectId = request.ProjectId;
            projectSupport.Date = request.Date ?? DateOnly.FromDateTime(DateTime.Now);
            projectSupport.TeamIds = request.TeamIds;
            projectSupport.AdviserIds = request.AdviserIds;
            projectSupport.SupportProvidedIds = request.SupportProvidedIds;
            projectSupport.Description = request.Description;
            projectSupport.WorkTimeInHours = request.WorkTimeInHours;
            projectSupport.SupportRequestedTeamId = request.SupportRequestedTeamId;
            projectSupport.IsPrePostAwardId = request.IsPrePostAwardId;

            // Calculated
            projectSupport.CalculatedMinutes = (int)(projectSupport.WorkTimeInHours * 60) * projectSupport.AdviserIds.Count;

            var cb = new RepositoryCommandBuffer();
            {
                projectSupport = await _projectSupportRepository.InsertProjectSupport(projectSupport, cb);
                await SyncroniseUserActivity(project, projectSupport, cb);
            }
            await cb.Execute();

            {
                var updatedProject = project.Copy();
                await _projectEnrichmentService.CalculateSupportMetricsForProject(updatedProject);
                await _projectRepository.UpdateProject(original: project, updated: updatedProject);
            }

            TeamSupportProvider.FlushSupportMetrics();

            if (projectSupport.SupportRequestedTeamId.HasValue)
                await _projectAdviserService.SendTeamSupportRequestedEmail(project: project, supportTeamId: projectSupport.SupportRequestedTeamId.Value);

            return projectSupport;
        }

        async Task SyncroniseUserActivity(Project project, ProjectSupport projectSupport, RepositoryCommandBuffer cb)
        {
            foreach (var adviserId in projectSupport.AdviserIds)
            {
                await _userActivityRepository.InsertUserActivity(new UserActivity
                {
                    UserId = adviserId,
                    Date = projectSupport.Date,
                    Description = projectSupport.Description,
                    WorkTimeInHours = projectSupport.WorkTimeInHours,

                    ProjectId = projectSupport.ProjectId,
                    ProjectSupportId = projectSupport.Id,
                    ProjectPrefixedNumber = project.PrefixedNumber,
                    ProjectSupportProvidedIds = projectSupport.SupportProvidedIds
                }, cb);
            }
        }

        public async Task Sanitize(CreateProjectSupport request)
        {
            if (request.WorkTimeInHours < 0)
                request.WorkTimeInHours = 0;
            if (request.WorkTimeInHours > 8)
                request.WorkTimeInHours = 8;

            request.AdviserIds = await _userSortService.SortUserIds(request.AdviserIds);
        }
    }
}