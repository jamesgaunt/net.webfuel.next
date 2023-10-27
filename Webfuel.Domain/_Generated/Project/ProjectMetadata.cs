using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectMetadata: IRepositoryMetadata<Project>
    {
        // Data Access
        
        public static string DatabaseTable => "Project";
        
        public static string DefaultOrderBy => "ORDER BY Id ASC";
        
        public static Project DataReader(SqlDataReader dr) => new Project(dr);
        
        public static List<SqlParameter> ExtractParameters(Project entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(Project.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(Project.Id):
                        break;
                    case nameof(Project.Number):
                        result.Add(new SqlParameter(nameof(Project.Number), entity.Number));
                        break;
                    case nameof(Project.PrefixedNumber):
                        result.Add(new SqlParameter(nameof(Project.PrefixedNumber), entity.PrefixedNumber));
                        break;
                    case nameof(Project.ClosureDate):
                        result.Add(new SqlParameter(nameof(Project.ClosureDate), entity.ClosureDate ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.NIHRRSSMemberCollaboratorIds):
                        result.Add(new SqlParameter(nameof(Project.NIHRRSSMemberCollaboratorIds), entity.NIHRRSSMemberCollaboratorIdsJson));
                        break;
                    case nameof(Project.SubmittedFundingStreamFreeText):
                        result.Add(new SqlParameter(nameof(Project.SubmittedFundingStreamFreeText), entity.SubmittedFundingStreamFreeText));
                        break;
                    case nameof(Project.SubmittedFundingStreamName):
                        result.Add(new SqlParameter(nameof(Project.SubmittedFundingStreamName), entity.SubmittedFundingStreamName));
                        break;
                    case nameof(Project.DateOfRequest):
                        result.Add(new SqlParameter(nameof(Project.DateOfRequest), entity.DateOfRequest));
                        break;
                    case nameof(Project.Title):
                        result.Add(new SqlParameter(nameof(Project.Title), entity.Title));
                        break;
                    case nameof(Project.BriefDescription):
                        result.Add(new SqlParameter(nameof(Project.BriefDescription), entity.BriefDescription));
                        break;
                    case nameof(Project.FundingStreamName):
                        result.Add(new SqlParameter(nameof(Project.FundingStreamName), entity.FundingStreamName));
                        break;
                    case nameof(Project.TargetSubmissionDate):
                        result.Add(new SqlParameter(nameof(Project.TargetSubmissionDate), entity.TargetSubmissionDate ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.ExperienceOfResearchAwards):
                        result.Add(new SqlParameter(nameof(Project.ExperienceOfResearchAwards), entity.ExperienceOfResearchAwards));
                        break;
                    case nameof(Project.SupportRequested):
                        result.Add(new SqlParameter(nameof(Project.SupportRequested), entity.SupportRequested));
                        break;
                    case nameof(Project.ProjectStartDate):
                        result.Add(new SqlParameter(nameof(Project.ProjectStartDate), entity.ProjectStartDate ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.RecruitmentTarget):
                        result.Add(new SqlParameter(nameof(Project.RecruitmentTarget), entity.RecruitmentTarget ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.NumberOfProjectSites):
                        result.Add(new SqlParameter(nameof(Project.NumberOfProjectSites), entity.NumberOfProjectSites ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.CreatedAt):
                        result.Add(new SqlParameter(nameof(Project.CreatedAt), entity.CreatedAt));
                        break;
                    case nameof(Project.StatusId):
                        result.Add(new SqlParameter(nameof(Project.StatusId), entity.StatusId));
                        break;
                    case nameof(Project.IsQuantativeTeamContributionId):
                        result.Add(new SqlParameter(nameof(Project.IsQuantativeTeamContributionId), entity.IsQuantativeTeamContributionId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.IsCTUTeamContributionId):
                        result.Add(new SqlParameter(nameof(Project.IsCTUTeamContributionId), entity.IsCTUTeamContributionId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.IsPPIEAndEDIContributionId):
                        result.Add(new SqlParameter(nameof(Project.IsPPIEAndEDIContributionId), entity.IsPPIEAndEDIContributionId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.SubmittedFundingStreamId):
                        result.Add(new SqlParameter(nameof(Project.SubmittedFundingStreamId), entity.SubmittedFundingStreamId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.SupportRequestId):
                        result.Add(new SqlParameter(nameof(Project.SupportRequestId), entity.SupportRequestId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.ApplicationStageId):
                        result.Add(new SqlParameter(nameof(Project.ApplicationStageId), entity.ApplicationStageId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.FundingStreamId):
                        result.Add(new SqlParameter(nameof(Project.FundingStreamId), entity.FundingStreamId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.FundingCallTypeId):
                        result.Add(new SqlParameter(nameof(Project.FundingCallTypeId), entity.FundingCallTypeId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.IsFellowshipId):
                        result.Add(new SqlParameter(nameof(Project.IsFellowshipId), entity.IsFellowshipId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.IsTeamMembersConsultedId):
                        result.Add(new SqlParameter(nameof(Project.IsTeamMembersConsultedId), entity.IsTeamMembersConsultedId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.IsResubmissionId):
                        result.Add(new SqlParameter(nameof(Project.IsResubmissionId), entity.IsResubmissionId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.IsLeadApplicantNHSId):
                        result.Add(new SqlParameter(nameof(Project.IsLeadApplicantNHSId), entity.IsLeadApplicantNHSId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.HowDidYouFindUsId):
                        result.Add(new SqlParameter(nameof(Project.HowDidYouFindUsId), entity.HowDidYouFindUsId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.IsInternationalMultiSiteStudyId):
                        result.Add(new SqlParameter(nameof(Project.IsInternationalMultiSiteStudyId), entity.IsInternationalMultiSiteStudyId ?? (object?)DBNull.Value));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<Project, ProjectMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<Project, ProjectMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<Project, ProjectMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Number";
                yield return "PrefixedNumber";
                yield return "ClosureDate";
                yield return "NIHRRSSMemberCollaboratorIds";
                yield return "SubmittedFundingStreamFreeText";
                yield return "SubmittedFundingStreamName";
                yield return "DateOfRequest";
                yield return "Title";
                yield return "BriefDescription";
                yield return "FundingStreamName";
                yield return "TargetSubmissionDate";
                yield return "ExperienceOfResearchAwards";
                yield return "SupportRequested";
                yield return "ProjectStartDate";
                yield return "RecruitmentTarget";
                yield return "NumberOfProjectSites";
                yield return "CreatedAt";
                yield return "StatusId";
                yield return "IsQuantativeTeamContributionId";
                yield return "IsCTUTeamContributionId";
                yield return "IsPPIEAndEDIContributionId";
                yield return "SubmittedFundingStreamId";
                yield return "SupportRequestId";
                yield return "ApplicationStageId";
                yield return "FundingStreamId";
                yield return "FundingCallTypeId";
                yield return "IsFellowshipId";
                yield return "IsTeamMembersConsultedId";
                yield return "IsResubmissionId";
                yield return "IsLeadApplicantNHSId";
                yield return "HowDidYouFindUsId";
                yield return "IsInternationalMultiSiteStudyId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Number";
                yield return "PrefixedNumber";
                yield return "ClosureDate";
                yield return "NIHRRSSMemberCollaboratorIds";
                yield return "SubmittedFundingStreamFreeText";
                yield return "SubmittedFundingStreamName";
                yield return "DateOfRequest";
                yield return "Title";
                yield return "BriefDescription";
                yield return "FundingStreamName";
                yield return "TargetSubmissionDate";
                yield return "ExperienceOfResearchAwards";
                yield return "SupportRequested";
                yield return "ProjectStartDate";
                yield return "RecruitmentTarget";
                yield return "NumberOfProjectSites";
                yield return "CreatedAt";
                yield return "StatusId";
                yield return "IsQuantativeTeamContributionId";
                yield return "IsCTUTeamContributionId";
                yield return "IsPPIEAndEDIContributionId";
                yield return "SubmittedFundingStreamId";
                yield return "SupportRequestId";
                yield return "ApplicationStageId";
                yield return "FundingStreamId";
                yield return "FundingCallTypeId";
                yield return "IsFellowshipId";
                yield return "IsTeamMembersConsultedId";
                yield return "IsResubmissionId";
                yield return "IsLeadApplicantNHSId";
                yield return "HowDidYouFindUsId";
                yield return "IsInternationalMultiSiteStudyId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Number";
                yield return "PrefixedNumber";
                yield return "ClosureDate";
                yield return "NIHRRSSMemberCollaboratorIds";
                yield return "SubmittedFundingStreamFreeText";
                yield return "SubmittedFundingStreamName";
                yield return "DateOfRequest";
                yield return "Title";
                yield return "BriefDescription";
                yield return "FundingStreamName";
                yield return "TargetSubmissionDate";
                yield return "ExperienceOfResearchAwards";
                yield return "SupportRequested";
                yield return "ProjectStartDate";
                yield return "RecruitmentTarget";
                yield return "NumberOfProjectSites";
                yield return "CreatedAt";
                yield return "StatusId";
                yield return "IsQuantativeTeamContributionId";
                yield return "IsCTUTeamContributionId";
                yield return "IsPPIEAndEDIContributionId";
                yield return "SubmittedFundingStreamId";
                yield return "SupportRequestId";
                yield return "ApplicationStageId";
                yield return "FundingStreamId";
                yield return "FundingCallTypeId";
                yield return "IsFellowshipId";
                yield return "IsTeamMembersConsultedId";
                yield return "IsResubmissionId";
                yield return "IsLeadApplicantNHSId";
                yield return "HowDidYouFindUsId";
                yield return "IsInternationalMultiSiteStudyId";
            }
        }
        
        // Validation
        
        public static void Validate(Project entity)
        {
            entity.PrefixedNumber = entity.PrefixedNumber ?? String.Empty;
            entity.PrefixedNumber = entity.PrefixedNumber.Trim();
            entity.SubmittedFundingStreamFreeText = entity.SubmittedFundingStreamFreeText ?? String.Empty;
            entity.SubmittedFundingStreamFreeText = entity.SubmittedFundingStreamFreeText.Trim();
            entity.SubmittedFundingStreamName = entity.SubmittedFundingStreamName ?? String.Empty;
            entity.SubmittedFundingStreamName = entity.SubmittedFundingStreamName.Trim();
            entity.Title = entity.Title ?? String.Empty;
            entity.Title = entity.Title.Trim();
            entity.BriefDescription = entity.BriefDescription ?? String.Empty;
            entity.BriefDescription = entity.BriefDescription.Trim();
            entity.FundingStreamName = entity.FundingStreamName ?? String.Empty;
            entity.FundingStreamName = entity.FundingStreamName.Trim();
            entity.ExperienceOfResearchAwards = entity.ExperienceOfResearchAwards ?? String.Empty;
            entity.ExperienceOfResearchAwards = entity.ExperienceOfResearchAwards.Trim();
            entity.SupportRequested = entity.SupportRequested ?? String.Empty;
            entity.SupportRequested = entity.SupportRequested.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ProjectRepositoryValidator Validator { get; } = new ProjectRepositoryValidator();
        
        public const int PrefixedNumber_MaxLength = 64;
        public const int SubmittedFundingStreamFreeText_MaxLength = 64;
        public const int SubmittedFundingStreamName_MaxLength = 64;
        public const int Title_MaxLength = 1000;
        public const int BriefDescription_MaxLength = 5000;
        public const int FundingStreamName_MaxLength = 64;
        public const int ExperienceOfResearchAwards_MaxLength = 1000;
        public const int SupportRequested_MaxLength = 2000;
        
        public static void PrefixedNumber_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(PrefixedNumber_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void SubmittedFundingStreamFreeText_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(SubmittedFundingStreamFreeText_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void SubmittedFundingStreamName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(SubmittedFundingStreamName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Title_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Title_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void BriefDescription_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(BriefDescription_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void FundingStreamName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(FundingStreamName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void ExperienceOfResearchAwards_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(ExperienceOfResearchAwards_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void SupportRequested_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(SupportRequested_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class ProjectRepositoryValidator: AbstractValidator<Project>
        {
            public ProjectRepositoryValidator()
            {
                RuleFor(x => x.PrefixedNumber).Use(PrefixedNumber_ValidationRules);
                RuleFor(x => x.SubmittedFundingStreamFreeText).Use(SubmittedFundingStreamFreeText_ValidationRules);
                RuleFor(x => x.SubmittedFundingStreamName).Use(SubmittedFundingStreamName_ValidationRules);
                RuleFor(x => x.Title).Use(Title_ValidationRules);
                RuleFor(x => x.BriefDescription).Use(BriefDescription_ValidationRules);
                RuleFor(x => x.FundingStreamName).Use(FundingStreamName_ValidationRules);
                RuleFor(x => x.ExperienceOfResearchAwards).Use(ExperienceOfResearchAwards_ValidationRules);
                RuleFor(x => x.SupportRequested).Use(SupportRequested_ValidationRules);
            }
        }
    }
}

