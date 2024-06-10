using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    internal static class SupportRequestMapper
    {
        public static SupportRequest Map(CreateSupportRequest request)
        {
            var created = Mapper.Map(request);
            return created;
        }

        public static SupportRequest Apply(UpdateSupportRequest request, SupportRequest existing)
        {
            var updated = existing.Copy();
            Mapper.Apply(request, updated);
            return updated;
        }

        public static SupportRequest Apply(UpdateSupportRequestResearcher request, SupportRequest existing)
        {
            var updated = existing.Copy();
            Mapper.Apply(request, updated);
            return updated;
        }

        public static Project Apply(SupportRequest request, Project existing)
        {
            var updated = existing.Copy();
            Mapper.Apply(request, updated);
            return updated;
        }

        static SupportRequestMapperImpl Mapper => new SupportRequestMapperImpl();
    }



    [Mapper]
    internal partial class SupportRequestMapperImpl
    {
        [MapperIgnoreTarget(nameof(SupportRequest.Id))]
        [MapperIgnoreTarget(nameof(SupportRequest.StatusId))]
        [MapperIgnoreTarget(nameof(SupportRequest.FileStorageGroupId))]
        public partial SupportRequest Map(CreateSupportRequest request);


        [MapperIgnoreTarget(nameof(SupportRequest.Id))]
        [MapperIgnoreTarget(nameof(SupportRequest.StatusId))]
        [MapperIgnoreTarget(nameof(SupportRequest.FileStorageGroupId))]
        public partial void  Apply(UpdateSupportRequest request, SupportRequest existing);


        [MapperIgnoreTarget(nameof(SupportRequest.Id))]
        [MapperIgnoreTarget(nameof(SupportRequest.StatusId))]
        public partial void Apply(UpdateSupportRequestResearcher request, SupportRequest existing);


        [MapperIgnoreTarget(nameof(Project.Id))]
        [MapperIgnoreTarget(nameof(Project.StatusId))]
        public partial void Apply(SupportRequest request, Project existing);
    }
}
