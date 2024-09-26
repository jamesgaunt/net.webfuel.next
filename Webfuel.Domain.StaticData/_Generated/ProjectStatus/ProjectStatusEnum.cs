using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class ProjectStatusEnum
    {
        public static readonly Guid Active = Guid.Parse("6c83e9e4-617b-4386-b087-16a11f6b24af");
        public static readonly Guid OnHold = Guid.Parse("691ee44b-7b01-4af0-b8f3-13f96f00f0ce");
        public static readonly Guid SubmittedOnHold = Guid.Parse("e66a0fdb-8d2e-4f4a-b813-a0a487e8c25d");
        public static readonly Guid Closed = Guid.Parse("ed4845b0-1f4c-4df3-b4ec-46e5ce94c275");
        public static readonly Guid Discarded = Guid.Parse("164fdeee-8d6f-42fa-a23b-fbab0ef3ba93");
        
        public static readonly ProjectStatus[] Values = new ProjectStatus[] {
            new ProjectStatus { Id = Guid.Parse("6c83e9e4-617b-4386-b087-16a11f6b24af"), Name = "Active", Active = true, Default = true },
            new ProjectStatus { Id = Guid.Parse("691ee44b-7b01-4af0-b8f3-13f96f00f0ce"), Name = "On Hold", Active = true },
            new ProjectStatus { Id = Guid.Parse("e66a0fdb-8d2e-4f4a-b813-a0a487e8c25d"), Name = "Submitted - On Hold", Active = true },
            new ProjectStatus { Id = Guid.Parse("ed4845b0-1f4c-4df3-b4ec-46e5ce94c275"), Name = "Closed", Locked = true },
            new ProjectStatus { Id = Guid.Parse("164fdeee-8d6f-42fa-a23b-fbab0ef3ba93"), Name = "Discarded", Locked = true, Discarded = true, Hidden = true },
        };
        
        public static ProjectStatus Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

