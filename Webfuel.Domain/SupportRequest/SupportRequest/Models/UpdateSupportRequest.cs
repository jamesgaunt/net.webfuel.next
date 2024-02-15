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
        public string ProposedFundingStreamName { get; set; } = String.Empty;
        public string NIHRApplicationId { get; set; } = String.Empty;
        public DateOnly? TargetSubmissionDate { get; set; } = null;
        public string ExperienceOfResearchAwards { get; set; } = String.Empty;
        public string BriefDescription { get; set; } = String.Empty;
        public string SupportRequested { get; set; } = String.Empty;
        public Guid? IsFellowshipId { get; set; }
        public Guid? IsTeamMembersConsultedId { get; set; }
        public Guid? IsResubmissionId { get; set; }
        public Guid? ApplicationStageId { get; set; }
        public string ApplicationStageFreeText { get; set; } = String.Empty;
        public Guid? ProposedFundingStreamId { get; set; }
        public Guid? ProposedFundingCallTypeId { get; set; }
        public Guid? HowDidYouFindUsId { get; set; }
        public string HowDidYouFindUsFreeText { get; set; } = String.Empty;
        public string WhoElseIsOnTheStudyTeam { get; set; } = String.Empty;
        public Guid? IsCTUAlreadyInvolvedId { get; set; }
        public string IsCTUAlreadyInvolvedFreeText { get; set; } = String.Empty;
        public List<Guid> ProfessionalBackgroundIds { get; set; } = new List<Guid>(); // 1.2 Development
        public string ProfessionalBackgroundFreeText { get; set; } = String.Empty; // 1.2 Development   
    }
}
