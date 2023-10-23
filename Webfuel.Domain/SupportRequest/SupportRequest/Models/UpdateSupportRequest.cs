using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class UpdateSupportRequest: IRequest<SupportRequest>
    {
        public required Guid Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string FundingStreamName { get; set; } = String.Empty;
        public DateOnly? TargetSubmissionDate { get; set; } = null;
        public string ExperienceOfResearchAwards { get; set; } = String.Empty;
        public string BriefDescription { get; set; } = String.Empty;
        public string SupportRequested { get; set; } = String.Empty;
        public Guid? IsFellowshipId { get; set; }
        public Guid? IsTeamMembersConsultedId { get; set; }
        public Guid? IsResubmissionId { get; set; }
        public Guid? IsLeadApplicantNHSId { get; set; }
        public Guid? ApplicationStageId { get; set; }
        public Guid? FundingStreamId { get; set; }
        public Guid? FundingCallTypeId { get; set; }
        public Guid? HowDidYouFindUsId { get; set; }
    }
}
