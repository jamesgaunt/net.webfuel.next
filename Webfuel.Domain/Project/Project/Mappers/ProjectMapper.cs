using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    [Mapper]
    internal partial class ProjectMapper
    {
        [MapperIgnoreTarget(nameof(Project.Id))]
        [MapperIgnoreTarget(nameof(Project.StatusId))]
        public partial Project Map(CreateProject request);
    }
}
