using System;
using System.Text;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    internal partial class SupportRequestChangeLogService: ISupportRequestChangeLogService
    {
        public async Task<string> GenerateChangeLog(SupportRequest original, SupportRequest updated, string delimiter = "\n")
        {
            var staticData = await _staticDataService.GetStaticData();
            var sb = new StringBuilder();
            
            if(original.PrefixedNumber != updated.PrefixedNumber)
            {
                sb.Append("Prefixed Number: ").Append(original.PrefixedNumber).Append(" -> ").Append(updated.PrefixedNumber).Append(delimiter);
            }
            if(original.DateOfRequest != updated.DateOfRequest)
            {
                sb.Append("Date Of Request: ").Append(original.DateOfRequest).Append(" -> ").Append(updated.DateOfRequest).Append(delimiter);
            }
            if(original.Title != updated.Title)
            {
                sb.Append("Title: ").Append(original.Title).Append(" -> ").Append(updated.Title).Append(delimiter);
            }
            if(original.ApplicationStageFreeText != updated.ApplicationStageFreeText)
            {
                sb.Append("Application Stage Free Text: ").Append(original.ApplicationStageFreeText).Append(" -> ").Append(updated.ApplicationStageFreeText).Append(delimiter);
            }
            if(original.ProposedFundingStreamName != updated.ProposedFundingStreamName)
            {
                sb.Append("Proposed Funding Stream Name: ").Append(original.ProposedFundingStreamName).Append(" -> ").Append(updated.ProposedFundingStreamName).Append(delimiter);
            }
            if(original.TargetSubmissionDate != updated.TargetSubmissionDate)
            {
                sb.Append("Target Submission Date: ").Append(original.TargetSubmissionDate?.ToString() ?? "NULL").Append(" -> ").Append(updated.TargetSubmissionDate?.ToString() ?? "NULL").Append(delimiter);
            }
            if(original.ExperienceOfResearchAwards != updated.ExperienceOfResearchAwards)
            {
                sb.Append("Experience Of Research Awards: ").Append(original.ExperienceOfResearchAwards).Append(" -> ").Append(updated.ExperienceOfResearchAwards).Append(delimiter);
            }
            if(original.BriefDescription != updated.BriefDescription)
            {
                sb.Append("Brief Description: ").Append(original.BriefDescription).Append(" -> ").Append(updated.BriefDescription).Append(delimiter);
            }
            if(original.SupportRequested != updated.SupportRequested)
            {
                sb.Append("Support Requested: ").Append(original.SupportRequested).Append(" -> ").Append(updated.SupportRequested).Append(delimiter);
            }
            if(original.HowDidYouFindUsFreeText != updated.HowDidYouFindUsFreeText)
            {
                sb.Append("How Did You Find Us Free Text: ").Append(original.HowDidYouFindUsFreeText).Append(" -> ").Append(updated.HowDidYouFindUsFreeText).Append(delimiter);
            }
            if(original.WhoElseIsOnTheStudyTeam != updated.WhoElseIsOnTheStudyTeam)
            {
                sb.Append("Who Else Is On The Study Team: ").Append(original.WhoElseIsOnTheStudyTeam).Append(" -> ").Append(updated.WhoElseIsOnTheStudyTeam).Append(delimiter);
            }
            if(original.IsCTUAlreadyInvolvedFreeText != updated.IsCTUAlreadyInvolvedFreeText)
            {
                sb.Append("Is C T U Already Involved Free Text: ").Append(original.IsCTUAlreadyInvolvedFreeText).Append(" -> ").Append(updated.IsCTUAlreadyInvolvedFreeText).Append(delimiter);
            }
            if(original.TeamContactTitle != updated.TeamContactTitle)
            {
                sb.Append("Team Contact Title: ").Append(original.TeamContactTitle).Append(" -> ").Append(updated.TeamContactTitle).Append(delimiter);
            }
            if(original.TeamContactFirstName != updated.TeamContactFirstName)
            {
                sb.Append("Team Contact First Name: ").Append(original.TeamContactFirstName).Append(" -> ").Append(updated.TeamContactFirstName).Append(delimiter);
            }
            if(original.TeamContactLastName != updated.TeamContactLastName)
            {
                sb.Append("Team Contact Last Name: ").Append(original.TeamContactLastName).Append(" -> ").Append(updated.TeamContactLastName).Append(delimiter);
            }
            if(original.TeamContactEmail != updated.TeamContactEmail)
            {
                sb.Append("Team Contact Email: ").Append(original.TeamContactEmail).Append(" -> ").Append(updated.TeamContactEmail).Append(delimiter);
            }
            if(original.TeamContactRoleFreeText != updated.TeamContactRoleFreeText)
            {
                sb.Append("Team Contact Role Free Text: ").Append(original.TeamContactRoleFreeText).Append(" -> ").Append(updated.TeamContactRoleFreeText).Append(delimiter);
            }
            if(original.TeamContactMailingPermission != updated.TeamContactMailingPermission)
            {
                sb.Append("Team Contact Mailing Permission: ").Append(original.TeamContactMailingPermission).Append(" -> ").Append(updated.TeamContactMailingPermission).Append(delimiter);
            }
            if(original.TeamContactPrivacyStatementRead != updated.TeamContactPrivacyStatementRead)
            {
                sb.Append("Team Contact Privacy Statement Read: ").Append(original.TeamContactPrivacyStatementRead).Append(" -> ").Append(updated.TeamContactPrivacyStatementRead).Append(delimiter);
            }
            if(original.LeadApplicantTitle != updated.LeadApplicantTitle)
            {
                sb.Append("Lead Applicant Title: ").Append(original.LeadApplicantTitle).Append(" -> ").Append(updated.LeadApplicantTitle).Append(delimiter);
            }
            if(original.LeadApplicantFirstName != updated.LeadApplicantFirstName)
            {
                sb.Append("Lead Applicant First Name: ").Append(original.LeadApplicantFirstName).Append(" -> ").Append(updated.LeadApplicantFirstName).Append(delimiter);
            }
            if(original.LeadApplicantLastName != updated.LeadApplicantLastName)
            {
                sb.Append("Lead Applicant Last Name: ").Append(original.LeadApplicantLastName).Append(" -> ").Append(updated.LeadApplicantLastName).Append(delimiter);
            }
            if(original.LeadApplicantEmail != updated.LeadApplicantEmail)
            {
                sb.Append("Lead Applicant Email: ").Append(original.LeadApplicantEmail).Append(" -> ").Append(updated.LeadApplicantEmail).Append(delimiter);
            }
            if(original.LeadApplicantJobRole != updated.LeadApplicantJobRole)
            {
                sb.Append("Lead Applicant Job Role: ").Append(original.LeadApplicantJobRole).Append(" -> ").Append(updated.LeadApplicantJobRole).Append(delimiter);
            }
            if(original.LeadApplicantOrganisation != updated.LeadApplicantOrganisation)
            {
                sb.Append("Lead Applicant Organisation: ").Append(original.LeadApplicantOrganisation).Append(" -> ").Append(updated.LeadApplicantOrganisation).Append(delimiter);
            }
            if(original.LeadApplicantDepartment != updated.LeadApplicantDepartment)
            {
                sb.Append("Lead Applicant Department: ").Append(original.LeadApplicantDepartment).Append(" -> ").Append(updated.LeadApplicantDepartment).Append(delimiter);
            }
            if(original.LeadApplicantAddressLine1 != updated.LeadApplicantAddressLine1)
            {
                sb.Append("Lead Applicant Address Line1: ").Append(original.LeadApplicantAddressLine1).Append(" -> ").Append(updated.LeadApplicantAddressLine1).Append(delimiter);
            }
            if(original.LeadApplicantAddressLine2 != updated.LeadApplicantAddressLine2)
            {
                sb.Append("Lead Applicant Address Line2: ").Append(original.LeadApplicantAddressLine2).Append(" -> ").Append(updated.LeadApplicantAddressLine2).Append(delimiter);
            }
            if(original.LeadApplicantAddressTown != updated.LeadApplicantAddressTown)
            {
                sb.Append("Lead Applicant Address Town: ").Append(original.LeadApplicantAddressTown).Append(" -> ").Append(updated.LeadApplicantAddressTown).Append(delimiter);
            }
            if(original.LeadApplicantAddressCounty != updated.LeadApplicantAddressCounty)
            {
                sb.Append("Lead Applicant Address County: ").Append(original.LeadApplicantAddressCounty).Append(" -> ").Append(updated.LeadApplicantAddressCounty).Append(delimiter);
            }
            if(original.LeadApplicantAddressCountry != updated.LeadApplicantAddressCountry)
            {
                sb.Append("Lead Applicant Address Country: ").Append(original.LeadApplicantAddressCountry).Append(" -> ").Append(updated.LeadApplicantAddressCountry).Append(delimiter);
            }
            if(original.LeadApplicantAddressPostcode != updated.LeadApplicantAddressPostcode)
            {
                sb.Append("Lead Applicant Address Postcode: ").Append(original.LeadApplicantAddressPostcode).Append(" -> ").Append(updated.LeadApplicantAddressPostcode).Append(delimiter);
            }
            if(original.LeadApplicantORCID != updated.LeadApplicantORCID)
            {
                sb.Append("Lead Applicant O R C I D: ").Append(original.LeadApplicantORCID).Append(" -> ").Append(updated.LeadApplicantORCID).Append(delimiter);
            }
            if(original.StatusId != updated.StatusId)
            {
                var o = (await _staticDataService.GetSupportRequestStatus(original.StatusId))?.Name ?? "UNKNOWN";
                var u = (await _staticDataService.GetSupportRequestStatus(updated.StatusId))?.Name ?? "UNKNOWN";
                sb.Append("Status: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.IsFellowshipId != updated.IsFellowshipId)
            {
                var o = original.IsFellowshipId.HasValue ? (await _staticDataService.GetIsFellowship(original.IsFellowshipId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.IsFellowshipId.HasValue ? (await _staticDataService.GetIsFellowship(updated.IsFellowshipId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Is Fellowship: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.ApplicationStageId != updated.ApplicationStageId)
            {
                var o = original.ApplicationStageId.HasValue ? (await _staticDataService.GetApplicationStage(original.ApplicationStageId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.ApplicationStageId.HasValue ? (await _staticDataService.GetApplicationStage(updated.ApplicationStageId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Application Stage: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.ProposedFundingCallTypeId != updated.ProposedFundingCallTypeId)
            {
                var o = original.ProposedFundingCallTypeId.HasValue ? (await _staticDataService.GetFundingCallType(original.ProposedFundingCallTypeId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.ProposedFundingCallTypeId.HasValue ? (await _staticDataService.GetFundingCallType(updated.ProposedFundingCallTypeId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Proposed Funding Call Type: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.ProposedFundingStreamId != updated.ProposedFundingStreamId)
            {
                var o = original.ProposedFundingStreamId.HasValue ? (await _staticDataService.GetFundingStream(original.ProposedFundingStreamId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.ProposedFundingStreamId.HasValue ? (await _staticDataService.GetFundingStream(updated.ProposedFundingStreamId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Proposed Funding Stream: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.IsTeamMembersConsultedId != updated.IsTeamMembersConsultedId)
            {
                var o = original.IsTeamMembersConsultedId.HasValue ? (await _staticDataService.GetIsTeamMembersConsulted(original.IsTeamMembersConsultedId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.IsTeamMembersConsultedId.HasValue ? (await _staticDataService.GetIsTeamMembersConsulted(updated.IsTeamMembersConsultedId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Is Team Members Consulted: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.IsResubmissionId != updated.IsResubmissionId)
            {
                var o = original.IsResubmissionId.HasValue ? (await _staticDataService.GetIsResubmission(original.IsResubmissionId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.IsResubmissionId.HasValue ? (await _staticDataService.GetIsResubmission(updated.IsResubmissionId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Is Resubmission: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.HowDidYouFindUsId != updated.HowDidYouFindUsId)
            {
                var o = original.HowDidYouFindUsId.HasValue ? (await _staticDataService.GetHowDidYouFindUs(original.HowDidYouFindUsId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.HowDidYouFindUsId.HasValue ? (await _staticDataService.GetHowDidYouFindUs(updated.HowDidYouFindUsId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("How Did You Find Us: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.IsCTUAlreadyInvolvedId != updated.IsCTUAlreadyInvolvedId)
            {
                var o = original.IsCTUAlreadyInvolvedId.HasValue ? (await _staticDataService.GetIsCTUAlreadyInvolved(original.IsCTUAlreadyInvolvedId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.IsCTUAlreadyInvolvedId.HasValue ? (await _staticDataService.GetIsCTUAlreadyInvolved(updated.IsCTUAlreadyInvolvedId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Is C T U Already Involved: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.TeamContactRoleId != updated.TeamContactRoleId)
            {
                var o = original.TeamContactRoleId.HasValue ? (await _staticDataService.GetResearcherRole(original.TeamContactRoleId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.TeamContactRoleId.HasValue ? (await _staticDataService.GetResearcherRole(updated.TeamContactRoleId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Team Contact Role: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.LeadApplicantOrganisationTypeId != updated.LeadApplicantOrganisationTypeId)
            {
                var o = original.LeadApplicantOrganisationTypeId.HasValue ? (await _staticDataService.GetResearcherOrganisationType(original.LeadApplicantOrganisationTypeId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.LeadApplicantOrganisationTypeId.HasValue ? (await _staticDataService.GetResearcherOrganisationType(updated.LeadApplicantOrganisationTypeId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Lead Applicant Organisation Type: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.IsLeadApplicantNHSId != updated.IsLeadApplicantNHSId)
            {
                var o = original.IsLeadApplicantNHSId.HasValue ? (await _staticDataService.GetIsLeadApplicantNHS(original.IsLeadApplicantNHSId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.IsLeadApplicantNHSId.HasValue ? (await _staticDataService.GetIsLeadApplicantNHS(updated.IsLeadApplicantNHSId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Is Lead Applicant N H S: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.LeadApplicantAgeRangeId != updated.LeadApplicantAgeRangeId)
            {
                var o = original.LeadApplicantAgeRangeId.HasValue ? (await _staticDataService.GetAgeRange(original.LeadApplicantAgeRangeId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.LeadApplicantAgeRangeId.HasValue ? (await _staticDataService.GetAgeRange(updated.LeadApplicantAgeRangeId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Lead Applicant Age Range: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.LeadApplicantGenderId != updated.LeadApplicantGenderId)
            {
                var o = original.LeadApplicantGenderId.HasValue ? (await _staticDataService.GetGender(original.LeadApplicantGenderId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.LeadApplicantGenderId.HasValue ? (await _staticDataService.GetGender(updated.LeadApplicantGenderId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Lead Applicant Gender: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.LeadApplicantEthnicityId != updated.LeadApplicantEthnicityId)
            {
                var o = original.LeadApplicantEthnicityId.HasValue ? (await _staticDataService.GetEthnicity(original.LeadApplicantEthnicityId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.LeadApplicantEthnicityId.HasValue ? (await _staticDataService.GetEthnicity(updated.LeadApplicantEthnicityId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Lead Applicant Ethnicity: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            return sb.ToString();
        }
    }
}

