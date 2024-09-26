using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class ProfessionalBackgroundEnum
    {
        public static readonly Guid AlliedHealthcareProfessional = Guid.Parse("9cd4cbde-011e-4b8b-898f-4a23a80d8e1f");
        public static readonly Guid Doctor = Guid.Parse("e031dcc1-a8c7-4a26-9a89-f5246da76596");
        public static readonly Guid Dentist = Guid.Parse("22dc26e7-8685-401e-b8bd-d1d957e27f0d");
        public static readonly Guid Midwife = Guid.Parse("eeed9783-71e4-45b9-82da-0161838d5080");
        public static readonly Guid Psychologist = Guid.Parse("0ee15892-343e-4117-9362-0abd68761bce");
        public static readonly Guid Nurse = Guid.Parse("c2161d6e-c39c-4ee0-9d9d-45126f3ac03a");
        public static readonly Guid PublicHealthSpecialist = Guid.Parse("897dcab9-fda6-403e-aa8d-c3dde2043d57");
        public static readonly Guid SocialCareSpecialist = Guid.Parse("91706bb9-c8ff-4f03-9eb6-b70d6510513c");
        public static readonly Guid OtherHealthcareProfessional = Guid.Parse("3ae2a25d-8993-4bdc-acec-8e345ecec728");
        public static readonly Guid NotHealthcareProfessional = Guid.Parse("db90675f-5225-43bc-87c0-53ba9267c19e");
        
        public static readonly ProfessionalBackground[] Values = new ProfessionalBackground[] {
            new ProfessionalBackground { Id = Guid.Parse("9cd4cbde-011e-4b8b-898f-4a23a80d8e1f"), Name = "Allied Healthcare Professional" },
            new ProfessionalBackground { Id = Guid.Parse("e031dcc1-a8c7-4a26-9a89-f5246da76596"), Name = "Doctor" },
            new ProfessionalBackground { Id = Guid.Parse("22dc26e7-8685-401e-b8bd-d1d957e27f0d"), Name = "Dentist" },
            new ProfessionalBackground { Id = Guid.Parse("eeed9783-71e4-45b9-82da-0161838d5080"), Name = "Midwife" },
            new ProfessionalBackground { Id = Guid.Parse("0ee15892-343e-4117-9362-0abd68761bce"), Name = "Psychologist" },
            new ProfessionalBackground { Id = Guid.Parse("c2161d6e-c39c-4ee0-9d9d-45126f3ac03a"), Name = "Nurse" },
            new ProfessionalBackground { Id = Guid.Parse("897dcab9-fda6-403e-aa8d-c3dde2043d57"), Name = "Public Health Specialist" },
            new ProfessionalBackground { Id = Guid.Parse("91706bb9-c8ff-4f03-9eb6-b70d6510513c"), Name = "Social Care Specialist" },
            new ProfessionalBackground { Id = Guid.Parse("3ae2a25d-8993-4bdc-acec-8e345ecec728"), Name = "Other Healthcare Professional" },
            new ProfessionalBackground { Id = Guid.Parse("db90675f-5225-43bc-87c0-53ba9267c19e"), Name = "Not Healthcare Professional" },
        };
        
        public static ProfessionalBackground Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

