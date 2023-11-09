using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class SupportRequestMetadata: IRepositoryMetadata<SupportRequest>
    {
        // Data Access
        
        public static string DatabaseTable => "SupportRequest";
        
        public static string DefaultOrderBy => "ORDER BY Number DESC";
        
        public static SupportRequest DataReader(SqlDataReader dr) => new SupportRequest(dr);
        
        public static List<SqlParameter> ExtractParameters(SupportRequest entity, IEnumerable<string> properties)
        {
            var result = new List<SqlParameter> { new SqlParameter(nameof(SupportRequest.Id), entity.Id) };
            foreach(var property in properties)
            {
                switch (property)
                {
                    case nameof(SupportRequest.Id):
                        break;
                    case nameof(SupportRequest.Number):
                        result.Add(new SqlParameter(nameof(SupportRequest.Number), entity.Number));
                        break;
                    case nameof(SupportRequest.PrefixedNumber):
                        result.Add(new SqlParameter(nameof(SupportRequest.PrefixedNumber), entity.PrefixedNumber));
                        break;
                    case nameof(SupportRequest.DateOfRequest):
                        result.Add(new SqlParameter(nameof(SupportRequest.DateOfRequest), entity.DateOfRequest));
                        break;
                    case nameof(SupportRequest.Title):
                        result.Add(new SqlParameter(nameof(SupportRequest.Title), entity.Title));
                        break;
                    case nameof(SupportRequest.ProposedFundingStreamName):
                        result.Add(new SqlParameter(nameof(SupportRequest.ProposedFundingStreamName), entity.ProposedFundingStreamName));
                        break;
                    case nameof(SupportRequest.TargetSubmissionDate):
                        result.Add(new SqlParameter(nameof(SupportRequest.TargetSubmissionDate), entity.TargetSubmissionDate ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.ExperienceOfResearchAwards):
                        result.Add(new SqlParameter(nameof(SupportRequest.ExperienceOfResearchAwards), entity.ExperienceOfResearchAwards));
                        break;
                    case nameof(SupportRequest.BriefDescription):
                        result.Add(new SqlParameter(nameof(SupportRequest.BriefDescription), entity.BriefDescription));
                        break;
                    case nameof(SupportRequest.SupportRequested):
                        result.Add(new SqlParameter(nameof(SupportRequest.SupportRequested), entity.SupportRequested));
                        break;
                    case nameof(SupportRequest.TeamContactTitle):
                        result.Add(new SqlParameter(nameof(SupportRequest.TeamContactTitle), entity.TeamContactTitle));
                        break;
                    case nameof(SupportRequest.TeamContactFirstName):
                        result.Add(new SqlParameter(nameof(SupportRequest.TeamContactFirstName), entity.TeamContactFirstName));
                        break;
                    case nameof(SupportRequest.TeamContactLastName):
                        result.Add(new SqlParameter(nameof(SupportRequest.TeamContactLastName), entity.TeamContactLastName));
                        break;
                    case nameof(SupportRequest.TeamContactEmail):
                        result.Add(new SqlParameter(nameof(SupportRequest.TeamContactEmail), entity.TeamContactEmail));
                        break;
                    case nameof(SupportRequest.TeamContactMailingPermission):
                        result.Add(new SqlParameter(nameof(SupportRequest.TeamContactMailingPermission), entity.TeamContactMailingPermission));
                        break;
                    case nameof(SupportRequest.TeamContactPrivacyStatementRead):
                        result.Add(new SqlParameter(nameof(SupportRequest.TeamContactPrivacyStatementRead), entity.TeamContactPrivacyStatementRead));
                        break;
                    case nameof(SupportRequest.LeadApplicantTitle):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantTitle), entity.LeadApplicantTitle));
                        break;
                    case nameof(SupportRequest.LeadApplicantFirstName):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantFirstName), entity.LeadApplicantFirstName));
                        break;
                    case nameof(SupportRequest.LeadApplicantLastName):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantLastName), entity.LeadApplicantLastName));
                        break;
                    case nameof(SupportRequest.LeadApplicantJobRole):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantJobRole), entity.LeadApplicantJobRole));
                        break;
                    case nameof(SupportRequest.LeadApplicantOrganisation):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantOrganisation), entity.LeadApplicantOrganisation));
                        break;
                    case nameof(SupportRequest.LeadApplicantDepartment):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantDepartment), entity.LeadApplicantDepartment));
                        break;
                    case nameof(SupportRequest.LeadApplicantAddressLine1):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantAddressLine1), entity.LeadApplicantAddressLine1));
                        break;
                    case nameof(SupportRequest.LeadApplicantAddressLine2):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantAddressLine2), entity.LeadApplicantAddressLine2));
                        break;
                    case nameof(SupportRequest.LeadApplicantAddressTown):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantAddressTown), entity.LeadApplicantAddressTown));
                        break;
                    case nameof(SupportRequest.LeadApplicantAddressCounty):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantAddressCounty), entity.LeadApplicantAddressCounty));
                        break;
                    case nameof(SupportRequest.LeadApplicantAddressCountry):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantAddressCountry), entity.LeadApplicantAddressCountry));
                        break;
                    case nameof(SupportRequest.LeadApplicantAddressPostcode):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantAddressPostcode), entity.LeadApplicantAddressPostcode));
                        break;
                    case nameof(SupportRequest.LeadApplicantORCID):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantORCID), entity.LeadApplicantORCID));
                        break;
                    case nameof(SupportRequest.ProjectId):
                        result.Add(new SqlParameter(nameof(SupportRequest.ProjectId), entity.ProjectId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.CreatedAt):
                        result.Add(new SqlParameter(nameof(SupportRequest.CreatedAt), entity.CreatedAt));
                        break;
                    case nameof(SupportRequest.StatusId):
                        result.Add(new SqlParameter(nameof(SupportRequest.StatusId), entity.StatusId));
                        break;
                    case nameof(SupportRequest.FileStorageGroupId):
                        result.Add(new SqlParameter(nameof(SupportRequest.FileStorageGroupId), entity.FileStorageGroupId));
                        break;
                    case nameof(SupportRequest.IsFellowshipId):
                        result.Add(new SqlParameter(nameof(SupportRequest.IsFellowshipId), entity.IsFellowshipId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.ApplicationStageId):
                        result.Add(new SqlParameter(nameof(SupportRequest.ApplicationStageId), entity.ApplicationStageId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.ProposedFundingCallTypeId):
                        result.Add(new SqlParameter(nameof(SupportRequest.ProposedFundingCallTypeId), entity.ProposedFundingCallTypeId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.ProposedFundingStreamId):
                        result.Add(new SqlParameter(nameof(SupportRequest.ProposedFundingStreamId), entity.ProposedFundingStreamId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.IsTeamMembersConsultedId):
                        result.Add(new SqlParameter(nameof(SupportRequest.IsTeamMembersConsultedId), entity.IsTeamMembersConsultedId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.IsResubmissionId):
                        result.Add(new SqlParameter(nameof(SupportRequest.IsResubmissionId), entity.IsResubmissionId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.HowDidYouFindUsId):
                        result.Add(new SqlParameter(nameof(SupportRequest.HowDidYouFindUsId), entity.HowDidYouFindUsId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.TeamContactRoleId):
                        result.Add(new SqlParameter(nameof(SupportRequest.TeamContactRoleId), entity.TeamContactRoleId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.LeadApplicantOrganisationTypeId):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantOrganisationTypeId), entity.LeadApplicantOrganisationTypeId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.IsLeadApplicantNHSId):
                        result.Add(new SqlParameter(nameof(SupportRequest.IsLeadApplicantNHSId), entity.IsLeadApplicantNHSId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.LeadApplicantAgeRangeId):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantAgeRangeId), entity.LeadApplicantAgeRangeId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.LeadApplicantDisabilityId):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantDisabilityId), entity.LeadApplicantDisabilityId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.LeadApplicantGenderId):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantGenderId), entity.LeadApplicantGenderId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.LeadApplicantEthnicityId):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantEthnicityId), entity.LeadApplicantEthnicityId ?? (object?)DBNull.Value));
                        break;
                }
            }
            return result;
        }
        
        public static string InsertSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? InsertProperties;
            return RepositoryMetadataDefaults.InsertSQL<SupportRequest, SupportRequestMetadata>(properties);
        }
        
        public static string UpdateSQL(IEnumerable<string>? properties = null)
        {
            properties = properties ?? UpdateProperties;
            return RepositoryMetadataDefaults.UpdateSQL<SupportRequest, SupportRequestMetadata>(properties);
        }
        
        public static string DeleteSQL()
        {
            return RepositoryMetadataDefaults.DeleteSQL<SupportRequest, SupportRequestMetadata>();
        }
        
        public static IEnumerable<string> SelectProperties
        {
            get
            {
                yield return "Id";
                yield return "Number";
                yield return "PrefixedNumber";
                yield return "DateOfRequest";
                yield return "Title";
                yield return "ProposedFundingStreamName";
                yield return "TargetSubmissionDate";
                yield return "ExperienceOfResearchAwards";
                yield return "BriefDescription";
                yield return "SupportRequested";
                yield return "TeamContactTitle";
                yield return "TeamContactFirstName";
                yield return "TeamContactLastName";
                yield return "TeamContactEmail";
                yield return "TeamContactMailingPermission";
                yield return "TeamContactPrivacyStatementRead";
                yield return "LeadApplicantTitle";
                yield return "LeadApplicantFirstName";
                yield return "LeadApplicantLastName";
                yield return "LeadApplicantJobRole";
                yield return "LeadApplicantOrganisation";
                yield return "LeadApplicantDepartment";
                yield return "LeadApplicantAddressLine1";
                yield return "LeadApplicantAddressLine2";
                yield return "LeadApplicantAddressTown";
                yield return "LeadApplicantAddressCounty";
                yield return "LeadApplicantAddressCountry";
                yield return "LeadApplicantAddressPostcode";
                yield return "LeadApplicantORCID";
                yield return "ProjectId";
                yield return "CreatedAt";
                yield return "StatusId";
                yield return "FileStorageGroupId";
                yield return "IsFellowshipId";
                yield return "ApplicationStageId";
                yield return "ProposedFundingCallTypeId";
                yield return "ProposedFundingStreamId";
                yield return "IsTeamMembersConsultedId";
                yield return "IsResubmissionId";
                yield return "HowDidYouFindUsId";
                yield return "TeamContactRoleId";
                yield return "LeadApplicantOrganisationTypeId";
                yield return "IsLeadApplicantNHSId";
                yield return "LeadApplicantAgeRangeId";
                yield return "LeadApplicantDisabilityId";
                yield return "LeadApplicantGenderId";
                yield return "LeadApplicantEthnicityId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Number";
                yield return "PrefixedNumber";
                yield return "DateOfRequest";
                yield return "Title";
                yield return "ProposedFundingStreamName";
                yield return "TargetSubmissionDate";
                yield return "ExperienceOfResearchAwards";
                yield return "BriefDescription";
                yield return "SupportRequested";
                yield return "TeamContactTitle";
                yield return "TeamContactFirstName";
                yield return "TeamContactLastName";
                yield return "TeamContactEmail";
                yield return "TeamContactMailingPermission";
                yield return "TeamContactPrivacyStatementRead";
                yield return "LeadApplicantTitle";
                yield return "LeadApplicantFirstName";
                yield return "LeadApplicantLastName";
                yield return "LeadApplicantJobRole";
                yield return "LeadApplicantOrganisation";
                yield return "LeadApplicantDepartment";
                yield return "LeadApplicantAddressLine1";
                yield return "LeadApplicantAddressLine2";
                yield return "LeadApplicantAddressTown";
                yield return "LeadApplicantAddressCounty";
                yield return "LeadApplicantAddressCountry";
                yield return "LeadApplicantAddressPostcode";
                yield return "LeadApplicantORCID";
                yield return "ProjectId";
                yield return "CreatedAt";
                yield return "StatusId";
                yield return "FileStorageGroupId";
                yield return "IsFellowshipId";
                yield return "ApplicationStageId";
                yield return "ProposedFundingCallTypeId";
                yield return "ProposedFundingStreamId";
                yield return "IsTeamMembersConsultedId";
                yield return "IsResubmissionId";
                yield return "HowDidYouFindUsId";
                yield return "TeamContactRoleId";
                yield return "LeadApplicantOrganisationTypeId";
                yield return "IsLeadApplicantNHSId";
                yield return "LeadApplicantAgeRangeId";
                yield return "LeadApplicantDisabilityId";
                yield return "LeadApplicantGenderId";
                yield return "LeadApplicantEthnicityId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Number";
                yield return "PrefixedNumber";
                yield return "DateOfRequest";
                yield return "Title";
                yield return "ProposedFundingStreamName";
                yield return "TargetSubmissionDate";
                yield return "ExperienceOfResearchAwards";
                yield return "BriefDescription";
                yield return "SupportRequested";
                yield return "TeamContactTitle";
                yield return "TeamContactFirstName";
                yield return "TeamContactLastName";
                yield return "TeamContactEmail";
                yield return "TeamContactMailingPermission";
                yield return "TeamContactPrivacyStatementRead";
                yield return "LeadApplicantTitle";
                yield return "LeadApplicantFirstName";
                yield return "LeadApplicantLastName";
                yield return "LeadApplicantJobRole";
                yield return "LeadApplicantOrganisation";
                yield return "LeadApplicantDepartment";
                yield return "LeadApplicantAddressLine1";
                yield return "LeadApplicantAddressLine2";
                yield return "LeadApplicantAddressTown";
                yield return "LeadApplicantAddressCounty";
                yield return "LeadApplicantAddressCountry";
                yield return "LeadApplicantAddressPostcode";
                yield return "LeadApplicantORCID";
                yield return "ProjectId";
                yield return "CreatedAt";
                yield return "StatusId";
                yield return "FileStorageGroupId";
                yield return "IsFellowshipId";
                yield return "ApplicationStageId";
                yield return "ProposedFundingCallTypeId";
                yield return "ProposedFundingStreamId";
                yield return "IsTeamMembersConsultedId";
                yield return "IsResubmissionId";
                yield return "HowDidYouFindUsId";
                yield return "TeamContactRoleId";
                yield return "LeadApplicantOrganisationTypeId";
                yield return "IsLeadApplicantNHSId";
                yield return "LeadApplicantAgeRangeId";
                yield return "LeadApplicantDisabilityId";
                yield return "LeadApplicantGenderId";
                yield return "LeadApplicantEthnicityId";
            }
        }
        
        // Validation
        
        public static void Validate(SupportRequest entity)
        {
            entity.PrefixedNumber = entity.PrefixedNumber ?? String.Empty;
            entity.PrefixedNumber = entity.PrefixedNumber.Trim();
            entity.Title = entity.Title ?? String.Empty;
            entity.Title = entity.Title.Trim();
            entity.ProposedFundingStreamName = entity.ProposedFundingStreamName ?? String.Empty;
            entity.ProposedFundingStreamName = entity.ProposedFundingStreamName.Trim();
            entity.ExperienceOfResearchAwards = entity.ExperienceOfResearchAwards ?? String.Empty;
            entity.ExperienceOfResearchAwards = entity.ExperienceOfResearchAwards.Trim();
            entity.BriefDescription = entity.BriefDescription ?? String.Empty;
            entity.BriefDescription = entity.BriefDescription.Trim();
            entity.SupportRequested = entity.SupportRequested ?? String.Empty;
            entity.SupportRequested = entity.SupportRequested.Trim();
            entity.TeamContactTitle = entity.TeamContactTitle ?? String.Empty;
            entity.TeamContactTitle = entity.TeamContactTitle.Trim();
            entity.TeamContactFirstName = entity.TeamContactFirstName ?? String.Empty;
            entity.TeamContactFirstName = entity.TeamContactFirstName.Trim();
            entity.TeamContactLastName = entity.TeamContactLastName ?? String.Empty;
            entity.TeamContactLastName = entity.TeamContactLastName.Trim();
            entity.TeamContactEmail = entity.TeamContactEmail ?? String.Empty;
            entity.TeamContactEmail = entity.TeamContactEmail.Trim();
            entity.LeadApplicantTitle = entity.LeadApplicantTitle ?? String.Empty;
            entity.LeadApplicantTitle = entity.LeadApplicantTitle.Trim();
            entity.LeadApplicantFirstName = entity.LeadApplicantFirstName ?? String.Empty;
            entity.LeadApplicantFirstName = entity.LeadApplicantFirstName.Trim();
            entity.LeadApplicantLastName = entity.LeadApplicantLastName ?? String.Empty;
            entity.LeadApplicantLastName = entity.LeadApplicantLastName.Trim();
            entity.LeadApplicantJobRole = entity.LeadApplicantJobRole ?? String.Empty;
            entity.LeadApplicantJobRole = entity.LeadApplicantJobRole.Trim();
            entity.LeadApplicantOrganisation = entity.LeadApplicantOrganisation ?? String.Empty;
            entity.LeadApplicantOrganisation = entity.LeadApplicantOrganisation.Trim();
            entity.LeadApplicantDepartment = entity.LeadApplicantDepartment ?? String.Empty;
            entity.LeadApplicantDepartment = entity.LeadApplicantDepartment.Trim();
            entity.LeadApplicantAddressLine1 = entity.LeadApplicantAddressLine1 ?? String.Empty;
            entity.LeadApplicantAddressLine1 = entity.LeadApplicantAddressLine1.Trim();
            entity.LeadApplicantAddressLine2 = entity.LeadApplicantAddressLine2 ?? String.Empty;
            entity.LeadApplicantAddressLine2 = entity.LeadApplicantAddressLine2.Trim();
            entity.LeadApplicantAddressTown = entity.LeadApplicantAddressTown ?? String.Empty;
            entity.LeadApplicantAddressTown = entity.LeadApplicantAddressTown.Trim();
            entity.LeadApplicantAddressCounty = entity.LeadApplicantAddressCounty ?? String.Empty;
            entity.LeadApplicantAddressCounty = entity.LeadApplicantAddressCounty.Trim();
            entity.LeadApplicantAddressCountry = entity.LeadApplicantAddressCountry ?? String.Empty;
            entity.LeadApplicantAddressCountry = entity.LeadApplicantAddressCountry.Trim();
            entity.LeadApplicantAddressPostcode = entity.LeadApplicantAddressPostcode ?? String.Empty;
            entity.LeadApplicantAddressPostcode = entity.LeadApplicantAddressPostcode.Trim();
            entity.LeadApplicantORCID = entity.LeadApplicantORCID ?? String.Empty;
            entity.LeadApplicantORCID = entity.LeadApplicantORCID.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static SupportRequestRepositoryValidator Validator { get; } = new SupportRequestRepositoryValidator();
        
        public const int PrefixedNumber_MaxLength = 64;
        public const int Title_MaxLength = 1000;
        public const int ProposedFundingStreamName_MaxLength = 64;
        public const int ExperienceOfResearchAwards_MaxLength = 1000;
        public const int BriefDescription_MaxLength = 5000;
        public const int SupportRequested_MaxLength = 2000;
        public const int TeamContactTitle_MaxLength = 64;
        public const int TeamContactFirstName_MaxLength = 64;
        public const int TeamContactLastName_MaxLength = 64;
        public const int TeamContactEmail_MaxLength = 64;
        public const int LeadApplicantTitle_MaxLength = 64;
        public const int LeadApplicantFirstName_MaxLength = 64;
        public const int LeadApplicantLastName_MaxLength = 64;
        public const int LeadApplicantJobRole_MaxLength = 64;
        public const int LeadApplicantOrganisation_MaxLength = 64;
        public const int LeadApplicantDepartment_MaxLength = 64;
        public const int LeadApplicantAddressLine1_MaxLength = 64;
        public const int LeadApplicantAddressLine2_MaxLength = 64;
        public const int LeadApplicantAddressTown_MaxLength = 64;
        public const int LeadApplicantAddressCounty_MaxLength = 64;
        public const int LeadApplicantAddressCountry_MaxLength = 64;
        public const int LeadApplicantAddressPostcode_MaxLength = 64;
        public const int LeadApplicantORCID_MaxLength = 64;
        
        public static void PrefixedNumber_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(PrefixedNumber_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void Title_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(Title_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void ProposedFundingStreamName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(ProposedFundingStreamName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void ExperienceOfResearchAwards_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(ExperienceOfResearchAwards_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void BriefDescription_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(BriefDescription_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void SupportRequested_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(SupportRequested_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void TeamContactTitle_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(TeamContactTitle_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void TeamContactFirstName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(TeamContactFirstName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void TeamContactLastName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(TeamContactLastName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void TeamContactEmail_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(TeamContactEmail_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LeadApplicantTitle_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantTitle_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LeadApplicantFirstName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantFirstName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LeadApplicantLastName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantLastName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LeadApplicantJobRole_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantJobRole_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LeadApplicantOrganisation_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantOrganisation_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LeadApplicantDepartment_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantDepartment_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LeadApplicantAddressLine1_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantAddressLine1_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LeadApplicantAddressLine2_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantAddressLine2_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LeadApplicantAddressTown_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantAddressTown_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LeadApplicantAddressCounty_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantAddressCounty_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LeadApplicantAddressCountry_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantAddressCountry_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LeadApplicantAddressPostcode_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantAddressPostcode_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LeadApplicantORCID_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantORCID_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public class SupportRequestRepositoryValidator: AbstractValidator<SupportRequest>
        {
            public SupportRequestRepositoryValidator()
            {
                RuleFor(x => x.PrefixedNumber).Use(PrefixedNumber_ValidationRules);
                RuleFor(x => x.Title).Use(Title_ValidationRules);
                RuleFor(x => x.ProposedFundingStreamName).Use(ProposedFundingStreamName_ValidationRules);
                RuleFor(x => x.ExperienceOfResearchAwards).Use(ExperienceOfResearchAwards_ValidationRules);
                RuleFor(x => x.BriefDescription).Use(BriefDescription_ValidationRules);
                RuleFor(x => x.SupportRequested).Use(SupportRequested_ValidationRules);
                RuleFor(x => x.TeamContactTitle).Use(TeamContactTitle_ValidationRules);
                RuleFor(x => x.TeamContactFirstName).Use(TeamContactFirstName_ValidationRules);
                RuleFor(x => x.TeamContactLastName).Use(TeamContactLastName_ValidationRules);
                RuleFor(x => x.TeamContactEmail).Use(TeamContactEmail_ValidationRules);
                RuleFor(x => x.LeadApplicantTitle).Use(LeadApplicantTitle_ValidationRules);
                RuleFor(x => x.LeadApplicantFirstName).Use(LeadApplicantFirstName_ValidationRules);
                RuleFor(x => x.LeadApplicantLastName).Use(LeadApplicantLastName_ValidationRules);
                RuleFor(x => x.LeadApplicantJobRole).Use(LeadApplicantJobRole_ValidationRules);
                RuleFor(x => x.LeadApplicantOrganisation).Use(LeadApplicantOrganisation_ValidationRules);
                RuleFor(x => x.LeadApplicantDepartment).Use(LeadApplicantDepartment_ValidationRules);
                RuleFor(x => x.LeadApplicantAddressLine1).Use(LeadApplicantAddressLine1_ValidationRules);
                RuleFor(x => x.LeadApplicantAddressLine2).Use(LeadApplicantAddressLine2_ValidationRules);
                RuleFor(x => x.LeadApplicantAddressTown).Use(LeadApplicantAddressTown_ValidationRules);
                RuleFor(x => x.LeadApplicantAddressCounty).Use(LeadApplicantAddressCounty_ValidationRules);
                RuleFor(x => x.LeadApplicantAddressCountry).Use(LeadApplicantAddressCountry_ValidationRules);
                RuleFor(x => x.LeadApplicantAddressPostcode).Use(LeadApplicantAddressPostcode_ValidationRules);
                RuleFor(x => x.LeadApplicantORCID).Use(LeadApplicantORCID_ValidationRules);
            }
        }
    }
}

