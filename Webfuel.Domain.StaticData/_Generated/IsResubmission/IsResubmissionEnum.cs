using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class IsResubmissionEnum
    {
        public static readonly Guid YesThisApplicationIsAResubmission = Guid.Parse("0cdc79cc-6053-4163-9661-2aff37dacf30");
        public static readonly Guid NoThisApplicationIsNotAResubmission = Guid.Parse("ab294a68-3b87-4809-8923-2b06fa9e2958");
        
        public static readonly IsResubmission[] Values = new IsResubmission[] {
            new IsResubmission { Id = Guid.Parse("0cdc79cc-6053-4163-9661-2aff37dacf30"), Name = "Yes - this application is a resubmission" },
            new IsResubmission { Id = Guid.Parse("ab294a68-3b87-4809-8923-2b06fa9e2958"), Name = "No - this application is not a resubmission" },
        };
        
        public static IsResubmission Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

