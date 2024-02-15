using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Math;
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
    public interface IAnnualReportProvider : IReportProvider
    {
    }

    [Service(typeof(IAnnualReportProvider), typeof(IReportProvider))]
    internal class AnnualReportProvider : IAnnualReportProvider
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IServiceProvider _serviceProvider;

        public AnnualReportProvider(IProjectRepository projectRepository, IServiceProvider serviceProvider)
        {
            _projectRepository = projectRepository;
            _serviceProvider = serviceProvider;
        }

        public Guid Id => ReportProviderEnum.Project;

        public ReportBuilderBase GetReportBuilder(ReportRequest request)
        {
            return new ReportBuilder(request);
        }

        public async Task<IEnumerable<object>> QueryItems(Query query)
        {
            var result = await _projectRepository.QueryProject(query, countTotal: false);
            return result.Items;
        }

        public async Task<int> GetTotalCount(Query query)
        {
            var result = await _projectRepository.QueryProject(query, selectItems: false, countTotal: true);
            return result.TotalCount;
        }

        public ReportSchema Schema
        {
            get
            {
                if (_schema == null)
                {
                    var builder = ReportSchemaBuilder<Project>.Create(ReportProviderEnum.Project);
                    _schema = builder.Schema;
                }
                return _schema;
            }
        }

        static ReportSchema? _schema = null;
    }
}
