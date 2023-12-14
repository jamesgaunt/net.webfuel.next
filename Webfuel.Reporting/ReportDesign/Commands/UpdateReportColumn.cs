using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class UpdateReportColumn: IRequest<ReportDesign>
    {
        public required Guid ReportProviderId { get; init; }

        public required ReportDesign Design { get; init; }

        public required Guid Id { get; init; }

        public required string Title { get; init; }

        public double? Width { get; set; }

        public bool Bold { get; set; }
    }
}
