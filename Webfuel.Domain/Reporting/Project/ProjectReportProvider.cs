using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    public interface IProjectReportProvider: IReportProvider, IReportGenerator
    {
    }

    [Service(typeof(IProjectReportProvider), typeof(IReportProvider))]
    internal class ProjectReportProvider : IProjectReportProvider
    {
        private readonly IReportGeneratorService _reportGeneratorService;
        private readonly IProjectRepository _projectRepository;

        const int ITEMS_PER_STEP = 1;

        public ProjectReportProvider(IReportGeneratorService reportGeneratorService, IProjectRepository projectRepository)
        {
            _reportGeneratorService = reportGeneratorService;
            _projectRepository = projectRepository;
        }

        public Guid Id => ReportProviderEnum.Project;

        static ProjectReportSchema Schema = new ProjectReportSchema();

        public Task<IReportSchema> GetReportSchema()
        {
            return Task.FromResult<IReportSchema>(Schema);
        }

        public async Task<ReportProgress> InitialiseReport(Report report,ReportRequest request)
        {
            var task = new ReportTask
            {
                Report = report,
                ReportGenerator = typeof(IProjectReportProvider),
            };

            return await _reportGeneratorService.RegisterReport(task);
        }

        public async Task GenerateReport(ReportTask task)
        {
            task.Query.Skip = task.ProgressCount;
            task.Query.Take = ITEMS_PER_STEP;

            var result = await _projectRepository.QueryProject(task.Query, countTotal: task.Query.Skip == 0);

            if (task.Query.Skip == 0)
            {
                // First iteration
                task.TotalCount = result.TotalCount;
            }

            if (result.Items.Count == 0)
            {
                // Last iteration
                task.ProgressCount = task.TotalCount;
                task.Complete = true;
                return;
            }

            task.ProgressCount += task.Query.Take;

            foreach (var item in result.Items)
            {
                task.Worksheet.Cell(task.CurrentRow, 1).SetValue(item.PrefixedNumber);
                task.Worksheet.Cell(task.CurrentRow, 1).SetValue(item.Title);
                task.CurrentRow++;
            }
        }
    }
}
