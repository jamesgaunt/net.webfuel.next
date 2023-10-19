using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class ProjectStatusEnum
    {
        public static readonly Guid Active = Guid.Parse("6c83e9e4-617b-4386-b087-16a11f6b24af");
        public static readonly Guid Inactive = Guid.Parse("15787677-fc83-4071-961a-1da5b5f41018");
        public static readonly Guid Closed = Guid.Parse("ed4845b0-1f4c-4df3-b4ec-46e5ce94c275");
        public static readonly Guid Discarded = Guid.Parse("164fdeee-8d6f-42fa-a23b-fbab0ef3ba93");
        
        public static readonly ProjectStatus[] Values = new ProjectStatus[] {
            new ProjectStatus { Id = Guid.Parse("6c83e9e4-617b-4386-b087-16a11f6b24af"), Name = "Active" },
            new ProjectStatus { Id = Guid.Parse("15787677-fc83-4071-961a-1da5b5f41018"), Name = "Inactive" },
            new ProjectStatus { Id = Guid.Parse("ed4845b0-1f4c-4df3-b4ec-46e5ce94c275"), Name = "Closed" },
            new ProjectStatus { Id = Guid.Parse("164fdeee-8d6f-42fa-a23b-fbab0ef3ba93"), Name = "Discarded" },
        };
        
        public static ProjectStatus Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

