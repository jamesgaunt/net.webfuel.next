using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class ResearcherProfessionalBackgroundEnum
    {
        public static readonly Guid ClinicalAcademic = Guid.Parse("75b769e7-9e9d-42df-b31d-c906d353d4d0");
        public static readonly Guid Academic = Guid.Parse("843db2be-bd3a-4f61-ac48-e1932e55f076");
        public static readonly Guid NonAcademicClinician = Guid.Parse("c3dcef26-d476-41bf-9a9c-89b008addf58");
        public static readonly Guid NurseMidwifeAHP = Guid.Parse("d16a4d18-5309-4fb4-b6f7-7f9bcf56c6a6");
        public static readonly Guid NHSManager = Guid.Parse("e4a7b2a1-1f65-4201-9608-ed282955263c");
        public static readonly Guid SocialCarePractitioner = Guid.Parse("89ffaa61-4d5c-4095-bb5c-36d8eb8248e1");
        public static readonly Guid PublicHealthPractitioner = Guid.Parse("5f329e4a-1881-41b1-886f-9706eea45e20");
        public static readonly Guid SME = Guid.Parse("f10651e2-aedb-41cd-9ee4-750b3d7af17c");
        public static readonly Guid OtherNHS = Guid.Parse("3cfcf7e5-4230-4083-a83a-5723c49beb82");
        public static readonly Guid LivedExperiencePublicCommunityContributor = Guid.Parse("26dbb5f4-d544-48cc-9084-79a7027d13df");
        public static readonly Guid LocalAuthorityOfficerPolicymaker = Guid.Parse("887e71fa-3ba7-49d4-adb1-827ffe0de611");
        public static readonly Guid Other = Guid.Parse("0943bb8e-42b8-4f14-870c-6a6a2bce1800");
        public static readonly Guid DonTKnow = Guid.Parse("d9d6683b-f3e6-439a-86e5-937cb21e3d99");
        
        public static readonly ResearcherProfessionalBackground[] Values = new ResearcherProfessionalBackground[] {
            new ResearcherProfessionalBackground { Id = Guid.Parse("75b769e7-9e9d-42df-b31d-c906d353d4d0"), Name = "Clinical academic" },
            new ResearcherProfessionalBackground { Id = Guid.Parse("843db2be-bd3a-4f61-ac48-e1932e55f076"), Name = "Academic" },
            new ResearcherProfessionalBackground { Id = Guid.Parse("c3dcef26-d476-41bf-9a9c-89b008addf58"), Name = "Non-academic clinician" },
            new ResearcherProfessionalBackground { Id = Guid.Parse("d16a4d18-5309-4fb4-b6f7-7f9bcf56c6a6"), Name = "Nurse/Midwife/AHP" },
            new ResearcherProfessionalBackground { Id = Guid.Parse("e4a7b2a1-1f65-4201-9608-ed282955263c"), Name = "NHS manager" },
            new ResearcherProfessionalBackground { Id = Guid.Parse("89ffaa61-4d5c-4095-bb5c-36d8eb8248e1"), Name = "Social care practitioner" },
            new ResearcherProfessionalBackground { Id = Guid.Parse("5f329e4a-1881-41b1-886f-9706eea45e20"), Name = "Public health practitioner" },
            new ResearcherProfessionalBackground { Id = Guid.Parse("f10651e2-aedb-41cd-9ee4-750b3d7af17c"), Name = "SME" },
            new ResearcherProfessionalBackground { Id = Guid.Parse("3cfcf7e5-4230-4083-a83a-5723c49beb82"), Name = "Other NHS" },
            new ResearcherProfessionalBackground { Id = Guid.Parse("26dbb5f4-d544-48cc-9084-79a7027d13df"), Name = "Lived experience/public/community contributor" },
            new ResearcherProfessionalBackground { Id = Guid.Parse("887e71fa-3ba7-49d4-adb1-827ffe0de611"), Name = "Local authority officer/policymaker" },
            new ResearcherProfessionalBackground { Id = Guid.Parse("0943bb8e-42b8-4f14-870c-6a6a2bce1800"), Name = "Other", FreeText = true },
            new ResearcherProfessionalBackground { Id = Guid.Parse("d9d6683b-f3e6-439a-86e5-937cb21e3d99"), Name = "Don't know" },
        };
        
        public static ResearcherProfessionalBackground Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

