using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class IsPrePostAwardEnum
    {
        public static readonly Guid PreAward = Guid.Parse("d8c2fe26-3a35-4a49-b61d-b5abc41611f6");
        public static readonly Guid PostAward = Guid.Parse("6b7d87be-4ba2-4b47-9de5-42672114fcaf");
        
        public static readonly IsPrePostAward[] Values = new IsPrePostAward[] {
            new IsPrePostAward { Id = Guid.Parse("d8c2fe26-3a35-4a49-b61d-b5abc41611f6"), Name = "Pre-Award" },
            new IsPrePostAward { Id = Guid.Parse("6b7d87be-4ba2-4b47-9de5-42672114fcaf"), Name = "Post-Award" },
        };
        
        public static IsPrePostAward Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

