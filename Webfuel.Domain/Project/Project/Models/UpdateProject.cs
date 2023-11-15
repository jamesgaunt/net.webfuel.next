using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class UpdateProject: IRequest<Project>
    {
        public required Guid Id { get; set; }
        public required Guid StatusId { get; set; }


        public Guid? SubmittedFundingStreamId { get; set; }
        public string SubmittedFundingStreamFreeText { get; set; } = String.Empty;
        public string SubmittedFundingStreamName { get; set; } = String.Empty;

        // Clinical Trial Submissions

        public DateOnly? ProjectStartDate { get; set; } = null;
        public int? RecruitmentTarget { get; set; } = null;
        public int? NumberOfProjectSites { get; set; } = null;
        public Guid? IsInternationalMultiSiteStudyId { get; set; }
    }
}
