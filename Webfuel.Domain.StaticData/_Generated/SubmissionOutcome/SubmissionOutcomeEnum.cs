using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class SubmissionOutcomeEnum
    {
        public static readonly Guid Unknown = Guid.Parse("32b584a9-c539-447b-a542-df3e5283ef3c");
        public static readonly Guid Shortlisted = Guid.Parse("36969c16-31b1-4c1e-81eb-b4ccf626a98c");
        public static readonly Guid Rejected = Guid.Parse("bdbf02dc-75a7-413c-b8f5-7c610a6695aa");
        public static readonly Guid Withdrawn = Guid.Parse("b60f7e48-d744-403e-8fc1-f23445cb40c5");
        public static readonly Guid FundedFullStageOnly = Guid.Parse("38dc9caf-820d-4980-8f00-6bc260092a96");
        public static readonly Guid NotApplicable = Guid.Parse("b785f487-82fa-4149-8c7d-4a615ca90420");
        
        public static readonly SubmissionOutcome[] Values = new SubmissionOutcome[] {
            new SubmissionOutcome { Id = Guid.Parse("32b584a9-c539-447b-a542-df3e5283ef3c"), Name = "Unknown" },
            new SubmissionOutcome { Id = Guid.Parse("36969c16-31b1-4c1e-81eb-b4ccf626a98c"), Name = "Shortlisted" },
            new SubmissionOutcome { Id = Guid.Parse("bdbf02dc-75a7-413c-b8f5-7c610a6695aa"), Name = "Rejected" },
            new SubmissionOutcome { Id = Guid.Parse("b60f7e48-d744-403e-8fc1-f23445cb40c5"), Name = "Withdrawn" },
            new SubmissionOutcome { Id = Guid.Parse("38dc9caf-820d-4980-8f00-6bc260092a96"), Name = "Funded (full stage only)" },
            new SubmissionOutcome { Id = Guid.Parse("b785f487-82fa-4149-8c7d-4a615ca90420"), Name = "Not Applicable" },
        };
        
        public static SubmissionOutcome Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

