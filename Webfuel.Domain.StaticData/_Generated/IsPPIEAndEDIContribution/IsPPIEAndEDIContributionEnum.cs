using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class IsPPIEAndEDIContributionEnum
    {
        public static readonly Guid Yes = Guid.Parse("547241ba-72d4-4901-bc1e-5fffc9aaadfa");
        public static readonly Guid No = Guid.Parse("c2c2e7b5-f64d-421b-b4c7-de34cebbef53");
        
        public static readonly IsPPIEAndEDIContribution[] Values = new IsPPIEAndEDIContribution[] {
            new IsPPIEAndEDIContribution { Id = Guid.Parse("547241ba-72d4-4901-bc1e-5fffc9aaadfa"), Name = "Yes" },
            new IsPPIEAndEDIContribution { Id = Guid.Parse("c2c2e7b5-f64d-421b-b4c7-de34cebbef53"), Name = "No" },
        };
        
        public static IsPPIEAndEDIContribution Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

