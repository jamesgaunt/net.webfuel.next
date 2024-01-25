using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class ResearcherRoleEnum
    {
        public static readonly Guid ChiefInvestigator = Guid.Parse("4c6fbfca-3601-4c24-81aa-d372e0d0320e");
        public static readonly Guid ResearchFellowAssociate = Guid.Parse("1c4b0501-821c-4a62-a560-f50b42d55657");
        public static readonly Guid FellowshipApplicant = Guid.Parse("d2791155-7b9e-416f-a169-9ecc7b1aa0cc");
        public static readonly Guid CoInvestigator = Guid.Parse("93a1de9c-ccbe-4bc4-8896-e34e97d217af");
        public static readonly Guid Administator = Guid.Parse("c26a348a-1581-4678-8d69-aa218d11bb36");
        public static readonly Guid Other = Guid.Parse("45013058-6dc8-4a3c-90e4-49a4da46efc8");
        
        public static readonly ResearcherRole[] Values = new ResearcherRole[] {
            new ResearcherRole { Id = Guid.Parse("4c6fbfca-3601-4c24-81aa-d372e0d0320e"), Name = "Chief Investigator" },
            new ResearcherRole { Id = Guid.Parse("1c4b0501-821c-4a62-a560-f50b42d55657"), Name = "Research Fellow/Associate" },
            new ResearcherRole { Id = Guid.Parse("d2791155-7b9e-416f-a169-9ecc7b1aa0cc"), Name = "Fellowship Applicant " },
            new ResearcherRole { Id = Guid.Parse("93a1de9c-ccbe-4bc4-8896-e34e97d217af"), Name = "Co-investigator" },
            new ResearcherRole { Id = Guid.Parse("c26a348a-1581-4678-8d69-aa218d11bb36"), Name = "Administator " },
            new ResearcherRole { Id = Guid.Parse("45013058-6dc8-4a3c-90e4-49a4da46efc8"), Name = "Other", FreeText = true },
        };
        
        public static ResearcherRole Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

