using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class SupportTeamEnum
    {
        public static readonly Guid TriageTeam = Guid.Parse("4b013d8c-7aad-4100-9586-83054ef15bac");
        public static readonly Guid CTUTeam = Guid.Parse("9d4b32b9-7e7e-42c5-8f08-82de256fbcf0");
        public static readonly Guid PPIETeam = Guid.Parse("662067e9-875c-47f7-8427-26ac98078c20");
        public static readonly Guid ExpertQuantitativeTeam = Guid.Parse("5283e9be-faf3-45f5-83b2-4c5288c090c3");
        public static readonly Guid ExpertQualitativeTeam = Guid.Parse("b1bdcae3-7df9-404a-aad7-a9265392b5aa");
        
        public static readonly SupportTeam[] Values = new SupportTeam[] {
            new SupportTeam { Id = Guid.Parse("4b013d8c-7aad-4100-9586-83054ef15bac"), Name = "Triage Team" },
            new SupportTeam { Id = Guid.Parse("9d4b32b9-7e7e-42c5-8f08-82de256fbcf0"), Name = "CTU Team" },
            new SupportTeam { Id = Guid.Parse("662067e9-875c-47f7-8427-26ac98078c20"), Name = "PPIE Team" },
            new SupportTeam { Id = Guid.Parse("5283e9be-faf3-45f5-83b2-4c5288c090c3"), Name = "Expert Quantitative Team" },
            new SupportTeam { Id = Guid.Parse("b1bdcae3-7df9-404a-aad7-a9265392b5aa"), Name = "Expert Qualitative Team" },
        };
        
        public static SupportTeam Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

