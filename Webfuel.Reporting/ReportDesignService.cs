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

        Task<ReportStep> RegisterReport(ReportRequest request);

        Task<IEnumerable<object>> QueryItems(Guid reportProviderId, int skip, int take);

        Task<int> GetTotalCount(Guid reportProviderId);

        Task<ReportReference?> GetReportReference(Guid reportProviderId, Guid fieldId, Guid id);

        Task<QueryResult<ReportReference>> QueryReportReference(Guid reportProviderId, Guid fieldId, Query query);

        void ValidateDesign(Guid reportProviderId, ReportDesign design);
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

        public async Task<ReportStep> RegisterReport(ReportRequest request)
        {
            var provider = GetReportProvider(request.ReportProviderId);
            var builder = await provider.GetReportBuilder(request);
            return await _reportGeneratorService.RegisterReport(builder);
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

        public async Task<ReportReference?> GetReportReference(Guid reportProviderId, Guid fieldId, Guid id)
        {
            var referenceProvider = GetReferenceProvider(reportProviderId, fieldId);
            return await referenceProvider.GetReportReference(id);
        }

        public async Task<QueryResult<ReportReference>> QueryReportReference(Guid reportProviderId, Guid fieldId, Query query)
        {
            var referenceProvider = GetReferenceProvider(reportProviderId, fieldId);
            return await referenceProvider.QueryReportReference(query);
        }

        public void ValidateDesign(Guid reportProviderId, ReportDesign design)
        {
            var schema = GetReportSchema(reportProviderId);
            foreach (var filter in design.Filters)
                filter.ValidateFilter(schema);
        }

        // Helpers

        IReportProvider GetReportProvider(Guid id)
        {
            var providers = _serviceProvider.GetServices<IReportProvider>();

            return providers.FirstOrDefault(p => p.Id == id)
                ?? throw new InvalidOperationException("The specified report provider does not exist.");
        }

        public IReportReferenceProvider GetReferenceProvider(Guid reportProviderId, Guid fieldId)
        {
            var schema = GetReportSchema(reportProviderId);

            var field = schema.Fields.FirstOrDefault(f => f.Id == fieldId)
                ?? throw new InvalidOperationException("The specified field does not exist.");

            var referenceProviderType = typeof(void);

            if (field is ReportReferenceField referenceField)
                referenceProviderType = referenceField.ReferenceProviderType;

            else if (field is ReportReferenceListField referenceListField)
                referenceProviderType = referenceListField.ReferenceProviderType;

            else
                throw new InvalidOperationException("The specified field is not a reference field.");

            return (IReportReferenceProvider)_serviceProvider.GetRequiredService(referenceProviderType);
        }
    }
}
