using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class RSSHubEnum
    {
        public static readonly Guid NewcastleUniversityAndPartners = Guid.Parse("02f2c7d3-691f-426d-b444-eb802c562715");
        public static readonly Guid UniversityOfBirminghamAndPartners = Guid.Parse("17aa2cae-1616-4f26-94ff-b8a4617680f0");
        public static readonly Guid UniversityOfLancasterAndPartners = Guid.Parse("d44377e3-2b75-4769-9b08-f24977aed598");
        public static readonly Guid UniversityOfYorkAndPartners = Guid.Parse("003b6f75-efbe-443b-86d3-186f2a3cc584");
        public static readonly Guid ImperialCollegeLondonAndPartners = Guid.Parse("9e4f4c12-022d-4769-abdf-71b40f2936bd");
        public static readonly Guid UniversityOfSouthamptonAndPartners = Guid.Parse("d5678978-ea52-4923-bed9-e4c2fcada10d");
        public static readonly Guid KingsCollegeLondonAndPartners = Guid.Parse("7a2a0314-6d06-4b32-a1eb-3dd96ab0ba6f");
        public static readonly Guid UniversityOfLeicesterAndPartners = Guid.Parse("01cc53bc-088d-4ab6-a0fe-09fcbc164cd0");
        public static readonly Guid SouthamptonAndPartnersPublicHealthSpecialistCentre = Guid.Parse("24f9f0e6-3d95-4c94-899d-f078903a9daa");
        public static readonly Guid NewcastleAndPartnersPublicHealthSpecialistCentre = Guid.Parse("979dc9c7-e454-49ad-974f-f424d5470a80");
        public static readonly Guid LancasterUniversityAndPartnersSocialCareSpecialistCentre = Guid.Parse("282014e1-ba60-4d92-af70-971ab918c04a");
        
        public static readonly RSSHub[] Values = new RSSHub[] {
            new RSSHub { Id = Guid.Parse("02f2c7d3-691f-426d-b444-eb802c562715"), Name = "Newcastle University and Partners" },
            new RSSHub { Id = Guid.Parse("17aa2cae-1616-4f26-94ff-b8a4617680f0"), Name = "University of Birmingham and Partners" },
            new RSSHub { Id = Guid.Parse("d44377e3-2b75-4769-9b08-f24977aed598"), Name = "University of Lancaster and Partners" },
            new RSSHub { Id = Guid.Parse("003b6f75-efbe-443b-86d3-186f2a3cc584"), Name = "University of York and Partners" },
            new RSSHub { Id = Guid.Parse("9e4f4c12-022d-4769-abdf-71b40f2936bd"), Name = "Imperial College London and Partners", Default = true },
            new RSSHub { Id = Guid.Parse("d5678978-ea52-4923-bed9-e4c2fcada10d"), Name = "University of Southampton and Partners" },
            new RSSHub { Id = Guid.Parse("7a2a0314-6d06-4b32-a1eb-3dd96ab0ba6f"), Name = "Kings College London and Partners" },
            new RSSHub { Id = Guid.Parse("01cc53bc-088d-4ab6-a0fe-09fcbc164cd0"), Name = "University of Leicester and Partners" },
            new RSSHub { Id = Guid.Parse("24f9f0e6-3d95-4c94-899d-f078903a9daa"), Name = "Southampton and Partners Public Health Specialist Centre" },
            new RSSHub { Id = Guid.Parse("979dc9c7-e454-49ad-974f-f424d5470a80"), Name = "Newcastle and Partners Public Health Specialist Centre" },
            new RSSHub { Id = Guid.Parse("282014e1-ba60-4d92-af70-971ab918c04a"), Name = "Lancaster University and Partners Social Care Specialist Centre" },
        };
        
        public static RSSHub Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

