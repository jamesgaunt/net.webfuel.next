using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain.StaticData;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public class RunAnnualReport: IRequest<ReportStep>
    {
        public required DateOnly? FromDate { get; set; }

        public required DateOnly? ToDate { get; set; }

        public required bool HighlightInvalidCells { get; set; }
    }

    internal class RunAnnualReportHandler: IRequestHandler<RunAnnualReport, ReportStep>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReportGeneratorService _reportGeneratorService;

        public RunAnnualReportHandler(IServiceProvider serviceProvider, IReportGeneratorService reportGeneratorService)
        {
            _serviceProvider = serviceProvider;
            _reportGeneratorService = reportGeneratorService;
        }

        public Task<ReportStep> Handle(RunAnnualReport request, CancellationToken cancellationToken)
        {
            var builder = new AnnualReportBuilder(_serviceProvider, new AnnualReportSettings { 
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                HighlightInvalidCells = request.HighlightInvalidCells
            });
            return Task.FromResult(_reportGeneratorService.RegisterReport(builder));
        }
    }
}
