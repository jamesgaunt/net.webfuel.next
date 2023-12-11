using FluentValidation.Validators;
using Webfuel.Domain.StaticData;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public interface ISupportRequestReportProvider: IReportProvider
    {
    }

    [Service(typeof(ISupportRequestReportProvider), typeof(IReportProvider))]
    internal class SupportRequestReportProvider : ISupportRequestReportProvider
    {
        private readonly ISupportRequestRepository _supportRequestRepository;

        public SupportRequestReportProvider(ISupportRequestRepository supportRequestRepository)
        {
            _supportRequestRepository = supportRequestRepository;
        }

        public Guid Id => ReportProviderEnum.SupportRequest;

        public ReportBuilderBase GetReportBuilder(ReportRequest request)
        {
            return new ReportBuilder(request);
        }

        public async Task<IEnumerable<object>> QueryItems(int skip, int take)
        {
            var result = await _supportRequestRepository.QuerySupportRequest(new Query { Skip = skip, Take = take }, countTotal: false);
            return result.Items;
        }

        public async Task<int> GetTotalCount()
        {
            var result = await _supportRequestRepository.QuerySupportRequest(new Query(), selectItems: false, countTotal: true);
            return result.TotalCount;
        }
        public ReportSchema Schema
        {
            get
            {
                if (_schema == null)
                {
                    var builder = new ReportSchemaBuilder<SupportRequest>(ReportProviderEnum.SupportRequest);

                    builder.Add(Guid.Parse("82b05021-9512-4217-9e71-bb0bc9bc8384"), p => p.Number);
                    builder.Add(Guid.Parse("c3b0b5a0-5b1a-4b7e-9b9a-0b6b8b8b6b8b"), p => p.PrefixedNumber);

                    builder.Ref<ISupportRequestStatusReferenceProvider>(Guid.Parse("c3b0b5a0-5b1a-4b7e-9b9a-0b6b8b8b6b8b"), p => p.StatusId);

                    builder.Add(Guid.Parse("cbeb9e2d-59a2-4896-a3c5-01c5c2aa42c7"), p => p.Title);
                    builder.Add(Guid.Parse("edde730a-8424-4415-b23c-29c4ae3e36b8"), p => p.DateOfRequest);

                    _schema = builder.Schema;
                }

                return _schema;
            }
        }

        static ReportSchema? _schema = null;
    }
}
