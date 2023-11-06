using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    [Mapper]
    internal partial class SupportRequestMapper
    {
        public partial SupportRequest Map(CreateSupportRequest request);


        [MapperIgnoreTarget(nameof(SupportRequest.Id))]
        public partial void  Apply(UpdateSupportRequest request, SupportRequest existing);


        [MapperIgnoreTarget(nameof(SupportRequest.Id))]
        public partial void Apply(UpdateSupportRequestResearcher request, SupportRequest existing);


        [MapperIgnoreTarget(nameof(Project.Id))]
        public partial void Apply(SupportRequest request, Project existing);
    }
}
