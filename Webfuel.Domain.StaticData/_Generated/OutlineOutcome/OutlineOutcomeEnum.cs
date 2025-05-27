using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class OutlineOutcomeEnum
    {
        public static readonly Guid Shortlisted = Guid.Parse("e8f599eb-cc36-4e4c-9500-462f0a2213d0");
        public static readonly Guid NotFunded = Guid.Parse("917d8a55-1d9a-4539-ac25-fec36a2ff8a7");
        public static readonly Guid Withdrawn = Guid.Parse("962cb041-bcef-4b5f-8e2e-81da23b7b7be");
        public static readonly Guid Unknown = Guid.Parse("caab14a7-801b-43e0-b9a9-1c08eb86e197");
        public static readonly Guid NotSubmitted = Guid.Parse("b4261645-69f3-4c61-a058-459a7746434a");
        public static readonly Guid NotApplicable = Guid.Parse("7f73e3e4-c886-43a2-8933-cfaa7cdabd9e");
        
        public static readonly OutlineOutcome[] Values = new OutlineOutcome[] {
            new OutlineOutcome { Id = Guid.Parse("e8f599eb-cc36-4e4c-9500-462f0a2213d0"), Name = "Shortlisted" },
            new OutlineOutcome { Id = Guid.Parse("917d8a55-1d9a-4539-ac25-fec36a2ff8a7"), Name = "Not Funded" },
            new OutlineOutcome { Id = Guid.Parse("962cb041-bcef-4b5f-8e2e-81da23b7b7be"), Name = "Withdrawn" },
            new OutlineOutcome { Id = Guid.Parse("caab14a7-801b-43e0-b9a9-1c08eb86e197"), Name = "Unknown" },
            new OutlineOutcome { Id = Guid.Parse("b4261645-69f3-4c61-a058-459a7746434a"), Name = "Not Submitted" },
            new OutlineOutcome { Id = Guid.Parse("7f73e3e4-c886-43a2-8933-cfaa7cdabd9e"), Name = "Not Applicable" },
        };
        
        public static OutlineOutcome Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

