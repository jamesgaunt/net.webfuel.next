using Azure.Core;
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
        Task<ReportSchema> GetReportSchema(Guid reportProviderId);

        Task<ReportStep> RegisterReport(ReportRequest request);

        Task<ReportReference?> GetReportReference(Guid reportProviderId, Guid fieldId, Guid id);

        Task<QueryResult<ReportReference>> QueryReportReference(Guid reportProviderId, Guid fieldId, Query query);
    }

    [Service(typeof(IReportDesignService))]
    internal class ReportDesignService: IReportDesignService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReportGeneratorService _reportGeneratorService;

        public ReportDesignService(IServiceProvider serviceProvider, IReportGeneratorService reportGeneratorService)
        {
            _serviceProvider = serviceProvider;
            _reportGeneratorService = reportGeneratorService;   
        }

        public async Task<ReportSchema> GetReportSchema(Guid reportProviderId)
        {
            var provider = GetReportProvider(reportProviderId);
            return await provider.GetReportSchema();
        }

        public async Task<ReportStep> RegisterReport(ReportRequest request)
        {
            var provider = GetReportProvider(request.ProviderId);
            var builder = await provider.InitialiseReport(request);
            return await _reportGeneratorService.RegisterReport(builder);
        }

        public async Task<ReportReference?> GetReportReference(Guid reportProviderId, Guid fieldId, Guid id)
        {
            var referenceProvider = await GetReferenceProvider(reportProviderId, fieldId);
            return await referenceProvider.GetReportReference(id);
        }

        public async Task<QueryResult<ReportReference>> QueryReportReference(Guid reportProviderId, Guid fieldId, Query query)
        {
            var referenceProvider = await GetReferenceProvider(reportProviderId, fieldId);
            return await referenceProvider.QueryReportReference(query);
        }

        // Helpers

        IReportProvider GetReportProvider(Guid id)
        {
            var providers = _serviceProvider.GetServices<IReportProvider>();

            return providers.FirstOrDefault(p => p.Id == id)
                ?? throw new InvalidOperationException("The specified report provider does not exist.");
        }

        public async Task<IReportReferenceProvider> GetReferenceProvider(Guid reportProviderId, Guid fieldId)
        {
            var schema = await GetReportSchema(reportProviderId);

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
