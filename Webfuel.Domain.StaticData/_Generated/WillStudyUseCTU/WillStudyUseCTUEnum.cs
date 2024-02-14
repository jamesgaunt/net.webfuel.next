using System;
using System.Linq;
namespace Webfuel.Domain.StaticData
{
    [ApiEnum]
    public static class WillStudyUseCTUEnum
    {
        public static readonly Guid No = Guid.Parse("8ba8e39c-8792-4901-9a74-2788e972ac60");
        public static readonly Guid YesExternalToThisRSS = Guid.Parse("4672a5ec-1c83-4a66-b0a8-a797db9e59a9");
        public static readonly Guid YesInternalToThisRSS = Guid.Parse("fc4ddc3d-9b1c-4cb6-970c-94c122ef2fdc");
        
        public static readonly WillStudyUseCTU[] Values = new WillStudyUseCTU[] {
            new WillStudyUseCTU { Id = Guid.Parse("8ba8e39c-8792-4901-9a74-2788e972ac60"), Name = "No" },
            new WillStudyUseCTU { Id = Guid.Parse("4672a5ec-1c83-4a66-b0a8-a797db9e59a9"), Name = "Yes - External to this RSS" },
            new WillStudyUseCTU { Id = Guid.Parse("fc4ddc3d-9b1c-4cb6-970c-94c122ef2fdc"), Name = "Yes - Internal to this RSS" },
        };
        
        public static WillStudyUseCTU Map(Guid id)
        {
            return Values.Single(p => p.Id == id);
        }
    }
}

