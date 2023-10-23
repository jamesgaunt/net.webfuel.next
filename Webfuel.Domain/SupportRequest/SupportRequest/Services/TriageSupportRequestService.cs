using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public interface ITriageSupportRequestService
    {
        Task<Project?> TriageSupportRequest(TriageSupportRequest request);
    }

    [Service(typeof(ITriageSupportRequestService))]
    internal class TriageSupportRequestService : ITriageSupportRequestService
    {
        private readonly ISupportRequestRepository _supportRequestRepository;
        private readonly ICreateProjectService _createProjectService;

        public TriageSupportRequestService(ISupportRequestRepository supportRequestRepository, ICreateProjectService createProjectService)
        {
            _supportRequestRepository = supportRequestRepository;
            _createProjectService = createProjectService;
        }

        public async Task<Project?> TriageSupportRequest(TriageSupportRequest request)
        {
            var supportRequest = await _supportRequestRepository.RequireSupportRequest(request.Id);

            if (supportRequest.StatusId == SupportRequestStatusEnum.ReferredToNIHRRSSExpertTeams)
            {
                throw new InvalidOperationException("The specified support request has already been referred to expert teams");
            }

            if (request.StatusId != SupportRequestStatusEnum.ReferredToNIHRRSSExpertTeams)
            {
                var updated = supportRequest.Copy();
                updated.StatusId = request.StatusId;
                await _supportRequestRepository.UpdateSupportRequest(updated: updated, original: supportRequest);
                return null;
            }
            else
            {
                // This support request is being referred to expert teams, so create a project
                var project = await this._createProjectService.CreateProject(supportRequest);

                var updated = supportRequest.Copy();
                updated.StatusId = request.StatusId;
                updated.ProjectId = project.Id;
                await _supportRequestRepository.UpdateSupportRequest(updated: updated, original: supportRequest);
                
                return project;
            }
        }
    }
}
