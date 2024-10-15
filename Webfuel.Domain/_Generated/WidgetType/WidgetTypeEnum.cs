using System;
using System.Linq;
namespace Webfuel.Domain
{
    [ApiEnum]
    public static class WidgetTypeEnum
    {
        public static readonly Guid ProjectSummary = Guid.Parse("4f5bed07-af06-40c5-a7ed-ad283e57e503");
        public static readonly Guid TeamSupport = Guid.Parse("54d1db4b-2b37-4d20-9e55-ffe2529446ac");
        public static readonly Guid TeamActivity = Guid.Parse("cf48d41d-934b-4fc8-b368-52ee87d5e76d");
        
        public static readonly WidgetType[] Values = new WidgetType[] {
            new WidgetType { Id = Guid.Parse("4f5bed07-af06-40c5-a7ed-ad283e57e503"), Name = "Project Summary" },
            new WidgetType { Id = Guid.Parse("54d1db4b-2b37-4d20-9e55-ffe2529446ac"), Name = "Team Support" },
            new WidgetType { Id = Guid.Parse("cf48d41d-934b-4fc8-b368-52ee87d5e76d"), Name = "Team Activity" },
        };
        
        public static WidgetType Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

