﻿using Microsoft.Extensions.DependencyInjection;
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
    public interface IProjectReportProvider: IReportProvider
    {
    }

    [Service(typeof(IProjectReportProvider), typeof(IReportProvider))]
    internal class ProjectReportProvider : IProjectReportProvider
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectReportProvider(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public Guid Id => ReportProviderEnum.Project;
         
        public ReportBuilder GetReportBuilder(ReportRequest request)
        {
            return new StandardReportBuilder(request);
        }

        public async Task<IEnumerable<object>> QueryItems(int skip, int take)
        {
            var result = await _projectRepository.QueryProject(new Query { Skip = skip, Take = take }, countTotal: false);
            return result.Items;
        }

        public async Task<int> GetTotalCount()
        {
            var result = await _projectRepository.QueryProject(new Query(), selectItems: false, countTotal: true);
            return result.TotalCount;
        }

        public ReportSchema Schema
        {
            get
            {
                if (_schema == null)
                {
                    var builder = new ReportSchemaBuilder<Project>(ReportProviderEnum.Project);

                    builder.AddProperty(Guid.Parse("82b05021-9512-4217-9e71-bb0bc9bc8384"), p => p.Number);
                    builder.AddProperty(Guid.Parse("c3b0b5a0-5b1a-4b7e-9b9a-0b6b8b8b6b8b"), p => p.PrefixedNumber);
                    builder.AddReference<IProjectStatusReferenceProvider>(Guid.Parse("10a8218f-9de8-4835-930f-3c0f06bdbcfa"), p => p.StatusId);

                    builder.AddProperty(Guid.Parse("cbeb9e2d-59a2-4896-a3c5-01c5c2aa42c7"), p => p.Title);
                    builder.AddProperty(Guid.Parse("edde730a-8424-4415-b23c-29c4ae3e36b8"), p => p.DateOfRequest);
                    builder.AddProperty(Guid.Parse("f64ac394-a697-4f22-92cc-274571bc65ae"), p => p.ClosureDate);

                    _schema = builder.Schema;
                }

                return _schema;
            }
        }

        static ReportSchema? _schema = null;
    }
}