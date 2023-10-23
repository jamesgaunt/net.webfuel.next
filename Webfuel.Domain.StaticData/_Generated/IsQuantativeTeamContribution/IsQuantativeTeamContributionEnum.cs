using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class IsQuantativeTeamContributionEnum
    {
        public static readonly Guid Yes = Guid.Parse("8192a0f7-a0c9-4e57-a36a-42383a3832d9");
        public static readonly Guid No = Guid.Parse("30923282-7f55-48b2-bc34-2a7c822401d9");
        
        public static readonly IsQuantativeTeamContribution[] Values = new IsQuantativeTeamContribution[] {
            new IsQuantativeTeamContribution { Id = Guid.Parse("8192a0f7-a0c9-4e57-a36a-42383a3832d9"), Name = "Yes" },
            new IsQuantativeTeamContribution { Id = Guid.Parse("30923282-7f55-48b2-bc34-2a7c822401d9"), Name = "No" },
        };
        
        public static IsQuantativeTeamContribution Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

