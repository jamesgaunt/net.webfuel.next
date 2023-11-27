using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;

namespace Webfuel.Domain
{
    internal class ProjectReportingContext : IReportingContext<Project>
    {
        public Guid Id => Guid.Parse("8df47d66-9648-4ed0-8afb-77ccf091a940");

        public string Name => "Project";

        public IEnumerable<ReportingField<Project>> Fields => throw new NotImplementedException();
    }
}
