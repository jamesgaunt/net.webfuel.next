using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class IsFellowshipEnum
    {
        public static readonly Guid YesThisApplicationIsForAFellowship = Guid.Parse("d8f6423c-00da-49df-94aa-24bad595858d");
        public static readonly Guid NoThisApplicationIsNotForAFellowship = Guid.Parse("65f431cb-e66a-4f36-b457-f783266827da");
        
        public static readonly IsFellowship[] Values = new IsFellowship[] {
            new IsFellowship { Id = Guid.Parse("d8f6423c-00da-49df-94aa-24bad595858d"), Name = "Yes - this application is for a fellowship" },
            new IsFellowship { Id = Guid.Parse("65f431cb-e66a-4f36-b457-f783266827da"), Name = "No - this application is not for a fellowship" },
        };
        
        public static IsFellowship Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

