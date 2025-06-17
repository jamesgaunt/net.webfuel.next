using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class SupportRequestMetadata: IRepositoryMetadata<SupportRequest>
    {
        // Data Access
        
        public static string DatabaseTable => "SupportRequest";
        
        public static string DefaultOrderBy => "ORDER BY Id DESC";
        
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
                    case nameof(SupportRequest.TriageNote):
                        result.Add(new SqlParameter(nameof(SupportRequest.TriageNote), entity.TriageNote));
                        break;
                    case nameof(SupportRequest.IsThisRequestLinkedToAnExistingProject):
                        result.Add(new SqlParameter(nameof(SupportRequest.IsThisRequestLinkedToAnExistingProject), entity.IsThisRequestLinkedToAnExistingProject));
                        break;
                    case nameof(SupportRequest.DateOfTriage):
                        result.Add(new SqlParameter(nameof(SupportRequest.DateOfTriage), entity.DateOfTriage ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.DateOfRequest):
                        result.Add(new SqlParameter(nameof(SupportRequest.DateOfRequest), entity.DateOfRequest));
                        break;
                    case nameof(SupportRequest.Title):
                        result.Add(new SqlParameter(nameof(SupportRequest.Title), entity.Title));
                        break;
                    case nameof(SupportRequest.ApplicationStageFreeText):
                        result.Add(new SqlParameter(nameof(SupportRequest.ApplicationStageFreeText), entity.ApplicationStageFreeText));
                        break;
                    case nameof(SupportRequest.ProposedFundingStreamName):
                        result.Add(new SqlParameter(nameof(SupportRequest.ProposedFundingStreamName), entity.ProposedFundingStreamName));
                        break;
                    case nameof(SupportRequest.NIHRApplicationId):
                        result.Add(new SqlParameter(nameof(SupportRequest.NIHRApplicationId), entity.NIHRApplicationId));
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
                    case nameof(SupportRequest.HowDidYouFindUsFreeText):
                        result.Add(new SqlParameter(nameof(SupportRequest.HowDidYouFindUsFreeText), entity.HowDidYouFindUsFreeText));
                        break;
                    case nameof(SupportRequest.WhoElseIsOnTheStudyTeam):
                        result.Add(new SqlParameter(nameof(SupportRequest.WhoElseIsOnTheStudyTeam), entity.WhoElseIsOnTheStudyTeam));
                        break;
                    case nameof(SupportRequest.IsCTUAlreadyInvolvedFreeText):
                        result.Add(new SqlParameter(nameof(SupportRequest.IsCTUAlreadyInvolvedFreeText), entity.IsCTUAlreadyInvolvedFreeText));
                        break;
                    case nameof(SupportRequest.ProfessionalBackgroundIds):
                        result.Add(new SqlParameter(nameof(SupportRequest.ProfessionalBackgroundIds), entity.ProfessionalBackgroundIdsJson));
                        break;
                    case nameof(SupportRequest.ProfessionalBackgroundFreeText):
                        result.Add(new SqlParameter(nameof(SupportRequest.ProfessionalBackgroundFreeText), entity.ProfessionalBackgroundFreeText));
                        break;
                    case nameof(SupportRequest.IsRoundRobinEnquiry):
                        result.Add(new SqlParameter(nameof(SupportRequest.IsRoundRobinEnquiry), entity.IsRoundRobinEnquiry));
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
                    case nameof(SupportRequest.TeamContactAltEmail):
                        result.Add(new SqlParameter(nameof(SupportRequest.TeamContactAltEmail), entity.TeamContactAltEmail));
                        break;
                    case nameof(SupportRequest.TeamContactRoleFreeText):
                        result.Add(new SqlParameter(nameof(SupportRequest.TeamContactRoleFreeText), entity.TeamContactRoleFreeText));
                        break;
                    case nameof(SupportRequest.TeamContactMailingPermission):
                        result.Add(new SqlParameter(nameof(SupportRequest.TeamContactMailingPermission), entity.TeamContactMailingPermission));
                        break;
                    case nameof(SupportRequest.TeamContactPrivacyStatementRead):
                        result.Add(new SqlParameter(nameof(SupportRequest.TeamContactPrivacyStatementRead), entity.TeamContactPrivacyStatementRead));
                        break;
                    case nameof(SupportRequest.TeamContactServiceAgreementRead):
                        result.Add(new SqlParameter(nameof(SupportRequest.TeamContactServiceAgreementRead), entity.TeamContactServiceAgreementRead));
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
                    case nameof(SupportRequest.LeadApplicantEmail):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantEmail), entity.LeadApplicantEmail));
                        break;
                    case nameof(SupportRequest.LeadApplicantJobRole):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantJobRole), entity.LeadApplicantJobRole));
                        break;
                    case nameof(SupportRequest.LeadApplicantCareerStage):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantCareerStage), entity.LeadApplicantCareerStage));
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
                    case nameof(SupportRequest.TeamContactFullName):
                        result.Add(new SqlParameter(nameof(SupportRequest.TeamContactFullName), entity.TeamContactFullName));
                        break;
                    case nameof(SupportRequest.LeadApplicantFullName):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantFullName), entity.LeadApplicantFullName));
                        break;
                    case nameof(SupportRequest.ProjectId):
                        result.Add(new SqlParameter(nameof(SupportRequest.ProjectId), entity.ProjectId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.CreatedAt):
                        result.Add(new SqlParameter(nameof(SupportRequest.CreatedAt), entity.CreatedAt));
                        break;
                    case nameof(SupportRequest.FileStorageGroupId):
                        result.Add(new SqlParameter(nameof(SupportRequest.FileStorageGroupId), entity.FileStorageGroupId));
                        break;
                    case nameof(SupportRequest.ProjectSupportGroupId):
                        result.Add(new SqlParameter(nameof(SupportRequest.ProjectSupportGroupId), entity.ProjectSupportGroupId));
                        break;
                    case nameof(SupportRequest.StatusId):
                        result.Add(new SqlParameter(nameof(SupportRequest.StatusId), entity.StatusId));
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
                    case nameof(SupportRequest.IsCTUAlreadyInvolvedId):
                        result.Add(new SqlParameter(nameof(SupportRequest.IsCTUAlreadyInvolvedId), entity.IsCTUAlreadyInvolvedId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.TeamContactRoleId):
                        result.Add(new SqlParameter(nameof(SupportRequest.TeamContactRoleId), entity.TeamContactRoleId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.LeadApplicantCareerStageId):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantCareerStageId), entity.LeadApplicantCareerStageId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.LeadApplicantOrganisationTypeId):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantOrganisationTypeId), entity.LeadApplicantOrganisationTypeId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.LeadApplicantLocationId):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantLocationId), entity.LeadApplicantLocationId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.IsLeadApplicantNHSId):
                        result.Add(new SqlParameter(nameof(SupportRequest.IsLeadApplicantNHSId), entity.IsLeadApplicantNHSId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.LeadApplicantAgeRangeId):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantAgeRangeId), entity.LeadApplicantAgeRangeId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.LeadApplicantGenderId):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantGenderId), entity.LeadApplicantGenderId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.LeadApplicantEthnicityId):
                        result.Add(new SqlParameter(nameof(SupportRequest.LeadApplicantEthnicityId), entity.LeadApplicantEthnicityId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.WouldYouLikeToReceiveAGrantsmanshipReviewId):
                        result.Add(new SqlParameter(nameof(SupportRequest.WouldYouLikeToReceiveAGrantsmanshipReviewId), entity.WouldYouLikeToReceiveAGrantsmanshipReviewId ?? (object?)DBNull.Value));
                        break;
                    case nameof(SupportRequest.IsYourSupportRequestOnlyForAGrantsmanshipReviewId):
                        result.Add(new SqlParameter(nameof(SupportRequest.IsYourSupportRequestOnlyForAGrantsmanshipReviewId), entity.IsYourSupportRequestOnlyForAGrantsmanshipReviewId ?? (object?)DBNull.Value));
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
                yield return "TriageNote";
                yield return "IsThisRequestLinkedToAnExistingProject";
                yield return "DateOfTriage";
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
                yield return "IsRoundRobinEnquiry";
                yield return "TeamContactTitle";
                yield return "TeamContactFirstName";
                yield return "TeamContactLastName";
                yield return "TeamContactEmail";
                yield return "TeamContactAltEmail";
                yield return "TeamContactRoleFreeText";
                yield return "TeamContactMailingPermission";
                yield return "TeamContactPrivacyStatementRead";
                yield return "TeamContactServiceAgreementRead";
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
                yield return "TeamContactFullName";
                yield return "LeadApplicantFullName";
                yield return "ProjectId";
                yield return "CreatedAt";
                yield return "FileStorageGroupId";
                yield return "ProjectSupportGroupId";
                yield return "StatusId";
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
                yield return "WouldYouLikeToReceiveAGrantsmanshipReviewId";
                yield return "IsYourSupportRequestOnlyForAGrantsmanshipReviewId";
            }
        }
        
        public static IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "TriageNote";
                yield return "IsThisRequestLinkedToAnExistingProject";
                yield return "DateOfTriage";
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
                yield return "IsRoundRobinEnquiry";
                yield return "TeamContactTitle";
                yield return "TeamContactFirstName";
                yield return "TeamContactLastName";
                yield return "TeamContactEmail";
                yield return "TeamContactAltEmail";
                yield return "TeamContactRoleFreeText";
                yield return "TeamContactMailingPermission";
                yield return "TeamContactPrivacyStatementRead";
                yield return "TeamContactServiceAgreementRead";
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
                yield return "TeamContactFullName";
                yield return "LeadApplicantFullName";
                yield return "ProjectId";
                yield return "CreatedAt";
                yield return "FileStorageGroupId";
                yield return "ProjectSupportGroupId";
                yield return "StatusId";
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
                yield return "WouldYouLikeToReceiveAGrantsmanshipReviewId";
                yield return "IsYourSupportRequestOnlyForAGrantsmanshipReviewId";
            }
        }
        
        public static IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "TriageNote";
                yield return "IsThisRequestLinkedToAnExistingProject";
                yield return "DateOfTriage";
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
                yield return "IsRoundRobinEnquiry";
                yield return "TeamContactTitle";
                yield return "TeamContactFirstName";
                yield return "TeamContactLastName";
                yield return "TeamContactEmail";
                yield return "TeamContactAltEmail";
                yield return "TeamContactRoleFreeText";
                yield return "TeamContactMailingPermission";
                yield return "TeamContactPrivacyStatementRead";
                yield return "TeamContactServiceAgreementRead";
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
                yield return "TeamContactFullName";
                yield return "LeadApplicantFullName";
                yield return "ProjectId";
                yield return "CreatedAt";
                yield return "FileStorageGroupId";
                yield return "ProjectSupportGroupId";
                yield return "StatusId";
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
                yield return "WouldYouLikeToReceiveAGrantsmanshipReviewId";
                yield return "IsYourSupportRequestOnlyForAGrantsmanshipReviewId";
            }
        }
        
        // Validation
        
        public static void Validate(SupportRequest entity)
        {
            entity.TriageNote = entity.TriageNote ?? String.Empty;
            entity.TriageNote = entity.TriageNote.Trim();
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
            entity.TeamContactAltEmail = entity.TeamContactAltEmail ?? String.Empty;
            entity.TeamContactAltEmail = entity.TeamContactAltEmail.Trim();
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
        
        public static SupportRequestRepositoryValidator Validator { get; } = new SupportRequestRepositoryValidator();
        
        public const int TriageNote_MaxLength = 1024;
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
        public const int TeamContactAltEmail_MaxLength = 128;
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
        
        public static void TriageNote_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(TriageNote_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
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
        
        public static void TeamContactAltEmail_ValidationRules<T>(IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder
                .NotNull()
                .MaximumLength(TeamContactAltEmail_MaxLength).When(x => x != null, ApplyConditionTo.CurrentValidator);
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
    
    public partial class SupportRequestRepositoryValidator: AbstractValidator<SupportRequest>
    {
        public SupportRequestRepositoryValidator()
        {
            RuleFor(x => x.TriageNote).Use(SupportRequestMetadata.TriageNote_ValidationRules);
            RuleFor(x => x.Title).Use(SupportRequestMetadata.Title_ValidationRules);
            RuleFor(x => x.ApplicationStageFreeText).Use(SupportRequestMetadata.ApplicationStageFreeText_ValidationRules);
            RuleFor(x => x.ProposedFundingStreamName).Use(SupportRequestMetadata.ProposedFundingStreamName_ValidationRules);
            RuleFor(x => x.NIHRApplicationId).Use(SupportRequestMetadata.NIHRApplicationId_ValidationRules);
            RuleFor(x => x.ExperienceOfResearchAwards).Use(SupportRequestMetadata.ExperienceOfResearchAwards_ValidationRules);
            RuleFor(x => x.BriefDescription).Use(SupportRequestMetadata.BriefDescription_ValidationRules);
            RuleFor(x => x.SupportRequested).Use(SupportRequestMetadata.SupportRequested_ValidationRules);
            RuleFor(x => x.HowDidYouFindUsFreeText).Use(SupportRequestMetadata.HowDidYouFindUsFreeText_ValidationRules);
            RuleFor(x => x.WhoElseIsOnTheStudyTeam).Use(SupportRequestMetadata.WhoElseIsOnTheStudyTeam_ValidationRules);
            RuleFor(x => x.IsCTUAlreadyInvolvedFreeText).Use(SupportRequestMetadata.IsCTUAlreadyInvolvedFreeText_ValidationRules);
            RuleFor(x => x.ProfessionalBackgroundFreeText).Use(SupportRequestMetadata.ProfessionalBackgroundFreeText_ValidationRules);
            RuleFor(x => x.TeamContactTitle).Use(SupportRequestMetadata.TeamContactTitle_ValidationRules);
            RuleFor(x => x.TeamContactFirstName).Use(SupportRequestMetadata.TeamContactFirstName_ValidationRules);
            RuleFor(x => x.TeamContactLastName).Use(SupportRequestMetadata.TeamContactLastName_ValidationRules);
            RuleFor(x => x.TeamContactEmail).Use(SupportRequestMetadata.TeamContactEmail_ValidationRules);
            RuleFor(x => x.TeamContactAltEmail).Use(SupportRequestMetadata.TeamContactAltEmail_ValidationRules);
            RuleFor(x => x.TeamContactRoleFreeText).Use(SupportRequestMetadata.TeamContactRoleFreeText_ValidationRules);
            RuleFor(x => x.LeadApplicantTitle).Use(SupportRequestMetadata.LeadApplicantTitle_ValidationRules);
            RuleFor(x => x.LeadApplicantFirstName).Use(SupportRequestMetadata.LeadApplicantFirstName_ValidationRules);
            RuleFor(x => x.LeadApplicantLastName).Use(SupportRequestMetadata.LeadApplicantLastName_ValidationRules);
            RuleFor(x => x.LeadApplicantEmail).Use(SupportRequestMetadata.LeadApplicantEmail_ValidationRules);
            RuleFor(x => x.LeadApplicantJobRole).Use(SupportRequestMetadata.LeadApplicantJobRole_ValidationRules);
            RuleFor(x => x.LeadApplicantCareerStage).Use(SupportRequestMetadata.LeadApplicantCareerStage_ValidationRules);
            RuleFor(x => x.LeadApplicantOrganisation).Use(SupportRequestMetadata.LeadApplicantOrganisation_ValidationRules);
            RuleFor(x => x.LeadApplicantDepartment).Use(SupportRequestMetadata.LeadApplicantDepartment_ValidationRules);
            RuleFor(x => x.LeadApplicantAddressLine1).Use(SupportRequestMetadata.LeadApplicantAddressLine1_ValidationRules);
            RuleFor(x => x.LeadApplicantAddressLine2).Use(SupportRequestMetadata.LeadApplicantAddressLine2_ValidationRules);
            RuleFor(x => x.LeadApplicantAddressTown).Use(SupportRequestMetadata.LeadApplicantAddressTown_ValidationRules);
            RuleFor(x => x.LeadApplicantAddressCounty).Use(SupportRequestMetadata.LeadApplicantAddressCounty_ValidationRules);
            RuleFor(x => x.LeadApplicantAddressCountry).Use(SupportRequestMetadata.LeadApplicantAddressCountry_ValidationRules);
            RuleFor(x => x.LeadApplicantAddressPostcode).Use(SupportRequestMetadata.LeadApplicantAddressPostcode_ValidationRules);
            RuleFor(x => x.LeadApplicantORCID).Use(SupportRequestMetadata.LeadApplicantORCID_ValidationRules);
            RuleFor(x => x.TeamContactFullName).Use(SupportRequestMetadata.TeamContactFullName_ValidationRules);
            RuleFor(x => x.LeadApplicantFullName).Use(SupportRequestMetadata.LeadApplicantFullName_ValidationRules);
            Validation();
        }
        
        partial void Validation();
    }
}

