using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class IsYesNoEnum
    {
        public static readonly Guid Yes = Guid.Parse("3fd6c70b-b87a-45bc-84bc-c71802ef3276");
        public static readonly Guid No = Guid.Parse("9c2dbdff-9020-419a-aa3f-bb976b19de6a");
        
        public static readonly IsYesNo[] Values = new IsYesNo[] {
            new IsYesNo { Id = Guid.Parse("3fd6c70b-b87a-45bc-84bc-c71802ef3276"), Name = "Yes" },
            new IsYesNo { Id = Guid.Parse("9c2dbdff-9020-419a-aa3f-bb976b19de6a"), Name = "No" },
        };
        
        public static IsYesNo Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

