using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class SupportRequestStatusEnum
    {
        public static readonly Guid ToBeTriaged = Guid.Parse("206b6f9d-feee-4b28-9795-1e1a2b7b887f");
        public static readonly Guid ClosedAsNotEligibleForNIHRRSS = Guid.Parse("1fa7ae4c-8bb7-49da-9efc-f9aa96de4c1d");
        public static readonly Guid ClosedAsReferredToAnotherNIHRRSS = Guid.Parse("3d344c3c-9964-4079-877e-bc3b6864f52d");
        public static readonly Guid ReferredToNIHRRSSExpertTeams = Guid.Parse("c22df21d-30ed-49d5-bee0-0f304b74a365");
        
        public static readonly SupportRequestStatus[] Values = new SupportRequestStatus[] {
            new SupportRequestStatus { Id = Guid.Parse("206b6f9d-feee-4b28-9795-1e1a2b7b887f"), Name = "To be triaged", Default = true },
            new SupportRequestStatus { Id = Guid.Parse("1fa7ae4c-8bb7-49da-9efc-f9aa96de4c1d"), Name = "Closed as not eligible for NIHR RSS" },
            new SupportRequestStatus { Id = Guid.Parse("3d344c3c-9964-4079-877e-bc3b6864f52d"), Name = "Closed as referred to another NIHR RSS" },
            new SupportRequestStatus { Id = Guid.Parse("c22df21d-30ed-49d5-bee0-0f304b74a365"), Name = "Referred to NIHR RSS expert teams" },
        };
        
        public static SupportRequestStatus Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

