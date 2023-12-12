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

        Task<IEnumerable<object>> QueryItems(Guid reportProviderId, int skip, int take);

        Task<int> GetTotalCount(Guid reportProviderId);

        void ValidateDesign(Guid reportProviderId, ReportDesign design);

        Task<QueryResult<ReportReference>> QueryReferenceField(Guid reportProviderId, Guid fieldId, Query query);
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
            var provider = GetReportProvider(request.ReportProviderId);
            var builder = provider.GetReportBuilder(request);
            return _reportGeneratorService.RegisterReport(builder);
        }

        public Task<IEnumerable<object>> QueryItems(Guid reportProviderId, int skip, int take)
        {
            var provider = GetReportProvider(reportProviderId);
            return provider.QueryItems(skip, take);
        }

        public Task<int> GetTotalCount(Guid reportProviderId)
        {
            var provider = GetReportProvider(reportProviderId);
            return provider.GetTotalCount();
        }

        public void ValidateDesign(Guid reportProviderId, ReportDesign design)
        {
            var schema = GetReportSchema(reportProviderId);
            design.ValidateDesign(schema);
        }

        public Task<QueryResult<ReportReference>> QueryReferenceField(Guid reportProviderId, Guid fieldId, Query query)
        {
            var provider = GetReportProvider(reportProviderId);

            var field = provider.Schema.GetField(fieldId);
            if(field == null)
                throw new InvalidOperationException($"The specified field does not exist");

            if(field is not IReportReferenceField referenceField)
                throw new InvalidOperationException($"Field {field.Name} is not a reference field");

            return referenceField.QueryReference(query, _serviceProvider);
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
