﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public interface IReportGeneratorService
    {
        Task<ReportStep> RegisterReport(ReportBuilder builder);

        Task<ReportStep> GenerateReport(Guid taskId);

        Task<ReportResult> RenderReport(Guid taskId);

        Task CancelReport(Guid taskId);
    }

    [Service(typeof(IReportGeneratorService))]
    internal class ReportGeneratorService: IReportGeneratorService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReportTaskService _reportTaskService;

        public ReportGeneratorService(IServiceProvider serviceProvider, IReportTaskService reportTaskService) 
        { 
            _serviceProvider = serviceProvider;
            _reportTaskService = reportTaskService;
        }

        public async Task<ReportStep> RegisterReport(ReportBuilder builder)
        {
            var task =_reportTaskService.RegisterTask(builder);

            task.Builder.ServiceProvider = _serviceProvider;
            await task.Builder.InitialiseReport();
            return ReportStep.FromTask(task);
        }

        public async Task<ReportStep> GenerateReport(Guid taskId)
        {
            var task = _reportTaskService.RetrieveTask(taskId);
            if (task == null)
                throw new DomainException("The specified report task no longer exists");

            task.Builder.ServiceProvider = _serviceProvider;
            await task.Builder.GenerateReport();
            return ReportStep.FromTask(task);
        }

        public async Task<ReportResult> RenderReport(Guid taskId)
        {
            var task = _reportTaskService.RetrieveTask(taskId);
            if (task == null)
                return new ReportResult();

            task.Builder.ServiceProvider = _serviceProvider;
            var result = await task.Builder.RenderReport();

            _reportTaskService.DeleteTask(task.TaskId);

            return result;
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