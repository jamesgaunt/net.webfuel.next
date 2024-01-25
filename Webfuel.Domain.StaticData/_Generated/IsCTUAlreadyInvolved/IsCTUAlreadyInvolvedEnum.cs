using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class IsCTUAlreadyInvolvedEnum
    {
        public static readonly Guid YesWeHaveAlreadyGotACTUInvolved = Guid.Parse("d51d2c40-578a-4797-b728-943b6dd9e87c");
        public static readonly Guid NoWeHaveNotGotACTUInvolved = Guid.Parse("78ccf6a2-e166-4774-9c0c-aedcb57fd789");
        
        public static readonly IsCTUAlreadyInvolved[] Values = new IsCTUAlreadyInvolved[] {
            new IsCTUAlreadyInvolved { Id = Guid.Parse("d51d2c40-578a-4797-b728-943b6dd9e87c"), Name = "Yes - we have already got a CTU involved", FreeText = true },
            new IsCTUAlreadyInvolved { Id = Guid.Parse("78ccf6a2-e166-4774-9c0c-aedcb57fd789"), Name = "No - we have not got a CTU involved" },
        };
        
        public static IsCTUAlreadyInvolved Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

