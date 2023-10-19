using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class SuportRequestStatusEnum
    {
        public static readonly Guid Tobetriaged = Guid.Parse("206b6f9d-feee-4b28-9795-1e1a2b7b887f");
        public static readonly Guid ClosedasnoteligiblefromNIHRRSS = Guid.Parse("1fa7ae4c-8bb7-49da-9efc-f9aa96de4c1d");
        public static readonly Guid ClosedasreferredtoanotherNIHRRSS = Guid.Parse("3d344c3c-9964-4079-877e-bc3b6864f52d");
        public static readonly Guid ReferredtoNIHRRSSexpertteams = Guid.Parse("c22df21d-30ed-49d5-bee0-0f304b74a365");
        
        public static readonly SuportRequestStatus[] Values = new SuportRequestStatus[] {
            new SuportRequestStatus { Id = Guid.Parse("206b6f9d-feee-4b28-9795-1e1a2b7b887f"), Name = "To be triaged" },
            new SuportRequestStatus { Id = Guid.Parse("1fa7ae4c-8bb7-49da-9efc-f9aa96de4c1d"), Name = "Closed as not eligible from NIHR RSS" },
            new SuportRequestStatus { Id = Guid.Parse("3d344c3c-9964-4079-877e-bc3b6864f52d"), Name = "Closed as referred to another NIHR RSS" },
            new SuportRequestStatus { Id = Guid.Parse("c22df21d-30ed-49d5-bee0-0f304b74a365"), Name = "Referred to NIHR RSS expert teams" },
        };
        
        public static SuportRequestStatus Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

