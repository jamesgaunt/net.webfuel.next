using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class ReportProviderEnum
    {
        public static readonly Guid Project = Guid.Parse("b8b6dea3-d6de-4480-a944-e7b0c2827888");
        public static readonly Guid ProjectSupport = Guid.Parse("2af96eb5-e52d-4163-9247-c34e7b170f62");
        public static readonly Guid User = Guid.Parse("96124eea-4434-4669-b377-580bbf85a96d");
        
        public static readonly ReportProvider[] Values = new ReportProvider[] {
            new ReportProvider { Id = Guid.Parse("b8b6dea3-d6de-4480-a944-e7b0c2827888"), Name = "Project" },
            new ReportProvider { Id = Guid.Parse("2af96eb5-e52d-4163-9247-c34e7b170f62"), Name = "Project Support" },
            new ReportProvider { Id = Guid.Parse("96124eea-4434-4669-b377-580bbf85a96d"), Name = "User" },
        };
        
        public static ReportProvider Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

