using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class IsCTUTeamContributionEnum
    {
        public static readonly Guid Yes = Guid.Parse("3e1f2673-e708-44a8-8768-cbf9fe1afb7c");
        public static readonly Guid No = Guid.Parse("5b18a6b2-1169-4f7c-91b7-e738ea4527c6");
        
        public static readonly IsCTUTeamContribution[] Values = new IsCTUTeamContribution[] {
            new IsCTUTeamContribution { Id = Guid.Parse("3e1f2673-e708-44a8-8768-cbf9fe1afb7c"), Name = "Yes" },
            new IsCTUTeamContribution { Id = Guid.Parse("5b18a6b2-1169-4f7c-91b7-e738ea4527c6"), Name = "No" },
        };
        
        public static IsCTUTeamContribution Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

