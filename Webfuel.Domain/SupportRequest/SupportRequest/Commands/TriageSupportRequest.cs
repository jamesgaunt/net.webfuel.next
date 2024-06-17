using MediatR;
using Webfuel.Common;
using Webfuel.Domain.Dashboard;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public class TriageSupportRequest : IRequest<SupportRequest>
    {
        public required Guid Id { get; set; }

        public required Guid StatusId { get; set; }

        public required string TriageNote { get; set; }

        public required List<Guid> SupportProvidedIds { get; set; }

        public string Description { get; set; } = String.Empty;

        public Decimal? WorkTimeInHours { get; set; }

        public Guid? SupportRequestedTeamId { get; set; }

        public required Guid IsPrePostAwardId { get; set; }
    }

    internal class TriageSupportRequestHandler : IRequestHandler<TriageSupportRequest, SupportRequest>
    {
        private readonly ISupportRequestRepository _supportRequestRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IConfigurationService _configurationService;
        private readonly IStaticDataService _staticDataService;
        private readonly ISupportRequestChangeLogService _supportRequestChangeLogService;
        private readonly IIdentityAccessor _identityAccessor;
        private readonly IMediator _mediator;

        public TriageSupportRequestHandler(
            ISupportRequestRepository supportRequestRepository,
            IProjectRepository projectRepository,
            IConfigurationService configurationService,
            IStaticDataService staticDataService,
            ISupportRequestChangeLogService supportRequestChangeLogService,
            IIdentityAccessor identityAccessor,
            IMediator mediator)
        {
            _supportRequestRepository = supportRequestRepository;
            _projectRepository = projectRepository;
            _configurationService = configurationService;
            _staticDataService = staticDataService;
            _supportRequestChangeLogService = supportRequestChangeLogService;
            _identityAccessor = identityAccessor;
            _mediator = mediator;
        }

        public async Task<SupportRequest> Handle(TriageSupportRequest request, CancellationToken cancellationToken)
        {
            var original = await _supportRequestRepository.RequireSupportRequest(request.Id);
            if (original.StatusId == request.StatusId)
                return original; // No change to status

            var oldStatus = await _staticDataService.RequireSupportRequestStatus(original.StatusId);

            if (oldStatus.Id != SupportRequestStatusEnum.ToBeTriaged && oldStatus.Id != SupportRequestStatusEnum.OnHold)
                throw new InvalidOperationException("The specified support request has already been triaged");

            var newStatus = await _staticDataService.RequireSupportRequestStatus(request.StatusId);

            var updated = original.Copy();
            updated.StatusId = newStatus.Id;
            updated.TriageNote = request.TriageNote;
            
            if (newStatus.Id == SupportRequestStatusEnum.ReferredToNIHRRSSExpertTeams)
            {
                // This support request is being referred to expert teams, so create a new project
                var project = await CreateNewProjectFromSupportRequest(updated);
                updated.ProjectId = project.Id;

                // Record a triage support provided event on the project
                if (request.SupportProvidedIds.Count > 0 && _identityAccessor.User != null)
                {
                    await _mediator.Send(new CreateProjectSupport
                    {
                        ProjectId = project.Id,
                        AdviserIds = new List<Guid> { _identityAccessor.User.Id },
                        TeamIds = new List<Guid> { SupportTeamEnum.TriageTeam },
                        SupportProvidedIds = request.SupportProvidedIds,
                        Description = request.Description,
                        WorkTimeInHours = request.WorkTimeInHours ?? 0,
                        SupportRequestedTeamId = request.SupportRequestedTeamId,
                        IsPrePostAwardId = request.IsPrePostAwardId
                    });
                }
            }
            updated = await _supportRequestRepository.UpdateSupportRequest(updated: updated, original: original);

            await _supportRequestChangeLogService.InsertChangeLog(original: original, updated: updated);
            return updated;
        }

        async Task<Project> CreateNewProjectFromSupportRequest(SupportRequest supportRequest)
        {
            var project = SupportRequestMapper.Apply(supportRequest, new Project());

            project.Number = await _configurationService.AllocateNextProjectNumber();
            project.PrefixedNumber = FormatPrefixedNumber(project);
            project.StatusId = ProjectStatusEnum.Active;
            project.CreatedAt = DateTimeOffset.UtcNow;

            project.SupportRequestId = supportRequest.Id;
            project.FileStorageGroupId = supportRequest.FileStorageGroupId; // This will always exist

            await _projectRepository.InsertProject(project);
            DashboardService.FlushProjectMetrics();

            return project;
        }

        string FormatPrefixedNumber(Project project)
        {
            return "IC" + project.Number.ToString("D5");
        }
    }
}