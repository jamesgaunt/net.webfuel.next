using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    [Mapper]
    internal partial class SupportRequestMapper
    {
        public partial SupportRequest Map(CreateSupportRequest request);

        public partial void  Apply(UpdateSupportRequest request, SupportRequest existing);

        public partial void Apply(UpdateSupportRequestResearcher request, SupportRequest existing);
    }
}
