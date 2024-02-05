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
    public interface IProjectReportProvider : IReportProvider
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

        public ReportSchema Schema
        {
            get
            {
                if (_schema == null)
                {
                    var builder = ReportSchemaBuilder<Project>.Create(ReportProviderEnum.Project);

                    // Project

                    builder.Add(Guid.Parse("82b05021-9512-4217-9e71-bb0bc9bc8384"), "Number", p => p.Number);
                    builder.Add(Guid.Parse("c3b0b5a0-5b1a-4b7e-9b9a-0b6b8b8b6b8b"), "Prefixed Number", p => p.PrefixedNumber);
                    builder.Map<ProjectStatus>(Guid.Parse("10a8218f-9de8-4835-930f-3c0f06bdbcfa"), "Status", p => p.StatusId);
                        
                    builder.Add(Guid.Parse("cbeb9e2d-59a2-4896-a3c5-01c5c2aa42c7"), "Title", p => p.Title);
                    builder.Add(Guid.Parse("edde730a-8424-4415-b23c-29c4ae3e36b8"), "Date of Request", p => p.DateOfRequest);
                    builder.Add(Guid.Parse("f64ac394-a697-4f22-92cc-274571bc65ae"), "Closure Date", p => p.ClosureDate);
                    builder.Map<User>(Guid.Parse("9228be1d-1a09-4be6-b90f-67bc7b54427c"), "Lead Adviser", p => p.LeadAdviserUserId);
                    builder.Map<FundingStream>(Guid.Parse("19f8e8ee-bc08-4a3c-8de3-7718809ca36d"), "Funding Stream Submitted To", p => p.SubmittedFundingStreamId);
                        
                    builder.Add(Guid.Parse("e324749c-f8fb-453f-8093-f2f0f3901380"), "Funding Stream Other", p => p.SubmittedFundingStreamFreeText);
                    builder.Add(Guid.Parse("abb43da3-2c5a-4f00-84a6-b66ad03ba9a9"), "Funding Stream Submitted To Name", p => p.SubmittedFundingStreamName);

                    // Request 

                    builder.Map<ApplicationStage>(Guid.Parse("c27f4e67-a50a-4790-87f8-997cf08ac433"), "Application Stage", p => p.ApplicationStageId);
                    builder.Map<IsFellowship>(Guid.Parse("67ee92ce-717f-4e0f-b48d-d3dba338c753"), "Is this application for a fellowship?", p => p.IsFellowshipId);
                    builder.Map<FundingStream>(Guid.Parse("24e35fc4-f213-4ec2-9074-1339e3299dc8"), "Proposed Funding Stream", p => p.ProposedFundingStreamId);
                    builder.Add(Guid.Parse("9878cca6-801c-45a5-9413-a2b27d624761"), "Propose Funding Stream Name", p => p.ProposedFundingStreamName);
                    builder.Map<FundingCallType>(Guid.Parse("0c3869d3-337b-4fee-a3f4-b2bbe9df2a08"), "Proposed Funding Call Type", p => p.ProposedFundingCallTypeId);
                    builder.Add(Guid.Parse("b117a9e0-9815-47d5-9332-0174c8563303"), "Target Submission Date", p => p.TargetSubmissionDate);
                    builder.Map<IsTeamMembersConsulted>(Guid.Parse("6517bfed-790f-4987-8b75-67a8fdfe30f0"), "Have your team members been consulted for support?", p => p.IsTeamMembersConsultedId);
                    builder.Map<IsResubmission>(Guid.Parse("0720f0f0-b917-4fa0-8b81-87e7b7040050"), "Has the application or similar been submitted to a funder before?", p => p.IsResubmissionId);
                    builder.Map<IsCTUAlreadyInvolved>(Guid.Parse("7c665912-dc1e-413b-a95c-60bd624a6f21"), "Have you already got a CTU involved and if so which one?", p => p.IsCTUAlreadyInvolvedId);
                    builder.Add(Guid.Parse("1cb89101-85dc-44fa-bd98-41d7972f71c4"), "CTU Free Text", p => p.IsCTUAlreadyInvolvedFreeText);
                    builder.Map<HowDidYouFindUs>(Guid.Parse("4eddc509-b9a8-47f3-8e22-56a035674be9"), "How did you hear about our hub?", p => p.HowDidYouFindUsId);

                    // Team Contact

                    builder.Add(Guid.Parse("42aeb1aa-0728-4dd2-bebf-e00ad1156a88"), "Team Contact Title", p => p.TeamContactTitle);
                    builder.Add(Guid.Parse("0df4653c-02c9-4c27-b8d4-093c5ec3066b"), "Team Contact First Name", p => p.TeamContactFirstName);
                    builder.Add(Guid.Parse("68f5f5cd-085d-4f8f-8e17-50561840ac3e"), "Team Contact Last Name", p => p.TeamContactLastName);
                    builder.Add(Guid.Parse("252017cf-4058-4d20-b124-53cccc05c55d"), "Team Contact Email", p => p.TeamContactEmail);
                    builder.Map<ResearcherRole>(Guid.Parse("826b34c2-b3f4-4841-bb9a-4c553e5237ce"), "Team Contact Role In Application", p => p.TeamContactRoleId);
             
                    // Lead Applicant

                    builder.Add(Guid.Parse("784354ed-fc21-43de-bc31-4be238894f1c"), "Chief Investigator Title", p => p.LeadApplicantTitle);
                    builder.Add(Guid.Parse("28fe46aa-4ffe-47fa-95ec-9a0298e66070"), "Chief Investigator First Name", p => p.LeadApplicantFirstName);
                    builder.Add(Guid.Parse("fcfb5a05-b45f-4c40-919d-8be6bb14d790"), "Chief Investigator Last Name", p => p.LeadApplicantLastName);
                    builder.Add(Guid.Parse("02280a72-14c2-49f0-b080-a5f79018ee54"), "Chief Investigator Email", p => p.LeadApplicantEmail);
                    builder.Add(Guid.Parse("8664d91e-2fac-45c7-9313-1244d0e1b2d6"), "Chief Investigator Job Role", p => p.LeadApplicantJobRole);
                    builder.Map<ResearcherOrganisationType>(Guid.Parse("7202793d-bbee-433c-bdae-81ad3112174f"), "Chief Investigator Organisation Type", p => p.LeadApplicantOrganisationTypeId);
                        
                    builder.Add(Guid.Parse("fe308cbf-6fe5-402f-b883-726a26eb72bc"), "Chief Investigator Organisation", p => p.LeadApplicantOrganisation);
                    builder.Add(Guid.Parse("3b086edd-23ac-4630-875b-473372e2f2f9"), "Chief Investigator Department", p => p.LeadApplicantDepartment);
                    builder.Add(Guid.Parse("919dd385-2990-44d9-ad09-d04b5d84e071"), "Chief Investigator ORCID", p => p.LeadApplicantORCID);

                    // Work Address

                    builder.Add(Guid.Parse("4826ff06-c2aa-4b60-aca6-a71a00158717"), "Address Line 1", p => p.LeadApplicantAddressLine1);
                    builder.Add(Guid.Parse("311fa22e-6972-47ea-970c-72342c03e7d0"), "Address Line 2", p => p.LeadApplicantAddressLine2);
                    builder.Add(Guid.Parse("8ac3a661-f7b2-478b-9a40-0c61eac9acf2"), "Address Town/City", p => p.LeadApplicantAddressTown);
                    builder.Add(Guid.Parse("221abbe4-c115-4001-8336-073a48f8f165"), "Address County", p => p.LeadApplicantAddressCounty);
                    builder.Add(Guid.Parse("cbb372c8-6d75-4fe4-82a6-8466dfc15241"), "Address Country", p => p.LeadApplicantAddressCountry);
                    builder.Add(Guid.Parse("26714ff3-fdf7-40db-aa1d-27d36c537387"), "Address Postcode", p => p.LeadApplicantAddressPostcode);

                    // EDI

                    builder.Map<AgeRange>(Guid.Parse("dccdb566-aa72-46b5-b579-6762d74e8c35"), "Age Range", p => p.LeadApplicantAgeRangeId);
                    builder.Map<Gender>(Guid.Parse("39345a45-4d8c-45d7-9c72-3419ac153ba9"), "Gender", p => p.LeadApplicantGenderId);
                    builder.Map<Ethnicity>(Guid.Parse("6081b152-2353-4d0a-a534-c8413dbb9ea2"), "Ethnicity", p => p.LeadApplicantEthnicityId);

                    // Project Support

                    builder.Map<ProjectSupport>(Guid.Parse("ebf7b778-e2cf-4378-9eec-b6e14b649131"), String.Empty, async (p, b) =>
                    {
                        var repository = b.ServiceProvider.GetRequiredService<IProjectSupportRepository>();
                        var items = await repository.SelectProjectSupportByProjectId(p.Id);
                        return items.Select(p => p.Id).ToList();
                    }, 
                    action =>
                    {
                        action.Map<User>(Guid.Parse("b25d8fae-f426-4890-a47e-21752a5d28f9"), "Support Advisers", p => p.AdviserIds);
                        action.Map<SupportTeam>(Guid.Parse("7186191c-2128-497a-83d7-25240285b756"), "Support Teams", p => p.TeamIds);
                        action.Map<SupportProvided>(Guid.Parse("db47a4e5-84b3-411c-b4f4-4bca919fde2e"), "Support Provided", p => p.SupportProvidedIds);
                    });

                    _schema = builder.Schema;
                }

                return _schema;
            }
        }

        static ReportSchema? _schema = null;
    }
}
