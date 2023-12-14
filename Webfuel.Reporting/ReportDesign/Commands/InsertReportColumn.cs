using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class InsertReportColumn: IRequest<ReportDesign>
    {
        public required Guid ReportProviderId { get; init; }

        public required ReportDesign Design { get; init; }

        public required List<Guid> FieldIds { get; init; }
    }
}
