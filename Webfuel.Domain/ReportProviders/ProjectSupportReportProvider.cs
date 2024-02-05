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
    public interface IProjectSupportReportProvider : IReportProvider
    {
    }

    [Service(typeof(IProjectSupportReportProvider), typeof(IReportProvider))]
    internal class ProjectSupportReportProvider : IProjectSupportReportProvider
    {
        private readonly IProjectSupportRepository _projectSupportRepository;
        private readonly IServiceProvider _serviceProvider;

        public ProjectSupportReportProvider(IProjectSupportRepository projectSupportRepository, IServiceProvider serviceProvider)
        {
            _projectSupportRepository = projectSupportRepository;
            _serviceProvider = serviceProvider;
        }

        public Guid Id => ReportProviderEnum.ProjectSupport;

        public ReportBuilderBase GetReportBuilder(ReportRequest request)
        {
            return new ReportBuilder(request);
        }

        public async Task<IEnumerable<object>> QueryItems(int skip, int take)
        {
            var result = await _projectSupportRepository.QueryProjectSupport(new Query { Skip = skip, Take = take }, countTotal: false);
            return result.Items;
        }

        public async Task<int> GetTotalCount()
        {
            var result = await _projectSupportRepository.QueryProjectSupport(new Query(), selectItems: false, countTotal: true);
            return result.TotalCount;
        }

        public ReportSchema Schema
        {
            get
            {
                if (_schema == null)
                {
                    var builder = ReportSchemaBuilder<ProjectSupport>.Create(ReportProviderEnum.ProjectSupport);

                    builder.Add(Guid.Parse("25c60e05-6514-4969-b844-e984326f4f36"), "Date of Support", p => p.Date);

                    builder.Add(Guid.Parse("57204248-4827-4f69-a739-14f1c70dd617"), "Time in Hours", p => p.WorkTimeInHours);

                    builder.Map<Project>(Guid.Parse("609b9d79-4ff5-46af-9ff7-19099d879bb8"), "Project", p => p.ProjectId, a =>
                    {
                        a.Add(Guid.Parse("b9dc13e6-c221-4886-8243-7618d5fdffbf"), "Project Number", p => p.Number);
                        a.Add(Guid.Parse("810b1424-5d44-40d4-b84f-438636a1c0e0"), "Project Title", p => p.Title);

                        a.Map<ProjectStatus>(Guid.Parse("0bfd322c-e155-410f-937a-52c547d1949f"), "Project Status", p => p.StatusId);
                        a.Map<User>(Guid.Parse("7820e23c-9b11-4539-8477-552814e351da"), "Project Lead Adviser", p => p.LeadAdviserUserId);
                    });

                    builder.Map<User>(Guid.Parse("a542588f-4ba3-418b-8957-0040155f9287"), "Advisers", p => p.AdviserIds);
                    builder.Map<SupportTeam>(Guid.Parse("37fa0df6-9ef3-4b3b-9f66-7420b655833b"), "Support Teams", p => p.TeamIds);
                    builder.Map<SupportProvided>(Guid.Parse("2ccd3c99-9251-4051-8adc-a1970ac81511"), "Support Provided", p => p.SupportProvidedIds);

                    builder.Add(Guid.Parse("ece243c3-b5dd-4c25-a559-5e4ffe2bafe0"), "Description", p => p.Description);

                    _schema = builder.Schema;
                }

                return _schema;
            }
        }

        static ReportSchema? _schema = null;
    }
}
