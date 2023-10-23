using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class IsTeamMembersConsultedEnum
    {
        public static readonly Guid YesTeamMembersHaveBeenConsulted = Guid.Parse("01340892-ce30-4c07-8ff3-90f5b9898b8c");
        public static readonly Guid NoTeamMembersHaveNotBeenConsulted = Guid.Parse("b5f5f413-e742-494d-a22a-4ecfa1d52ce8");
        
        public static readonly IsTeamMembersConsulted[] Values = new IsTeamMembersConsulted[] {
            new IsTeamMembersConsulted { Id = Guid.Parse("01340892-ce30-4c07-8ff3-90f5b9898b8c"), Name = "Yes - team members have been consulted" },
            new IsTeamMembersConsulted { Id = Guid.Parse("b5f5f413-e742-494d-a22a-4ecfa1d52ce8"), Name = "No - team members have not been consulted" },
        };
        
        public static IsTeamMembersConsulted Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

