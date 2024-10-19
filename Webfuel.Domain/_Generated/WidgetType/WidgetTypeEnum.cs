using System;
using System.Linq;
namespace Webfuel.Domain
{
    [ApiEnum]
    public static class WidgetTypeEnum
    {
        public static readonly Guid ProjectSummary = Guid.Parse("4f5bed07-af06-40c5-a7ed-ad283e57e503");
        public static readonly Guid TeamSupport = Guid.Parse("54d1db4b-2b37-4d20-9e55-ffe2529446ac");
        public static readonly Guid TriageSummary = Guid.Parse("3f9d5f90-87bd-4257-828e-95fa04e55d7a");
        public static readonly Guid TeamActivity = Guid.Parse("cf48d41d-934b-4fc8-b368-52ee87d5e76d");
        public static readonly Guid LeadAdviserProjects = Guid.Parse("866208a8-c266-4e56-ad3e-717491cf0089");
        public static readonly Guid SupportAdviserProjects = Guid.Parse("391677a2-119d-4c18-982b-27769abe42bd");
        
        public static readonly WidgetType[] Values = new WidgetType[] {
            new WidgetType { Id = Guid.Parse("4f5bed07-af06-40c5-a7ed-ad283e57e503"), Name = "Project Summary", Description = "Number of projects by status" },
            new WidgetType { Id = Guid.Parse("54d1db4b-2b37-4d20-9e55-ffe2529446ac"), Name = "Team Support", Description = "Number of projects with open support requests for each support team" },
            new WidgetType { Id = Guid.Parse("3f9d5f90-87bd-4257-828e-95fa04e55d7a"), Name = "Triage Summary", Description = "Number of triage requests by status" },
            new WidgetType { Id = Guid.Parse("cf48d41d-934b-4fc8-b368-52ee87d5e76d"), Name = "Team Activity", Description = "Support and user activity hours across each member in a support team" },
            new WidgetType { Id = Guid.Parse("866208a8-c266-4e56-ad3e-717491cf0089"), Name = "Lead Adviser Projects", Description = "Projects on which you are the lead adviser" },
            new WidgetType { Id = Guid.Parse("391677a2-119d-4c18-982b-27769abe42bd"), Name = "Support Adviser Projects", Description = "Projects on which you are a support adviser" },
        };
        
        public static WidgetType Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

