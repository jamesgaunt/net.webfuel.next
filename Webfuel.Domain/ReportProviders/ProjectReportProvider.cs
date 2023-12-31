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
        private readonly IServiceProvider _serviceProvider;

        public ProjectReportProvider(IProjectRepository projectRepository, IServiceProvider serviceProvider)
        {
            _projectRepository = projectRepository;
            _serviceProvider = serviceProvider;
        }

        public Guid Id => ReportProviderEnum.Project;
         
        public ReportBuilderBase GetReportBuilder(ReportRequest request)
        {
            return new ReportBuilder(request);
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

        public Task<QueryResult<object>> QueryReferenceField(Guid fieldId, Query query)
        {
            var field = Schema.GetField(fieldId);
            if(field == null)
                throw new InvalidOperationException($"Field {fieldId} not found");

            if (field is not ReportReferenceField referenceField)
                throw new InvalidOperationException($"Field {field.Name} is not a reference field");

            return referenceField.GetMapper(_serviceProvider).Query(query);
        }

        public ReportSchema Schema
        {
            get
            {
                if (_schema == null)
                {
                    var builder = ReportSchemaBuilder<Project>.Create(ReportProviderEnum.Project);

                    builder.Add(Guid.Parse("82b05021-9512-4217-9e71-bb0bc9bc8384"), "Number", p => p.Number);
                    builder.Add(Guid.Parse("c3b0b5a0-5b1a-4b7e-9b9a-0b6b8b8b6b8b"), "Prefixed Number", p => p.PrefixedNumber);

                    // builder.Ref<IProjectStatusReferenceProvider>(Guid.Parse("10a8218f-9de8-4835-930f-3c0f06bdbcfa"), p => p.StatusId);

                    builder.Add(Guid.Parse("cbeb9e2d-59a2-4896-a3c5-01c5c2aa42c7"), "Title", p => p.Title);
                    builder.Add(Guid.Parse("edde730a-8424-4415-b23c-29c4ae3e36b8"), "Date of Request", p => p.DateOfRequest);
                    builder.Add(Guid.Parse("f64ac394-a697-4f22-92cc-274571bc65ae"), "Closure Date", p => p.ClosureDate);

                    _schema = builder.Schema;
                }

                return _schema;
            }
        }

        static ReportSchema? _schema = null;
    }
}
