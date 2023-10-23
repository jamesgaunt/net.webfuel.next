using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public interface ICreateSupportRequestService
    {
        Task<SupportRequest> CreateSupportRequest(CreateSupportRequest request);
    }

    [Service(typeof(ICreateSupportRequestService))]
    internal class CreateSupportRequestService : ICreateSupportRequestService
    {
        private readonly ISupportRequestRepository _supportRequestRepository;

        public CreateSupportRequestService(ISupportRequestRepository supportRequestRepository)
        {
            _supportRequestRepository = supportRequestRepository;
        }

        public async Task<SupportRequest> CreateSupportRequest(CreateSupportRequest request)
        {
            var supportRequest = new SupportRequest();

            supportRequest.Title = request.Title;
            supportRequest.IsFellowshipId = request.IsFellowshipId;
            supportRequest.DateOfRequest = DateOnly.FromDateTime(DateTime.Now);
            supportRequest.FundingStreamName = request.FundingStreamName;
            supportRequest.TargetSubmissionDate = request.TargetSubmissionDate;
            supportRequest.ExperienceOfResearchAwards = request.ExperienceOfResearchAwards;
            supportRequest.IsTeamMembersConsultedId = request.IsTeamMembersConsultedId;
            supportRequest.IsResubmissionId = request.IsResubmissionId;
            supportRequest.BriefDescription = request.BriefDescription;
            supportRequest.SupportRequested = request.SupportRequested;
            supportRequest.IsLeadApplicantNHSId = request.IsLeadApplicantNHSId;
            supportRequest.ApplicationStageId = request.ApplicationStageId;
            supportRequest.FundingStreamId = request.FundingStreamId;
            supportRequest.FundingCallTypeId = request.FundingCallTypeId;
            supportRequest.HowDidYouFindUsId = request.HowDidYouFindUsId;
            supportRequest.StatusId = SupportRequestStatusEnum.ToBeTriaged;

            return await _supportRequestRepository.InsertSupportRequest(supportRequest);
        }
    }
}
