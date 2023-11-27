using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;
using Webfuel.Excel;

namespace Webfuel.Domain
{
    public class ProjectExportRequest
    {
        public string Number { get; set; } = String.Empty;

        public string Title { get; set; } = String.Empty;

        public DateOnly? FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        public Guid? StatusId { get; set; }

        public Guid? FundingStreamId { get; set; }
    }

    public class ProjectExportTask: ReportTask
    {
        public override Type ReportGenerator => typeof(IProjectExportService);
        public required ProjectExportRequest Request { get; init; }
        public int Taken { get; set; }
        public int Total { get; set; }
        public ExcelData Data { get; } = new ExcelData();
    }

    public interface IProjectExportService
    {
        Task<ReportProgress> InitialiseReport(ProjectExportRequest request);

        Task GenerateReport(ProjectExportTask task);
    }

    [Service(typeof(IProjectExportService))]
    internal class ProjectExportService : IProjectExportService
    {
        private readonly IReportService _reportService;

        public ProjectExportService(IReportService reportService)
        {
            _reportService = reportService;
        }

        public Task<ReportProgress> InitialiseReport(ProjectExportRequest request)
        {
            var task = new ProjectExportTask
            {
                Request = request
            };

            return _reportService.RegisterReport(task);
        }

        public Task GenerateReport(ProjectExportTask task)
        {
            throw new NotImplementedException();
        }
    }
}
