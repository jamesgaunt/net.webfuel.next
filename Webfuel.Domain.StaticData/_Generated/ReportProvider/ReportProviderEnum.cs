using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class ReportProviderEnum
    {
        public static readonly Guid Project = Guid.Parse("b8b6dea3-d6de-4480-a944-e7b0c2827888");
        public static readonly Guid ProjectSupport = Guid.Parse("2af96eb5-e52d-4163-9247-c34e7b170f62");
        
        public static readonly ReportProvider[] Values = new ReportProvider[] {
            new ReportProvider { Id = Guid.Parse("b8b6dea3-d6de-4480-a944-e7b0c2827888"), Name = "Project" },
            new ReportProvider { Id = Guid.Parse("2af96eb5-e52d-4163-9247-c34e7b170f62"), Name = "Project Support" },
        };
        
        public static ReportProvider Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

