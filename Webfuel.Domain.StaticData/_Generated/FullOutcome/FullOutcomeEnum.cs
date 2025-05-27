using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class FullOutcomeEnum
    {
        public static readonly Guid Successful = Guid.Parse("c9dbd5ad-6c78-4773-a3a8-ce30412bb5dc");
        public static readonly Guid NotFunded = Guid.Parse("6a59516c-9472-4c63-9611-f651bdbfcff2");
        public static readonly Guid Withdrawn = Guid.Parse("cd81994e-1bc1-4404-a023-eea57e3fd114");
        public static readonly Guid Unknown = Guid.Parse("2b33d4c2-0340-4a57-9c75-203607fed107");
        public static readonly Guid NotSubmitted = Guid.Parse("f73be486-7d1d-4573-b76c-c0fda7cab4c5");
        public static readonly Guid NotApplicable = Guid.Parse("ba13b285-2429-4952-8b79-fc515eeaccbf");
        
        public static readonly FullOutcome[] Values = new FullOutcome[] {
            new FullOutcome { Id = Guid.Parse("c9dbd5ad-6c78-4773-a3a8-ce30412bb5dc"), Name = "Successful" },
            new FullOutcome { Id = Guid.Parse("6a59516c-9472-4c63-9611-f651bdbfcff2"), Name = "Not Funded" },
            new FullOutcome { Id = Guid.Parse("cd81994e-1bc1-4404-a023-eea57e3fd114"), Name = "Withdrawn" },
            new FullOutcome { Id = Guid.Parse("2b33d4c2-0340-4a57-9c75-203607fed107"), Name = "Unknown" },
            new FullOutcome { Id = Guid.Parse("f73be486-7d1d-4573-b76c-c0fda7cab4c5"), Name = "Not Submitted" },
            new FullOutcome { Id = Guid.Parse("ba13b285-2429-4952-8b79-fc515eeaccbf"), Name = "Not Applicable" },
        };
        
        public static FullOutcome Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

