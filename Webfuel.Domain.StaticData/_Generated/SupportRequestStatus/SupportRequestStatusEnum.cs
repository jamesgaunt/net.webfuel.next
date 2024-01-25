using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class SupportRequestStatusEnum
    {
        public static readonly Guid ToBeTriaged = Guid.Parse("206b6f9d-feee-4b28-9795-1e1a2b7b887f");
        public static readonly Guid OnHold = Guid.Parse("667ccd51-00f4-4890-9d54-3c925332124d");
        public static readonly Guid ClosedNotEligibleForNIHRRSS = Guid.Parse("1fa7ae4c-8bb7-49da-9efc-f9aa96de4c1d");
        public static readonly Guid ClosedReferredToAnotherNIHRRSS = Guid.Parse("3d344c3c-9964-4079-877e-bc3b6864f52d");
        public static readonly Guid ClosedOutOfRemit = Guid.Parse("529aaebf-5e02-4003-825e-265a6271c816");
        public static readonly Guid ReferredToNIHRRSSExpertTeams = Guid.Parse("c22df21d-30ed-49d5-bee0-0f304b74a365");
        
        public static readonly SupportRequestStatus[] Values = new SupportRequestStatus[] {
            new SupportRequestStatus { Id = Guid.Parse("206b6f9d-feee-4b28-9795-1e1a2b7b887f"), Name = "To be triaged", Default = true },
            new SupportRequestStatus { Id = Guid.Parse("667ccd51-00f4-4890-9d54-3c925332124d"), Name = "On hold" },
            new SupportRequestStatus { Id = Guid.Parse("1fa7ae4c-8bb7-49da-9efc-f9aa96de4c1d"), Name = "Closed - Not eligible for NIHR RSS" },
            new SupportRequestStatus { Id = Guid.Parse("3d344c3c-9964-4079-877e-bc3b6864f52d"), Name = "Closed - Referred to another NIHR RSS" },
            new SupportRequestStatus { Id = Guid.Parse("529aaebf-5e02-4003-825e-265a6271c816"), Name = "Closed - Out of remit" },
            new SupportRequestStatus { Id = Guid.Parse("c22df21d-30ed-49d5-bee0-0f304b74a365"), Name = "Referred to NIHR RSS expert teams" },
        };
        
        public static SupportRequestStatus Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

