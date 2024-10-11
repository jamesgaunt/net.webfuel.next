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
        const string ActivityReport = "ActivityReport";
        const string SupportHoursSpentReport = "SupportHoursSpentReport";

        public CustomReportProvider()
        {
        }

        public Guid Id => ReportProviderEnum.CustomReport;

        public ReportBuilderBase GetReportBuilder(ReportRequest request)
        {
            switch(request.Design.CustomReportProvider)
            {
                case ActivityReport:
                    return new ActivityReportBuilder(request);

                case SupportHoursSpentReport:
                    return new SupportHoursSpentReportBuilder(request);

                default:
                    throw new NotImplementedException();
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
            if (design.CustomReportProvider == ActivityReport)
                return ActivityReportBuilder.GenerateArguments(design, serviceProvider);

            if (design.CustomReportProvider == SupportHoursSpentReport)
                return SupportHoursSpentReportBuilder.GenerateArguments(design, serviceProvider);
           
            return Task.FromResult(new List<ReportArgument>());
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

