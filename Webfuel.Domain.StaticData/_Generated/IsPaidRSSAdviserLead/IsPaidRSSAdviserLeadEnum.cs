using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class IsPaidRSSAdviserLeadEnum
    {
        public static readonly Guid No = Guid.Parse("b2a969e5-6dff-4ef4-a629-a3cbd5105275");
        public static readonly Guid Yes = Guid.Parse("05213f9c-e901-4e63-82f8-4b558415f9ad");
        
        public static readonly IsPaidRSSAdviserLead[] Values = new IsPaidRSSAdviserLead[] {
            new IsPaidRSSAdviserLead { Id = Guid.Parse("b2a969e5-6dff-4ef4-a629-a3cbd5105275"), Name = "No" },
            new IsPaidRSSAdviserLead { Id = Guid.Parse("05213f9c-e901-4e63-82f8-4b558415f9ad"), Name = "Yes" },
        };
        
        public static IsPaidRSSAdviserLead Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

