using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public interface IUpdateSupportRequestService
    {
        Task<SupportRequest> UpdateSupportRequest(UpdateSupportRequest request);
    }

    [Service(typeof(IUpdateSupportRequestService))]
    internal class UpdateSupportRequestService: IUpdateSupportRequestService
    {
        private readonly ISupportRequestRepository _supportRequestRepository;

        public UpdateSupportRequestService(ISupportRequestRepository supportRequestRepository)
        {
            _supportRequestRepository = supportRequestRepository;
        }

        public async Task<SupportRequest> UpdateSupportRequest(UpdateSupportRequest request)
        {
            var original = await _supportRequestRepository.RequireSupportRequest(request.Id);

            var updated = original.Copy();

            updated.Title = request.Title;
            updated.IsFellowshipId = request.IsFellowshipId;
            updated.DateOfRequest = DateOnly.FromDateTime(DateTime.Now);
            updated.FundingStreamName = request.FundingStreamName;
            updated.TargetSubmissionDate = request.TargetSubmissionDate;
            updated.ExperienceOfResearchAwards = request.ExperienceOfResearchAwards;
            updated.IsTeamMembersConsultedId = request.IsTeamMembersConsultedId;
            updated.IsResubmissionId = request.IsResubmissionId;
            updated.BriefDescription = request.BriefDescription;
            updated.SupportRequested = request.SupportRequested;
            updated.IsLeadApplicantNHSId = request.IsLeadApplicantNHSId;
            updated.ApplicationStageId = request.ApplicationStageId;
            updated.FundingStreamId = request.FundingStreamId;
            updated.FundingCallTypeId = request.FundingCallTypeId;
            updated.HowDidYouFindUsId = request.HowDidYouFindUsId;

            return await _supportRequestRepository.UpdateSupportRequest(original: original, updated: updated);
        }
    }
}
