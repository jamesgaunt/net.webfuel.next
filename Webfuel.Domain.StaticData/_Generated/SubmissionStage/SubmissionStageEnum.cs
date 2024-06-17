using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class SubmissionStageEnum
    {
        public static readonly Guid OutlineStageStage1 = Guid.Parse("18174faf-8787-480e-b0b4-5698a5e59d93");
        public static readonly Guid Stage2 = Guid.Parse("deecb94c-8284-45c8-b499-1c0c099215b0");
        public static readonly Guid OneStageApplication = Guid.Parse("aa13a2ae-f1dd-4b8b-959e-15bdff000fee");
        public static readonly Guid AcceleratorAward = Guid.Parse("3e658c3b-11fc-464b-9296-7bf369866bce");
        public static readonly Guid NotApplicable = Guid.Parse("af170296-2c64-4a39-8fb8-6ddd6f8d3137");
        public static readonly Guid Unknown = Guid.Parse("174a713b-aff7-432b-8e7a-eede783a6067");
        
        public static readonly SubmissionStage[] Values = new SubmissionStage[] {
            new SubmissionStage { Id = Guid.Parse("18174faf-8787-480e-b0b4-5698a5e59d93"), Name = "Outline Stage (Stage 1)" },
            new SubmissionStage { Id = Guid.Parse("deecb94c-8284-45c8-b499-1c0c099215b0"), Name = "Stage 2" },
            new SubmissionStage { Id = Guid.Parse("aa13a2ae-f1dd-4b8b-959e-15bdff000fee"), Name = "One Stage Application" },
            new SubmissionStage { Id = Guid.Parse("3e658c3b-11fc-464b-9296-7bf369866bce"), Name = "Accelerator Award" },
            new SubmissionStage { Id = Guid.Parse("af170296-2c64-4a39-8fb8-6ddd6f8d3137"), Name = "Not applicable" },
            new SubmissionStage { Id = Guid.Parse("174a713b-aff7-432b-8e7a-eede783a6067"), Name = "Unknown" },
        };
        
        public static SubmissionStage Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

