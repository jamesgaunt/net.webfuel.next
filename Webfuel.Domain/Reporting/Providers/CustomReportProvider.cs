﻿using Azure.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Common;
using Webfuel.Domain.StaticData;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public interface ICustomReportProvider: IReportProvider
    {
    }

    [Service(typeof(ICustomReportProvider), typeof(IReportProvider))]
    internal class CustomReportProvider : ICustomReportProvider
    {
        const string TeamActivityReport = "TeamActivityReport";
        const string SupportHoursSpentReport = "SupportHoursSpentReport";
        const string AssignedProjectsReport = "AssignedProjectsReport";

        public CustomReportProvider()
        {
        }

        public Guid Id => ReportProviderEnum.CustomReport;

        public ReportBuilderBase GetReportBuilder(ReportRequest request)
        {
            switch(request.Design.CustomReportProvider)
            {
                case TeamActivityReport:
                    return new TeamActivityReportBuilder(request);

                case SupportHoursSpentReport:
                    return new SupportHoursSpentReportBuilder(request);

                case AssignedProjectsReport:
                    return new AssignedProjectsReportBuilder(request);

                default:
                    throw new NotImplementedException($"Custom report provider '{request.Design.CustomReportProvider}' does not have a registered report builder");
            }
        }

        public Task<IEnumerable<object>> QueryItems(Query query)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalCount(Query query)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReportArgument>> GenerateArguments(ReportDesign design, IServiceProvider serviceProvider)
        {
            if (design.CustomReportProvider == TeamActivityReport)
                return TeamActivityReportBuilder.GenerateArguments(design, serviceProvider);

            if (design.CustomReportProvider == SupportHoursSpentReport)
                return SupportHoursSpentReportBuilder.GenerateArguments(design, serviceProvider);

            if (design.CustomReportProvider == AssignedProjectsReport)
                return AssignedProjectsReportBuilder.GenerateArguments(design, serviceProvider);

            throw new NotImplementedException($"Custom report provider '{design.CustomReportProvider}' does not have a registered argument generator");
        }

        // Schema

        public ReportSchema Schema
        {
            get
            {
                if (_schema == null)
                {
                    var bldr = ReportSchemaBuilder<object>.Create(ReportProviderEnum.CustomReport);
                    _schema = bldr.Schema;
                }
                return _schema;
            }
        }

        static ReportSchema? _schema = null;
    }
}

