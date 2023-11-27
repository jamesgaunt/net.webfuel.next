using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;

namespace Webfuel.Domain
{

    public interface IProjectExportService: IReportGenerator
    {
        Task<ReportProgress> InitialiseReport(ProjectExportRequest request);
    }

    [Service(typeof(IProjectExportService))]
    internal class ProjectExportService : IProjectExportService
    {
        private readonly IReportService _reportService;
        private readonly IProjectRepository _projectRepository;

        public ProjectExportService(IReportService reportService, IProjectRepository projectRepository)
        {
            _reportService = reportService;
            _projectRepository = projectRepository;
        }

        public Task<ReportProgress> InitialiseReport(ProjectExportRequest request)
        {
            var task = new ProjectExportTask(request);
            return _reportService.RegisterReport(task);
        }

        public async Task GenerateReport(ReportTask _task)
        {
            if (!(_task is ProjectExportTask task))
                throw new DomainException("Wrong type of task passed to report generator");

            task.Query.Skip = task.ProgressCount;
            task.Query.Take = 10;

            var result = await _projectRepository.QueryProject(task.Query, countTotal: task.Query.Skip == 0);

            if (task.Query.Skip == 0) 
                task.TotalCount = result.TotalCount;
            task.ProgressCount += task.Query.Take;

            foreach(var item in result.Items)
            {
                var row = task.Data.AddRow();

                row.AddCell(item.PrefixedNumber);
                row.AddCell(item.Title);
            }
        }
    }
}
