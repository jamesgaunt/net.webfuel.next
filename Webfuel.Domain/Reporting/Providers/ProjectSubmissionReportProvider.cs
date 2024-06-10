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
    public interface IProjectSubmissionReportProvider : IReportProvider
    {
    }

    [Service(typeof(IProjectSubmissionReportProvider), typeof(IReportProvider))]
    internal class ProjectSubmissionReportProvider : IProjectSubmissionReportProvider
    {
        private readonly IProjectSubmissionRepository _projectSubmissionRepository;
        private readonly IServiceProvider _serviceProvider;

        public ProjectSubmissionReportProvider(IProjectSubmissionRepository projectSubmissionRepository, IServiceProvider serviceProvider)
        {
            _projectSubmissionRepository = projectSubmissionRepository;
            _serviceProvider = serviceProvider;
        }

        public Guid Id => ReportProviderEnum.ProjectSubmission;

        public ReportBuilderBase GetReportBuilder(ReportRequest request)
        {
            return new ReportBuilder(request);
        }

        public async Task<IEnumerable<object>> QueryItems(Query query)
        {
            var result = await _projectSubmissionRepository.QueryProjectSubmission(query, countTotal: false);
            return result.Items;
        }

        public async Task<int> GetTotalCount(Query query)
        {
            var result = await _projectSubmissionRepository.QueryProjectSubmission(query, selectItems: false, countTotal: true);
            return result.TotalCount;
        }

        public ReportSchema Schema
        {
            get
            {
                if (_schema == null)
                {
                    var builder = ReportSchemaBuilder<ProjectSubmission>.Create(ReportProviderEnum.ProjectSubmission);

                    builder.Add(Guid.Parse("e4293870-b4dd-43a4-bf03-ed6f7b5882ee"), "Submission Date", p => p.SubmissionDate);
                    builder.Add(Guid.Parse("c0af308f-589e-4a9e-bffa-3251f4f2716d"), "NIHR Reference", p => p.NIHRReference);
                    builder.Add(Guid.Parse("7c062fc1-9cb5-434f-bdfc-1ef3143b8aab"), "Funding Amount", p => p.FundingAmountOnSubmission);

                    builder.Map<SubmissionStage>(Guid.Parse("d87aaebc-8014-4079-9e12-e6a904d6424d"), "Submission Stage", p => p.SubmissionStageId);
                    builder.Map<SubmissionOutcome>(Guid.Parse("b4903e96-5954-490a-9892-7b007607e8fe"), "Submission Outcome", p => p.SubmissionOutcomeId);

                    builder.Map<Project>(Guid.Parse("609b9d79-4ff5-46af-9ff7-19099d879bb8"), "Project", p => p.ProjectId, a =>
                    {
                        a.Add(Guid.Parse("b9dc13e6-c221-4886-8243-7618d5fdffbf"), "Project Number", p => p.Number);
                        a.Add(Guid.Parse("810b1424-5d44-40d4-b84f-438636a1c0e0"), "Project Title", p => p.Title);
                        a.Add(Guid.Parse("3d18e615-0e65-4492-bdb3-6ff05ab6030a"), "Project Date of Request", p => p.DateOfRequest);

                        a.Map<ProjectStatus>(Guid.Parse("0bfd322c-e155-410f-937a-52c547d1949f"), "Project Status", p => p.StatusId);
                        a.Map<User>(Guid.Parse("7820e23c-9b11-4539-8477-552814e351da"), "Project Lead Adviser", p => p.LeadAdviserUserId);
                    });


                    _schema = builder.Schema;
                }

                return _schema;
            }
        }

        static ReportSchema? _schema = null;
    }
}
