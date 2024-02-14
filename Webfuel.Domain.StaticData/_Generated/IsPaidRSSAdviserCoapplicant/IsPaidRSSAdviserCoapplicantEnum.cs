using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class IsPaidRSSAdviserCoapplicantEnum
    {
        public static readonly Guid No = Guid.Parse("4f8c3cb5-d96a-4cfe-b04c-35dc8b925b15");
        public static readonly Guid Yes = Guid.Parse("22f78a71-0e8c-4047-82b3-305736b2d3cc");
        
        public static readonly IsPaidRSSAdviserCoapplicant[] Values = new IsPaidRSSAdviserCoapplicant[] {
            new IsPaidRSSAdviserCoapplicant { Id = Guid.Parse("4f8c3cb5-d96a-4cfe-b04c-35dc8b925b15"), Name = "No" },
            new IsPaidRSSAdviserCoapplicant { Id = Guid.Parse("22f78a71-0e8c-4047-82b3-305736b2d3cc"), Name = "Yes" },
        };
        
        public static IsPaidRSSAdviserCoapplicant Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

