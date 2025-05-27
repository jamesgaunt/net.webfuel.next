using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class FullSubmissionStatusEnum
    {
        public static readonly Guid Yes = Guid.Parse("a40cad90-8975-4165-870c-52a4c71b9869");
        public static readonly Guid No = Guid.Parse("1f869a5b-0f34-4e88-8a2a-22d18e0c16f4");
        public static readonly Guid DonTKnow = Guid.Parse("081286d1-6817-4a77-9506-3308bd04eab1");
        public static readonly Guid NA = Guid.Parse("64407686-d81e-482f-8188-5bed6ac7b788");
        
        public static readonly FullSubmissionStatus[] Values = new FullSubmissionStatus[] {
            new FullSubmissionStatus { Id = Guid.Parse("a40cad90-8975-4165-870c-52a4c71b9869"), Name = "Yes" },
            new FullSubmissionStatus { Id = Guid.Parse("1f869a5b-0f34-4e88-8a2a-22d18e0c16f4"), Name = "No" },
            new FullSubmissionStatus { Id = Guid.Parse("081286d1-6817-4a77-9506-3308bd04eab1"), Name = "Don't know" },
            new FullSubmissionStatus { Id = Guid.Parse("64407686-d81e-482f-8188-5bed6ac7b788"), Name = "N/A" },
        };
        
        public static FullSubmissionStatus Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

