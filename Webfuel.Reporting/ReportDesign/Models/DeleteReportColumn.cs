using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class DeleteReportColumn: IRequest<ReportDesign>
    {
        public required Guid ReportProviderId { get; init; }

        public required ReportDesign Design { get; init; }

        public required Guid Id { get; init; }
    }
}
