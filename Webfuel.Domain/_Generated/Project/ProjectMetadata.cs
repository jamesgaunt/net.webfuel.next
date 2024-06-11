using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectMetadata: IRepositoryMetadata<Project>
    {
        // Data Access
        
        public static string DatabaseTable => "Project";
        
        public static string DefaultOrderBy => "ORDER BY Number DESC";
        
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
                    case nameof(Project.SupportRequestId):
                        result.Add(new SqlParameter(nameof(Project.SupportRequestId), entity.SupportRequestId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.ClosureDate):
                        result.Add(new SqlParameter(nameof(Project.ClosureDate), entity.ClosureDate ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.ClosureAttempted):
                        result.Add(new SqlParameter(nameof(Project.ClosureAttempted), entity.ClosureAttempted));
                        break;
                    case nameof(Project.SubmittedFundingStreamFreeText):
                        result.Add(new SqlParameter(nameof(Project.SubmittedFundingStreamFreeText), entity.SubmittedFundingStreamFreeText));
                        break;
                    case nameof(Project.SubmittedFundingStreamName):
                        result.Add(new SqlParameter(nameof(Project.SubmittedFundingStreamName), entity.SubmittedFundingStreamName));
                        break;
                    case nameof(Project.Locked):
                        result.Add(new SqlParameter(nameof(Project.Locked), entity.Locked));
                        break;
                    case nameof(Project.Discarded):
                        result.Add(new SqlParameter(nameof(Project.Discarded), entity.Discarded));
                        break;
                    case nameof(Project.RSSHubProvidingAdviceIds):
                        result.Add(new SqlParameter(nameof(Project.RSSHubProvidingAdviceIds), entity.RSSHubProvidingAdviceIdsJson));
                        break;
                    case nameof(Project.MonetaryValueOfFundingApplication):
                        result.Add(new SqlParameter(nameof(Project.MonetaryValueOfFundingApplication), entity.MonetaryValueOfFundingApplication ?? (object?)DBNull.Value));
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
                    case nameof(Project.SocialCare):
                        result.Add(new SqlParameter(nameof(Project.SocialCare), entity.SocialCare));
                        break;
                    case nameof(Project.PublicHealth):
                        result.Add(new SqlParameter(nameof(Project.PublicHealth), entity.PublicHealth));
                        break;
                    case nameof(Project.DateOfRequest):
                        result.Add(new SqlParameter(nameof(Project.DateOfRequest), entity.DateOfRequest));
                        break;
                    case nameof(Project.Title):
                        result.Add(new SqlParameter(nameof(Project.Title), entity.Title));
                        break;
                    case nameof(Project.ApplicationStageFreeText):
                        result.Add(new SqlParameter(nameof(Project.ApplicationStageFreeText), entity.ApplicationStageFreeText));
                        break;
                    case nameof(Project.ProposedFundingStreamName):
                        result.Add(new SqlParameter(nameof(Project.ProposedFundingStreamName), entity.ProposedFundingStreamName));
                        break;
                    case nameof(Project.NIHRApplicationId):
                        result.Add(new SqlParameter(nameof(Project.NIHRApplicationId), entity.NIHRApplicationId));
                        break;
                    case nameof(Project.TargetSubmissionDate):
                        result.Add(new SqlParameter(nameof(Project.TargetSubmissionDate), entity.TargetSubmissionDate ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.ExperienceOfResearchAwards):
                        result.Add(new SqlParameter(nameof(Project.ExperienceOfResearchAwards), entity.ExperienceOfResearchAwards));
                        break;
                    case nameof(Project.BriefDescription):
                        result.Add(new SqlParameter(nameof(Project.BriefDescription), entity.BriefDescription));
                        break;
                    case nameof(Project.SupportRequested):
                        result.Add(new SqlParameter(nameof(Project.SupportRequested), entity.SupportRequested));
                        break;
                    case nameof(Project.HowDidYouFindUsFreeText):
                        result.Add(new SqlParameter(nameof(Project.HowDidYouFindUsFreeText), entity.HowDidYouFindUsFreeText));
                        break;
                    case nameof(Project.WhoElseIsOnTheStudyTeam):
                        result.Add(new SqlParameter(nameof(Project.WhoElseIsOnTheStudyTeam), entity.WhoElseIsOnTheStudyTeam));
                        break;
                    case nameof(Project.IsCTUAlreadyInvolvedFreeText):
                        result.Add(new SqlParameter(nameof(Project.IsCTUAlreadyInvolvedFreeText), entity.IsCTUAlreadyInvolvedFreeText));
                        break;
                    case nameof(Project.ProfessionalBackgroundIds):
                        result.Add(new SqlParameter(nameof(Project.ProfessionalBackgroundIds), entity.ProfessionalBackgroundIdsJson));
                        break;
                    case nameof(Project.ProfessionalBackgroundFreeText):
                        result.Add(new SqlParameter(nameof(Project.ProfessionalBackgroundFreeText), entity.ProfessionalBackgroundFreeText));
                        break;
                    case nameof(Project.TeamContactTitle):
                        result.Add(new SqlParameter(nameof(Project.TeamContactTitle), entity.TeamContactTitle));
                        break;
                    case nameof(Project.TeamContactFirstName):
                        result.Add(new SqlParameter(nameof(Project.TeamContactFirstName), entity.TeamContactFirstName));
                        break;
                    case nameof(Project.TeamContactLastName):
                        result.Add(new SqlParameter(nameof(Project.TeamContactLastName), entity.TeamContactLastName));
                        break;
                    case nameof(Project.TeamContactEmail):
                        result.Add(new SqlParameter(nameof(Project.TeamContactEmail), entity.TeamContactEmail));
                        break;
                    case nameof(Project.TeamContactRoleFreeText):
                        result.Add(new SqlParameter(nameof(Project.TeamContactRoleFreeText), entity.TeamContactRoleFreeText));
                        break;
                    case nameof(Project.TeamContactMailingPermission):
                        result.Add(new SqlParameter(nameof(Project.TeamContactMailingPermission), entity.TeamContactMailingPermission));
                        break;
                    case nameof(Project.TeamContactPrivacyStatementRead):
                        result.Add(new SqlParameter(nameof(Project.TeamContactPrivacyStatementRead), entity.TeamContactPrivacyStatementRead));
                        break;
                    case nameof(Project.LeadApplicantTitle):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantTitle), entity.LeadApplicantTitle));
                        break;
                    case nameof(Project.LeadApplicantFirstName):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantFirstName), entity.LeadApplicantFirstName));
                        break;
                    case nameof(Project.LeadApplicantLastName):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantLastName), entity.LeadApplicantLastName));
                        break;
                    case nameof(Project.LeadApplicantEmail):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantEmail), entity.LeadApplicantEmail));
                        break;
                    case nameof(Project.LeadApplicantJobRole):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantJobRole), entity.LeadApplicantJobRole));
                        break;
                    case nameof(Project.LeadApplicantCareerStage):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantCareerStage), entity.LeadApplicantCareerStage));
                        break;
                    case nameof(Project.LeadApplicantOrganisation):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantOrganisation), entity.LeadApplicantOrganisation));
                        break;
                    case nameof(Project.LeadApplicantDepartment):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantDepartment), entity.LeadApplicantDepartment));
                        break;
                    case nameof(Project.LeadApplicantAddressLine1):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantAddressLine1), entity.LeadApplicantAddressLine1));
                        break;
                    case nameof(Project.LeadApplicantAddressLine2):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantAddressLine2), entity.LeadApplicantAddressLine2));
                        break;
                    case nameof(Project.LeadApplicantAddressTown):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantAddressTown), entity.LeadApplicantAddressTown));
                        break;
                    case nameof(Project.LeadApplicantAddressCounty):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantAddressCounty), entity.LeadApplicantAddressCounty));
                        break;
                    case nameof(Project.LeadApplicantAddressCountry):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantAddressCountry), entity.LeadApplicantAddressCountry));
                        break;
                    case nameof(Project.LeadApplicantAddressPostcode):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantAddressPostcode), entity.LeadApplicantAddressPostcode));
                        break;
                    case nameof(Project.LeadApplicantORCID):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantORCID), entity.LeadApplicantORCID));
                        break;
                    case nameof(Project.DiagnosticCount):
                        result.Add(new SqlParameter(nameof(Project.DiagnosticCount), entity.DiagnosticCount));
                        break;
                    case nameof(Project.DiagnosticList):
                        result.Add(new SqlParameter(nameof(Project.DiagnosticList), entity.DiagnosticListJson));
                        break;
                    case nameof(Project.TeamContactFullName):
                        result.Add(new SqlParameter(nameof(Project.TeamContactFullName), entity.TeamContactFullName));
                        break;
                    case nameof(Project.LeadApplicantFullName):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantFullName), entity.LeadApplicantFullName));
                        break;
                    case nameof(Project.SupportTotalMinutes):
                        result.Add(new SqlParameter(nameof(Project.SupportTotalMinutes), entity.SupportTotalMinutes));
                        break;
                    case nameof(Project.OpenSupportRequestTeamIds):
                        result.Add(new SqlParameter(nameof(Project.OpenSupportRequestTeamIds), entity.OpenSupportRequestTeamIdsJson));
                        break;
                    case nameof(Project.CreatedAt):
                        result.Add(new SqlParameter(nameof(Project.CreatedAt), entity.CreatedAt));
                        break;
                    case nameof(Project.FileStorageGroupId):
                        result.Add(new SqlParameter(nameof(Project.FileStorageGroupId), entity.FileStorageGroupId));
                        break;
                    case nameof(Project.LeadAdviserUserId):
                        result.Add(new SqlParameter(nameof(Project.LeadAdviserUserId), entity.LeadAdviserUserId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.SubmittedFundingStreamId):
                        result.Add(new SqlParameter(nameof(Project.SubmittedFundingStreamId), entity.SubmittedFundingStreamId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.StatusId):
                        result.Add(new SqlParameter(nameof(Project.StatusId), entity.StatusId));
                        break;
                    case nameof(Project.WillStudyUseCTUId):
                        result.Add(new SqlParameter(nameof(Project.WillStudyUseCTUId), entity.WillStudyUseCTUId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.IsPaidRSSAdviserLeadId):
                        result.Add(new SqlParameter(nameof(Project.IsPaidRSSAdviserLeadId), entity.IsPaidRSSAdviserLeadId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.IsPaidRSSAdviserCoapplicantId):
                        result.Add(new SqlParameter(nameof(Project.IsPaidRSSAdviserCoapplicantId), entity.IsPaidRSSAdviserCoapplicantId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.IsInternationalMultiSiteStudyId):
                        result.Add(new SqlParameter(nameof(Project.IsInternationalMultiSiteStudyId), entity.IsInternationalMultiSiteStudyId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.IsFellowshipId):
                        result.Add(new SqlParameter(nameof(Project.IsFellowshipId), entity.IsFellowshipId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.ApplicationStageId):
                        result.Add(new SqlParameter(nameof(Project.ApplicationStageId), entity.ApplicationStageId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.ProposedFundingCallTypeId):
                        result.Add(new SqlParameter(nameof(Project.ProposedFundingCallTypeId), entity.ProposedFundingCallTypeId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.ProposedFundingStreamId):
                        result.Add(new SqlParameter(nameof(Project.ProposedFundingStreamId), entity.ProposedFundingStreamId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.IsTeamMembersConsultedId):
                        result.Add(new SqlParameter(nameof(Project.IsTeamMembersConsultedId), entity.IsTeamMembersConsultedId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.IsResubmissionId):
                        result.Add(new SqlParameter(nameof(Project.IsResubmissionId), entity.IsResubmissionId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.HowDidYouFindUsId):
                        result.Add(new SqlParameter(nameof(Project.HowDidYouFindUsId), entity.HowDidYouFindUsId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.IsCTUAlreadyInvolvedId):
                        result.Add(new SqlParameter(nameof(Project.IsCTUAlreadyInvolvedId), entity.IsCTUAlreadyInvolvedId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.TeamContactRoleId):
                        result.Add(new SqlParameter(nameof(Project.TeamContactRoleId), entity.TeamContactRoleId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.LeadApplicantCareerStageId):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantCareerStageId), entity.LeadApplicantCareerStageId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.LeadApplicantOrganisationTypeId):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantOrganisationTypeId), entity.LeadApplicantOrganisationTypeId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.LeadApplicantLocationId):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantLocationId), entity.LeadApplicantLocationId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.IsLeadApplicantNHSId):
                        result.Add(new SqlParameter(nameof(Project.IsLeadApplicantNHSId), entity.IsLeadApplicantNHSId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.LeadApplicantAgeRangeId):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantAgeRangeId), entity.LeadApplicantAgeRangeId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.LeadApplicantGenderId):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantGenderId), entity.LeadApplicantGenderId ?? (object?)DBNull.Value));
                        break;
                    case nameof(Project.LeadApplicantEthnicityId):
                        result.Add(new SqlParameter(nameof(Project.LeadApplicantEthnicityId), entity.LeadApplicantEthnicityId ?? (object?)DBNull.Value));
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
                yield return "SupportRequestId";
                yield return "ClosureDate";
                yield return "ClosureAttempted";
                yield return "SubmittedFundingStreamFreeText";
                yield return "SubmittedFundingStreamName";
                yield return "Locked";
                yield return "Discarded";
                yield return "RSSHubProvidingAdviceIds";
                yield return "MonetaryValueOfFundingApplication";
                yield return "ProjectStartDate";
                yield return "RecruitmentTarget";
                yield return "NumberOfProjectSites";
                yield return "SocialCare";
                yield return "PublicHealth";
                yield return "DateOfRequest";
                yield return "Title";
                yield return "ApplicationStageFreeText";
                yield return "ProposedFundingStreamName";
                yield return "NIHRApplicationId";
                yield return "TargetSubmissionDate";
                yield return "ExperienceOfResearchAwards";
                yield return "BriefDescription";
                yield return "SupportRequested";
                yield return "HowDidYouFindUsFreeText";
                yield return "WhoElseIsOnTheStudyTeam";
                yield return "IsCTUAlreadyInvolvedFreeText";
                yield return "ProfessionalBackgroundIds";
                yield return "ProfessionalBackgroundFreeText";
                yield return "TeamContactTitle";
                yield return "TeamContactFirstName";
                yield return "TeamContactLastName";
                yield return "TeamContactEmail";
                yield return "TeamContactRoleFreeText";
                yield return "TeamContactMailingPermission";
                yield return "TeamContactPrivacyStatementRead";
                yield return "LeadApplicantTitle";
                yield return "LeadApplicantFirstName";
                yield return "LeadApplicantLastName";
                yield return "LeadApplicantEmail";
                yield return "LeadApplicantJobRole";
                yield return "LeadApplicantCareerStage";
                yield return "LeadApplicantOrganisation";
                yield return "LeadApplicantDepartment";
                yield return "LeadApplicantAddressLine1";
                yield return "LeadApplicantAddressLine2";
                yield return "LeadApplicantAddressTown";
                yield return "LeadApplicantAddressCounty";
                yield return "LeadApplicantAddressCountry";
                yield return "LeadApplicantAddressPostcode";
                yield return "LeadApplicantORCID";
                yield return "DiagnosticCount";
                yield return "DiagnosticList";
                yield return "TeamContactFullName";
                yield return "LeadApplicantFullName";
                yield return "SupportTotalMinutes";
                yield return "OpenSupportRequestTeamIds";
                yield return "CreatedAt";
                yield return "FileStorageGroupId";
                yield return "LeadAdviserUserId";
                yield return "SubmittedFundingStreamId";
                yield return "StatusId";
                yield return "WillStudyUseCTUId";
                yield return "IsPaidRSSAdviserLeadId";
                yield return "IsPaidRSSAdviserCoapplicantId";
                yield return "IsInternationalMultiSiteStudyId";
                yield return "IsFellowshipId";
                yield return "ApplicationStageId";
                yield return "ProposedFundingCallTypeId";
                yield return "ProposedFundingStreamId";
                yield return "IsTeamMembersConsultedId";
                yield return "IsResubmissionId";
                yield return "HowDidYouFindUsId";
                yield return "IsCTUAlreadyInvolvedId";
                yield return "TeamContactRoleId";
                yield return "LeadApplicantCareerStageId";
                yield return "LeadApplicantOrganisationTypeId";
                yield return "LeadApplicantLocationId";
                yield return "IsLeadApplicantNHSId";
                yield return "LeadApplicantAgeRangeId";
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
                yield return "SupportRequestId";
                yield return "ClosureDate";
                yield return "ClosureAttempted";
                yield return "SubmittedFundingStreamFreeText";
                yield return "SubmittedFundingStreamName";
                yield return "Locked";
                yield return "Discarded";
                yield return "RSSHubProvidingAdviceIds";
                yield return "MonetaryValueOfFundingApplication";
                yield return "ProjectStartDate";
                yield return "RecruitmentTarget";
                yield return "NumberOfProjectSites";
                yield return "SocialCare";
                yield return "PublicHealth";
                yield return "DateOfRequest";
                yield return "Title";
                yield return "ApplicationStageFreeText";
                yield return "ProposedFundingStreamName";
                yield return "NIHRApplicationId";
                yield return "TargetSubmissionDate";
                yield return "ExperienceOfResearchAwards";
                yield return "BriefDescription";
                yield return "SupportRequested";
                yield return "HowDidYouFindUsFreeText";
                yield return "WhoElseIsOnTheStudyTeam";
                yield return "IsCTUAlreadyInvolvedFreeText";
                yield return "ProfessionalBackgroundIds";
                yield return "ProfessionalBackgroundFreeText";
                yield return "TeamContactTitle";
                yield return "TeamContactFirstName";
                yield return "TeamContactLastName";
                yield return "TeamContactEmail";
                yield return "TeamContactRoleFreeText";
                yield return "TeamContactMailingPermission";
                yield return "TeamContactPrivacyStatementRead";
                yield return "LeadApplicantTitle";
                yield return "LeadApplicantFirstName";
                yield return "LeadApplicantLastName";
                yield return "LeadApplicantEmail";
                yield return "LeadApplicantJobRole";
                yield return "LeadApplicantCareerStage";
                yield return "LeadApplicantOrganisation";
                yield return "LeadApplicantDepartment";
                yield return "LeadApplicantAddressLine1";
                yield return "LeadApplicantAddressLine2";
                yield return "LeadApplicantAddressTown";
                yield return "LeadApplicantAddressCounty";
                yield return "LeadApplicantAddressCountry";
                yield return "LeadApplicantAddressPostcode";
                yield return "LeadApplicantORCID";
                yield return "DiagnosticCount";
                yield return "DiagnosticList";
                yield return "TeamContactFullName";
                yield return "LeadApplicantFullName";
                yield return "SupportTotalMinutes";
                yield return "OpenSupportRequestTeamIds";
                yield return "CreatedAt";
                yield return "FileStorageGroupId";
                yield return "LeadAdviserUserId";
                yield return "SubmittedFundingStreamId";
                yield return "StatusId";
                yield return "WillStudyUseCTUId";
                yield return "IsPaidRSSAdviserLeadId";
                yield return "IsPaidRSSAdviserCoapplicantId";
                yield return "IsInternationalMultiSiteStudyId";
                yield return "IsFellowshipId";
                yield return "ApplicationStageId";
                yield return "ProposedFundingCallTypeId";
                yield return "ProposedFundingStreamId";
                yield return "IsTeamMembersConsultedId";
                yield return "IsResubmissionId";
                yield return "HowDidYouFindUsId";
                yield return "IsCTUAlreadyInvolvedId";
                yield return "TeamContactRoleId";
                yield return "LeadApplicantCareerStageId";
                yield return "LeadApplicantOrganisationTypeId";
                yield return "LeadApplicantLocationId";
                yield return "IsLeadApplicantNHSId";
                yield return "LeadApplicantAgeRangeId";
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
                yield return "SupportRequestId";
                yield return "ClosureDate";
                yield return "ClosureAttempted";
                yield return "SubmittedFundingStreamFreeText";
                yield return "SubmittedFundingStreamName";
                yield return "Locked";
                yield return "Discarded";
                yield return "RSSHubProvidingAdviceIds";
                yield return "MonetaryValueOfFundingApplication";
                yield return "ProjectStartDate";
                yield return "RecruitmentTarget";
                yield return "NumberOfProjectSites";
                yield return "SocialCare";
                yield return "PublicHealth";
                yield return "DateOfRequest";
                yield return "Title";
                yield return "ApplicationStageFreeText";
                yield return "ProposedFundingStreamName";
                yield return "NIHRApplicationId";
                yield return "TargetSubmissionDate";
                yield return "ExperienceOfResearchAwards";
                yield return "BriefDescription";
                yield return "SupportRequested";
                yield return "HowDidYouFindUsFreeText";
                yield return "WhoElseIsOnTheStudyTeam";
                yield return "IsCTUAlreadyInvolvedFreeText";
                yield return "ProfessionalBackgroundIds";
                yield return "ProfessionalBackgroundFreeText";
                yield return "TeamContactTitle";
                yield return "TeamContactFirstName";
                yield return "TeamContactLastName";
                yield return "TeamContactEmail";
                yield return "TeamContactRoleFreeText";
                yield return "TeamContactMailingPermission";
                yield return "TeamContactPrivacyStatementRead";
                yield return "LeadApplicantTitle";
                yield return "LeadApplicantFirstName";
                yield return "LeadApplicantLastName";
                yield return "LeadApplicantEmail";
                yield return "LeadApplicantJobRole";
                yield return "LeadApplicantCareerStage";
                yield return "LeadApplicantOrganisation";
                yield return "LeadApplicantDepartment";
                yield return "LeadApplicantAddressLine1";
                yield return "LeadApplicantAddressLine2";
                yield return "LeadApplicantAddressTown";
                yield return "LeadApplicantAddressCounty";
                yield return "LeadApplicantAddressCountry";
                yield return "LeadApplicantAddressPostcode";
                yield return "LeadApplicantORCID";
                yield return "DiagnosticCount";
                yield return "DiagnosticList";
                yield return "TeamContactFullName";
                yield return "LeadApplicantFullName";
                yield return "SupportTotalMinutes";
                yield return "OpenSupportRequestTeamIds";
                yield return "CreatedAt";
                yield return "FileStorageGroupId";
                yield return "LeadAdviserUserId";
                yield return "SubmittedFundingStreamId";
                yield return "StatusId";
                yield return "WillStudyUseCTUId";
                yield return "IsPaidRSSAdviserLeadId";
                yield return "IsPaidRSSAdviserCoapplicantId";
                yield return "IsInternationalMultiSiteStudyId";
                yield return "IsFellowshipId";
                yield return "ApplicationStageId";
                yield return "ProposedFundingCallTypeId";
                yield return "ProposedFundingStreamId";
                yield return "IsTeamMembersConsultedId";
                yield return "IsResubmissionId";
                yield return "HowDidYouFindUsId";
                yield return "IsCTUAlreadyInvolvedId";
                yield return "TeamContactRoleId";
                yield return "LeadApplicantCareerStageId";
                yield return "LeadApplicantOrganisationTypeId";
                yield return "LeadApplicantLocationId";
                yield return "IsLeadApplicantNHSId";
                yield return "LeadApplicantAgeRangeId";
                yield return "LeadApplicantGenderId";
                yield return "LeadApplicantEthnicityId";
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
            entity.ApplicationStageFreeText = entity.ApplicationStageFreeText ?? String.Empty;
            entity.ApplicationStageFreeText = entity.ApplicationStageFreeText.Trim();
            entity.ProposedFundingStreamName = entity.ProposedFundingStreamName ?? String.Empty;
            entity.ProposedFundingStreamName = entity.ProposedFundingStreamName.Trim();
            entity.NIHRApplicationId = entity.NIHRApplicationId ?? String.Empty;
            entity.NIHRApplicationId = entity.NIHRApplicationId.Trim();
            entity.ExperienceOfResearchAwards = entity.ExperienceOfResearchAwards ?? String.Empty;
            entity.ExperienceOfResearchAwards = entity.ExperienceOfResearchAwards.Trim();
            entity.BriefDescription = entity.BriefDescription ?? String.Empty;
            entity.BriefDescription = entity.BriefDescription.Trim();
            entity.SupportRequested = entity.SupportRequested ?? String.Empty;
            entity.SupportRequested = entity.SupportRequested.Trim();
            entity.HowDidYouFindUsFreeText = entity.HowDidYouFindUsFreeText ?? String.Empty;
            entity.HowDidYouFindUsFreeText = entity.HowDidYouFindUsFreeText.Trim();
            entity.WhoElseIsOnTheStudyTeam = entity.WhoElseIsOnTheStudyTeam ?? String.Empty;
            entity.WhoElseIsOnTheStudyTeam = entity.WhoElseIsOnTheStudyTeam.Trim();
            entity.IsCTUAlreadyInvolvedFreeText = entity.IsCTUAlreadyInvolvedFreeText ?? String.Empty;
            entity.IsCTUAlreadyInvolvedFreeText = entity.IsCTUAlreadyInvolvedFreeText.Trim();
            entity.ProfessionalBackgroundFreeText = entity.ProfessionalBackgroundFreeText ?? String.Empty;
            entity.ProfessionalBackgroundFreeText = entity.ProfessionalBackgroundFreeText.Trim();
            entity.TeamContactTitle = entity.TeamContactTitle ?? String.Empty;
            entity.TeamContactTitle = entity.TeamContactTitle.Trim();
            entity.TeamContactFirstName = entity.TeamContactFirstName ?? String.Empty;
            entity.TeamContactFirstName = entity.TeamContactFirstName.Trim();
            entity.TeamContactLastName = entity.TeamContactLastName ?? String.Empty;
            entity.TeamContactLastName = entity.TeamContactLastName.Trim();
            entity.TeamContactEmail = entity.TeamContactEmail ?? String.Empty;
            entity.TeamContactEmail = entity.TeamContactEmail.Trim();
            entity.TeamContactRoleFreeText = entity.TeamContactRoleFreeText ?? String.Empty;
            entity.TeamContactRoleFreeText = entity.TeamContactRoleFreeText.Trim();
            entity.LeadApplicantTitle = entity.LeadApplicantTitle ?? String.Empty;
            entity.LeadApplicantTitle = entity.LeadApplicantTitle.Trim();
            entity.LeadApplicantFirstName = entity.LeadApplicantFirstName ?? String.Empty;
            entity.LeadApplicantFirstName = entity.LeadApplicantFirstName.Trim();
            entity.LeadApplicantLastName = entity.LeadApplicantLastName ?? String.Empty;
            entity.LeadApplicantLastName = entity.LeadApplicantLastName.Trim();
            entity.LeadApplicantEmail = entity.LeadApplicantEmail ?? String.Empty;
            entity.LeadApplicantEmail = entity.LeadApplicantEmail.Trim();
            entity.LeadApplicantJobRole = entity.LeadApplicantJobRole ?? String.Empty;
            entity.LeadApplicantJobRole = entity.LeadApplicantJobRole.Trim();
            entity.LeadApplicantCareerStage = entity.LeadApplicantCareerStage ?? String.Empty;
            entity.LeadApplicantCareerStage = entity.LeadApplicantCareerStage.Trim();
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
            entity.TeamContactFullName = entity.TeamContactFullName ?? String.Empty;
            entity.TeamContactFullName = entity.TeamContactFullName.Trim();
            entity.LeadApplicantFullName = entity.LeadApplicantFullName ?? String.Empty;
            entity.LeadApplicantFullName = entity.LeadApplicantFullName.Trim();
            Validator.ValidateAndThrow(entity);
        }
        
        public static ProjectRepositoryValidator Validator { get; } = new ProjectRepositoryValidator();
        
        public const int PrefixedNumber_MaxLength = 128;
        public const int SubmittedFundingStreamFreeText_MaxLength = 128;
        public const int SubmittedFundingStreamName_MaxLength = 128;
        public const int Title_MaxLength = 1000;
        public const int ApplicationStageFreeText_MaxLength = 128;
        public const int ProposedFundingStreamName_MaxLength = 128;
        public const int NIHRApplicationId_MaxLength = 128;
        public const int ExperienceOfResearchAwards_MaxLength = 1000;
        public const int BriefDescription_MaxLength = 5000;
        public const int SupportRequested_MaxLength = 2000;
        public const int HowDidYouFindUsFreeText_MaxLength = 128;
        public const int WhoElseIsOnTheStudyTeam_MaxLength = 2000;
        public const int IsCTUAlreadyInvolvedFreeText_MaxLength = 128;
        public const int ProfessionalBackgroundFreeText_MaxLength = 128;
        public const int TeamContactTitle_MaxLength = 128;
        public const int TeamContactFirstName_MaxLength = 128;
        public const int TeamContactLastName_MaxLength = 128;
        public const int TeamContactEmail_MaxLength = 128;
        public const int TeamContactRoleFreeText_MaxLength = 128;
        public const int LeadApplicantTitle_MaxLength = 128;
        public const int LeadApplicantFirstName_MaxLength = 128;
        public const int LeadApplicantLastName_MaxLength = 128;
        public const int LeadApplicantEmail_MaxLength = 128;
        public const int LeadApplicantJobRole_MaxLength = 128;
        public const int LeadApplicantCareerStage_MaxLength = 128;
        public const int LeadApplicantOrganisation_MaxLength = 128;
        public const int LeadApplicantDepartment_MaxLength = 128;
        public const int LeadApplicantAddressLine1_MaxLength = 128;
        public const int LeadApplicantAddressLine2_MaxLength = 128;
        public const int LeadApplicantAddressTown_MaxLength = 128;
        public const int LeadApplicantAddressCounty_MaxLength = 128;
        public const int LeadApplicantAddressCountry_MaxLength = 128;
        public const int LeadApplicantAddressPostcode_MaxLength = 128;
        public const int LeadApplicantORCID_MaxLength = 128;
        public const int TeamContactFullName_MaxLength = 128;
        public const int LeadApplicantFullName_MaxLength = 128;
        
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
        
        public static void ApplicationStageFreeText_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(ApplicationStageFreeText_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void ProposedFundingStreamName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(ProposedFundingStreamName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void NIHRApplicationId_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(NIHRApplicationId_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
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
        
        public static void HowDidYouFindUsFreeText_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(HowDidYouFindUsFreeText_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void WhoElseIsOnTheStudyTeam_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(WhoElseIsOnTheStudyTeam_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void IsCTUAlreadyInvolvedFreeText_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(IsCTUAlreadyInvolvedFreeText_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void ProfessionalBackgroundFreeText_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(ProfessionalBackgroundFreeText_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
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
        
        public static void TeamContactRoleFreeText_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(TeamContactRoleFreeText_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
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
        
        public static void LeadApplicantEmail_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantEmail_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LeadApplicantJobRole_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantJobRole_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LeadApplicantCareerStage_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantCareerStage_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
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
        
        public static void TeamContactFullName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(TeamContactFullName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
        
        public static void LeadApplicantFullName_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(LeadApplicantFullName_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
        }
    }
    
    public partial class ProjectRepositoryValidator: AbstractValidator<Project>
    {
        public ProjectRepositoryValidator()
        {
            RuleFor(x => x.PrefixedNumber).Use(ProjectMetadata.PrefixedNumber_ValidationRules);
            RuleFor(x => x.SubmittedFundingStreamFreeText).Use(ProjectMetadata.SubmittedFundingStreamFreeText_ValidationRules);
            RuleFor(x => x.SubmittedFundingStreamName).Use(ProjectMetadata.SubmittedFundingStreamName_ValidationRules);
            RuleFor(x => x.Title).Use(ProjectMetadata.Title_ValidationRules);
            RuleFor(x => x.ApplicationStageFreeText).Use(ProjectMetadata.ApplicationStageFreeText_ValidationRules);
            RuleFor(x => x.ProposedFundingStreamName).Use(ProjectMetadata.ProposedFundingStreamName_ValidationRules);
            RuleFor(x => x.NIHRApplicationId).Use(ProjectMetadata.NIHRApplicationId_ValidationRules);
            RuleFor(x => x.ExperienceOfResearchAwards).Use(ProjectMetadata.ExperienceOfResearchAwards_ValidationRules);
            RuleFor(x => x.BriefDescription).Use(ProjectMetadata.BriefDescription_ValidationRules);
            RuleFor(x => x.SupportRequested).Use(ProjectMetadata.SupportRequested_ValidationRules);
            RuleFor(x => x.HowDidYouFindUsFreeText).Use(ProjectMetadata.HowDidYouFindUsFreeText_ValidationRules);
            RuleFor(x => x.WhoElseIsOnTheStudyTeam).Use(ProjectMetadata.WhoElseIsOnTheStudyTeam_ValidationRules);
            RuleFor(x => x.IsCTUAlreadyInvolvedFreeText).Use(ProjectMetadata.IsCTUAlreadyInvolvedFreeText_ValidationRules);
            RuleFor(x => x.ProfessionalBackgroundFreeText).Use(ProjectMetadata.ProfessionalBackgroundFreeText_ValidationRules);
            RuleFor(x => x.TeamContactTitle).Use(ProjectMetadata.TeamContactTitle_ValidationRules);
            RuleFor(x => x.TeamContactFirstName).Use(ProjectMetadata.TeamContactFirstName_ValidationRules);
            RuleFor(x => x.TeamContactLastName).Use(ProjectMetadata.TeamContactLastName_ValidationRules);
            RuleFor(x => x.TeamContactEmail).Use(ProjectMetadata.TeamContactEmail_ValidationRules);
            RuleFor(x => x.TeamContactRoleFreeText).Use(ProjectMetadata.TeamContactRoleFreeText_ValidationRules);
            RuleFor(x => x.LeadApplicantTitle).Use(ProjectMetadata.LeadApplicantTitle_ValidationRules);
            RuleFor(x => x.LeadApplicantFirstName).Use(ProjectMetadata.LeadApplicantFirstName_ValidationRules);
            RuleFor(x => x.LeadApplicantLastName).Use(ProjectMetadata.LeadApplicantLastName_ValidationRules);
            RuleFor(x => x.LeadApplicantEmail).Use(ProjectMetadata.LeadApplicantEmail_ValidationRules);
            RuleFor(x => x.LeadApplicantJobRole).Use(ProjectMetadata.LeadApplicantJobRole_ValidationRules);
            RuleFor(x => x.LeadApplicantCareerStage).Use(ProjectMetadata.LeadApplicantCareerStage_ValidationRules);
            RuleFor(x => x.LeadApplicantOrganisation).Use(ProjectMetadata.LeadApplicantOrganisation_ValidationRules);
            RuleFor(x => x.LeadApplicantDepartment).Use(ProjectMetadata.LeadApplicantDepartment_ValidationRules);
            RuleFor(x => x.LeadApplicantAddressLine1).Use(ProjectMetadata.LeadApplicantAddressLine1_ValidationRules);
            RuleFor(x => x.LeadApplicantAddressLine2).Use(ProjectMetadata.LeadApplicantAddressLine2_ValidationRules);
            RuleFor(x => x.LeadApplicantAddressTown).Use(ProjectMetadata.LeadApplicantAddressTown_ValidationRules);
            RuleFor(x => x.LeadApplicantAddressCounty).Use(ProjectMetadata.LeadApplicantAddressCounty_ValidationRules);
            RuleFor(x => x.LeadApplicantAddressCountry).Use(ProjectMetadata.LeadApplicantAddressCountry_ValidationRules);
            RuleFor(x => x.LeadApplicantAddressPostcode).Use(ProjectMetadata.LeadApplicantAddressPostcode_ValidationRules);
            RuleFor(x => x.LeadApplicantORCID).Use(ProjectMetadata.LeadApplicantORCID_ValidationRules);
            RuleFor(x => x.TeamContactFullName).Use(ProjectMetadata.TeamContactFullName_ValidationRules);
            RuleFor(x => x.LeadApplicantFullName).Use(ProjectMetadata.LeadApplicantFullName_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

