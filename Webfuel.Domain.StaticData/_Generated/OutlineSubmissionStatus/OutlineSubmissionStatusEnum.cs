using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class OutlineSubmissionStatusEnum
    {
        public static readonly Guid Submitted = Guid.Parse("1c76e5b1-c0d4-4a91-8144-afb582eb2165");
        public static readonly Guid DidNotSubmit = Guid.Parse("dcd582e2-3ca3-4705-8eb8-d64953c1d1c1");
        public static readonly Guid DonTKnow = Guid.Parse("7a4b2966-eb01-4765-9615-d873915be554");
        public static readonly Guid NA = Guid.Parse("c9a32676-5e38-42e7-bb31-586cb9e5e1f5");
        
        public static readonly OutlineSubmissionStatus[] Values = new OutlineSubmissionStatus[] {
            new OutlineSubmissionStatus { Id = Guid.Parse("1c76e5b1-c0d4-4a91-8144-afb582eb2165"), Name = "Submitted" },
            new OutlineSubmissionStatus { Id = Guid.Parse("dcd582e2-3ca3-4705-8eb8-d64953c1d1c1"), Name = "Did not submit" },
            new OutlineSubmissionStatus { Id = Guid.Parse("7a4b2966-eb01-4765-9615-d873915be554"), Name = "Don't know" },
            new OutlineSubmissionStatus { Id = Guid.Parse("c9a32676-5e38-42e7-bb31-586cb9e5e1f5"), Name = "N/A" },
        };
        
        public static OutlineSubmissionStatus Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

