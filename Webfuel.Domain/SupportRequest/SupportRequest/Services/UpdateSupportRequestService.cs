using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public interface IUpdateSupportRequestService
    {
        Task<SupportRequest> UpdateSupportRequest(UpdateSupportRequest request);

        Task<SupportRequest> UpdateSupportRequestResearcher(UpdateSupportRequestResearcher request);

        Task<SupportRequest> UpdateSupportRequestStatus(UpdateSupportRequestStatus request);
    }

    [Service(typeof(IUpdateSupportRequestService))]
    internal class UpdateSupportRequestService : IUpdateSupportRequestService
    {
        private readonly ISupportRequestRepository _supportRequestRepository;
        private readonly ICreateProjectService _createProjectService;
        private readonly IMediator _mediator;
        private readonly IIdentityAccessor _identityAccessor;
        private readonly IStaticDataService _staticDataService;
        private readonly ISupportRequestChangeLogService _supportRequestChangeLogService;

        public UpdateSupportRequestService(
            ISupportRequestRepository supportRequestRepository,
            ICreateProjectService createProjectService,
            IMediator mediator,
            IIdentityAccessor identityAccessor,
            IStaticDataService staticDataService,
            ISupportRequestChangeLogService supportRequestChangeLogService)
        {
            _supportRequestRepository = supportRequestRepository;
            _createProjectService = createProjectService;
            _mediator = mediator;
            _identityAccessor = identityAccessor;
            _staticDataService = staticDataService;
            _supportRequestChangeLogService = supportRequestChangeLogService;
        }

        public async Task<SupportRequest> UpdateSupportRequest(UpdateSupportRequest request)
        {
            var original = await _supportRequestRepository.RequireSupportRequest(request.Id);
            var updated = original.Copy();
            new SupportRequestMapper().Apply(request, updated);
            updated = await _supportRequestRepository.UpdateSupportRequest(original: original, updated: updated);

            await _supportRequestChangeLogService.InsertChangeLog(original: original, updated: updated);
            return updated;
        }

        public async Task<SupportRequest> UpdateSupportRequestResearcher(UpdateSupportRequestResearcher request)
        {
            var original = await _supportRequestRepository.RequireSupportRequest(request.Id);
            var updated = original.Copy();
            new SupportRequestMapper().Apply(request, updated);
            updated = await _supportRequestRepository.UpdateSupportRequest(original: original, updated: updated);

            await _supportRequestChangeLogService.InsertChangeLog(original: original, updated: updated);
            return updated;
        }

        public async Task<SupportRequest> UpdateSupportRequestStatus(UpdateSupportRequestStatus request)
        {
            var original = await _supportRequestRepository.RequireSupportRequest(request.Id);
            var updated = original.Copy();

            updated = await UpdateSupportRequestStatus(updated, request);

            await _supportRequestChangeLogService.InsertChangeLog(original: original, updated: updated);
            return updated;
        }

        // Implementation

        async Task<SupportRequest> UpdateSupportRequestStatus(SupportRequest supportRequest, UpdateSupportRequestStatus request)
        {
            if (supportRequest.StatusId == request.StatusId)
                return supportRequest;

            var oldStatus = await _staticDataService.RequireSupportRequestStatus(supportRequest.StatusId);
            var newStatus = await _staticDataService.RequireSupportRequestStatus(request.StatusId);

            if (oldStatus.Id == SupportRequestStatusEnum.ReferredToNIHRRSSExpertTeams)
                throw new InvalidOperationException("The specified support request has already been referred to expert teams");

            if (newStatus.Id != SupportRequestStatusEnum.ReferredToNIHRRSSExpertTeams)
            {
                var updated = supportRequest.Copy();
                updated.StatusId = newStatus.Id;
                updated = await _supportRequestRepository.UpdateSupportRequest(original: supportRequest, updated: updated);
                return updated;
            }
            else
            {
                // This support request is being referred to expert teams, so create a project
                var project = await this._createProjectService.CreateProject(supportRequest);

                var updated = supportRequest.Copy();
                updated.StatusId = newStatus.Id;
                updated.ProjectId = project.Id;
                await _supportRequestRepository.UpdateSupportRequest(updated: updated, original: supportRequest);

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
                        WorkTimeInHours = request.WorkTimeInHours
                    });
                }

                return updated;
            }
        }
    }
}
