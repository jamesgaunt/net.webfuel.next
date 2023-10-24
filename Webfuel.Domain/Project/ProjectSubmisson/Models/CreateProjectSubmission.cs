using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class CreateProjectSubmission : IRequest<ProjectSubmission>
    {
        public required Guid ProjectId { get; set; }

        public DateOnly SubmissionDate { get; set; }

        public required string NIHRReference { get; set; }

        public required Guid SubmissionStageId { get; set; }

        public Guid? SubmissionOutcomeId { get; set; }

        public int FundingAmountOnSubmission { get; set; }
    }
}
