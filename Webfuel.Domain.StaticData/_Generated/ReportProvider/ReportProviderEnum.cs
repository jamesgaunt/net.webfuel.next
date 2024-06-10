using Microsoft.Identity.Client;
using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class ReportProviderEnum
    {
        public static readonly Guid Project = Guid.Parse("b8b6dea3-d6de-4480-a944-e7b0c2827888");
        public static readonly Guid ProjectSupport = Guid.Parse("2af96eb5-e52d-4163-9247-c34e7b170f62");
        public static readonly Guid ProjectSubmission = Guid.Parse("d47d2458-99b5-4c9a-b1e2-8694f86d28ca");
        public static readonly Guid SupportRequest = Guid.Parse("bb41b6b6-ef1a-4982-95d0-9f41f80c91cb");
        public static readonly Guid User = Guid.Parse("96124eea-4434-4669-b377-580bbf85a96d");
        public static readonly Guid UserActivity = Guid.Parse("3fd56f37-05f8-4418-bc57-d79a393e0ee9");
        
        public static readonly ReportProvider[] Values = new ReportProvider[] {
            new ReportProvider { Id = Guid.Parse("b8b6dea3-d6de-4480-a944-e7b0c2827888"), Name = "Project" },
            new ReportProvider { Id = Guid.Parse("2af96eb5-e52d-4163-9247-c34e7b170f62"), Name = "Project Support" },
            new ReportProvider { Id = Guid.Parse("d47d2458-99b5-4c9a-b1e2-8694f86d28ca"), Name = "Project Submission" },
            new ReportProvider { Id = Guid.Parse("bb41b6b6-ef1a-4982-95d0-9f41f80c91cb"), Name = "Support Request" },
            new ReportProvider { Id = Guid.Parse("96124eea-4434-4669-b377-580bbf85a96d"), Name = "User" },
            new ReportProvider { Id = Guid.Parse("3fd56f37-05f8-4418-bc57-d79a393e0ee9"), Name = "User Activity" },
        };
        
        public static ReportProvider Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

