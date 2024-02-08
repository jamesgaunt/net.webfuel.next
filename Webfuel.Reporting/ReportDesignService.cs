using Azure.Core;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public interface IReportDesignService
    {
        ReportSchema GetReportSchema(Guid reportProviderId);

        ReportStep RegisterReport(ReportRequest request);

        Task<IEnumerable<object>> QueryItems(Guid reportProviderId, Query query);

        Task<int> GetTotalCount(Guid reportProviderId, Query query);

        Task<ReportDesign> ValidateDesign(ReportDesign design);

        Task<List<ReportArgument>> GenerateArguments(ReportDesign design);

        Task<QueryResult<ReportMapEntity>> QueryReferenceField(Guid reportProviderId, Guid fieldId, Query query);
    }

    [Service(typeof(IReportDesignService))]
    internal class ReportDesignService : IReportDesignService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReportGeneratorService _reportGeneratorService;

        public ReportDesignService(IServiceProvider serviceProvider, IReportGeneratorService reportGeneratorService)
        {
            _serviceProvider = serviceProvider;
            _reportGeneratorService = reportGeneratorService;
        }

        public ReportSchema GetReportSchema(Guid reportProviderId)
        {
            var provider = GetReportProvider(reportProviderId);
            return provider.Schema;
        }

        public ReportStep RegisterReport(ReportRequest request)
        {
            var provider = GetReportProvider(request.Design.ReportProviderId);
            var builder = provider.GetReportBuilder(request);
            return _reportGeneratorService.RegisterReport(builder);
        }

        public Task<IEnumerable<object>> QueryItems(Guid reportProviderId, Query query)
        {
            var provider = GetReportProvider(reportProviderId);
            return provider.QueryItems(query);
        }

        public Task<int> GetTotalCount(Guid reportProviderId, Query query)
        {
            var provider = GetReportProvider(reportProviderId);
            return provider.GetTotalCount(query);
        }

        public async Task<ReportDesign> ValidateDesign(ReportDesign design)
        {
            var schema = GetReportSchema(design.ReportProviderId);
            await design.Validate(schema, _serviceProvider);
            return design;
        }

        public Task<List<ReportArgument>> GenerateArguments(ReportDesign design)
        {
            return design.GenerateArguments(_serviceProvider);
        }

        public Task<QueryResult<ReportMapEntity>> QueryReferenceField(Guid reportProviderId, Guid fieldId, Query query)
        {
            var provider = GetReportProvider(reportProviderId);

            var field = provider.Schema.GetField(fieldId);
            if (field == null)
                throw new InvalidOperationException($"The specified field does not exist");

            if (field.FieldType != ReportFieldType.Reference)
                throw new InvalidOperationException($"Field {field.Name} is not a reference field");

            return field.GetMap(_serviceProvider).Query(query);
        }

        // Helpers

        IReportProvider GetReportProvider(Guid id)
        {
            var providers = _serviceProvider.GetServices<IReportProvider>();

            return providers.FirstOrDefault(p => p.Id == id)
                ?? throw new InvalidOperationException("The specified report provider does not exist.");
        }
    }
}
