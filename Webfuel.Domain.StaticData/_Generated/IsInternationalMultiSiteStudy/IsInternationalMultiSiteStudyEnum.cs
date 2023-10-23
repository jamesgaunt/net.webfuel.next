using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class IsInternationalMultiSiteStudyEnum
    {
        public static readonly Guid Yes = Guid.Parse("744bb5e0-399e-4c3c-a92f-25ffcc4dbd7d");
        public static readonly Guid No = Guid.Parse("853f0502-651a-44d6-99c2-1ee731483c88");
        
        public static readonly IsInternationalMultiSiteStudy[] Values = new IsInternationalMultiSiteStudy[] {
            new IsInternationalMultiSiteStudy { Id = Guid.Parse("744bb5e0-399e-4c3c-a92f-25ffcc4dbd7d"), Name = "Yes" },
            new IsInternationalMultiSiteStudy { Id = Guid.Parse("853f0502-651a-44d6-99c2-1ee731483c88"), Name = "No" },
        };
        
        public static IsInternationalMultiSiteStudy Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

