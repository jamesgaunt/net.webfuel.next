using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class SubmissionStatusEnum
    {
        public static readonly Guid Yes = Guid.Parse("82a179e1-9a82-4a68-a883-5800bc2000d4");
        public static readonly Guid No = Guid.Parse("6f8e9238-6b7e-4bd4-97ce-a0584d802ee4");
        public static readonly Guid DonTKnow = Guid.Parse("f01c0b96-7feb-490e-b919-331df4fc47a0");
        public static readonly Guid NA = Guid.Parse("f319f56f-8434-45af-96fb-003bd29774b2");
        
        public static readonly SubmissionStatus[] Values = new SubmissionStatus[] {
            new SubmissionStatus { Id = Guid.Parse("82a179e1-9a82-4a68-a883-5800bc2000d4"), Name = "Yes" },
            new SubmissionStatus { Id = Guid.Parse("6f8e9238-6b7e-4bd4-97ce-a0584d802ee4"), Name = "No" },
            new SubmissionStatus { Id = Guid.Parse("f01c0b96-7feb-490e-b919-331df4fc47a0"), Name = "Don't know" },
            new SubmissionStatus { Id = Guid.Parse("f319f56f-8434-45af-96fb-003bd29774b2"), Name = "N/A" },
        };
        
        public static SubmissionStatus Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

