using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Common
{
    public interface IReportService
    {
        Task<ReportProgress> RegisterReport(ReportTask task);

        Task<ReportProgress> GenerateReport(Guid taskId);

        Task<ReportResult?> GenerateResult(Guid taskId);

        Task CancelReport(Guid taskId);
    }

    [Service(typeof(IReportService))]
    internal class ReportService: IReportService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReportTaskService _reportTaskService;

        public ReportService(IServiceProvider serviceProvider, IReportTaskService reportTaskService) 
        { 
            _serviceProvider = serviceProvider;
            _reportTaskService = reportTaskService;
        }

        public Task<ReportProgress> RegisterReport(ReportTask task)
        {
            _reportTaskService.StoreTask(task);

            return Task.FromResult(ReportProgress.FromTask(task));
        }

        public async Task<ReportProgress> GenerateReport(Guid taskId)
        {
            var task = _reportTaskService.RetrieveTask(taskId);
            if (task == null)
                throw new DomainException("The specified report task no longer exists");

            var reportGenerator = _serviceProvider.GetService(task.ReportGeneratorType) as IReportGenerator;
            if(reportGenerator == null)
                throw new DomainException("Unable to instantiate report generator of type " + task.ReportGeneratorType.Name);

            await reportGenerator.GenerateReport(task);

            return ReportProgress.FromTask(task);
        }

        public Task<ReportResult?> GenerateResult(Guid taskId)
        {
            var task = _reportTaskService.RetrieveTask(taskId);
            if (task == null)
                return Task.FromResult<ReportResult?>(null);

            var result = task.GenerateResult();

            _reportTaskService.DeleteTask(task.TaskId);

            return Task.FromResult<ReportResult?>(result);
        }

        public Task CancelReport(Guid taskId)
        {
            var task = _reportTaskService.RetrieveTask(taskId);
            if (task == null)
                return Task.CompletedTask;

            _reportTaskService.DeleteTask(task.TaskId);

            return Task.CompletedTask;
        }
    }
}
