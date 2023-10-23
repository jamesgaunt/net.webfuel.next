using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class IsLeadApplicantNHSEnum
    {
        public static readonly Guid YesTheyAre = Guid.Parse("616ee238-466d-4f5e-9eab-5b3b48b95bc9");
        public static readonly Guid NoTheyAreNot = Guid.Parse("f9aa774f-616e-481a-9137-e5d55a9c99bd");
        
        public static readonly IsLeadApplicantNHS[] Values = new IsLeadApplicantNHS[] {
            new IsLeadApplicantNHS { Id = Guid.Parse("616ee238-466d-4f5e-9eab-5b3b48b95bc9"), Name = "Yes - they are" },
            new IsLeadApplicantNHS { Id = Guid.Parse("f9aa774f-616e-481a-9137-e5d55a9c99bd"), Name = "No - they are not" },
        };
        
        public static IsLeadApplicantNHS Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

