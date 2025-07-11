using System;
using System.Text;
using Webfuel.Domain.StaticData;

namespace Webfuel.Domain
{
    internal partial class ProjectChangeLogService: IProjectChangeLogService
    {
        public async Task<string> GenerateChangeLog(Project original, Project updated, string delimiter = "\n")
        {
            var staticData = await _staticDataService.GetStaticData();
            var sb = new StringBuilder();
            
            if(original.PrefixedNumber != updated.PrefixedNumber)
            {
                sb.Append("Prefixed Number: ").Append(original.PrefixedNumber).Append(" -> ").Append(updated.PrefixedNumber).Append(delimiter);
            }
            if(original.ClosureDate != updated.ClosureDate)
            {
                sb.Append("Closure Date: ").Append(original.ClosureDate?.ToString() ?? "NULL").Append(" -> ").Append(updated.ClosureDate?.ToString() ?? "NULL").Append(delimiter);
            }
            if(original.ClosureAttempted != updated.ClosureAttempted)
            {
                sb.Append("Closure Attempted: ").Append(original.ClosureAttempted).Append(" -> ").Append(updated.ClosureAttempted).Append(delimiter);
            }
            if(original.AdministratorComments != updated.AdministratorComments)
            {
                sb.Append("Administrator Comments: ").Append(original.AdministratorComments).Append(" -> ").Append(updated.AdministratorComments).Append(delimiter);
            }
            if(original.SubmittedFundingStreamFreeText != updated.SubmittedFundingStreamFreeText)
            {
                sb.Append("Submitted Funding Stream Free Text: ").Append(original.SubmittedFundingStreamFreeText).Append(" -> ").Append(updated.SubmittedFundingStreamFreeText).Append(delimiter);
            }
            if(original.SubmittedFundingStreamName != updated.SubmittedFundingStreamName)
            {
                sb.Append("Submitted Funding Stream Name: ").Append(original.SubmittedFundingStreamName).Append(" -> ").Append(updated.SubmittedFundingStreamName).Append(delimiter);
            }
            if(original.RSSHubProvidingAdviceIdsJson != updated.RSSHubProvidingAdviceIdsJson)
            {
                var o = string.Join(", ", original.RSSHubProvidingAdviceIds.Select(async p => (await _staticDataService.GetRSSHub(p))?.Name ?? "UNKNOWN"));
                var u = string.Join(", ", updated.RSSHubProvidingAdviceIds.Select(async p => (await _staticDataService.GetRSSHub(p))?.Name ?? "UNKNOWN"));
                sb.Append("RSSHub Providing Advice: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.MonetaryValueOfFundingApplication != updated.MonetaryValueOfFundingApplication)
            {
                sb.Append("Monetary Value Of Funding Application: ").Append(original.MonetaryValueOfFundingApplication?.ToString() ?? "NULL").Append(" -> ").Append(updated.MonetaryValueOfFundingApplication?.ToString() ?? "NULL").Append(delimiter);
            }
            if(original.ProjectStartDate != updated.ProjectStartDate)
            {
                sb.Append("Project Start Date: ").Append(original.ProjectStartDate?.ToString() ?? "NULL").Append(" -> ").Append(updated.ProjectStartDate?.ToString() ?? "NULL").Append(delimiter);
            }
            if(original.RecruitmentTarget != updated.RecruitmentTarget)
            {
                sb.Append("Recruitment Target: ").Append(original.RecruitmentTarget?.ToString() ?? "NULL").Append(" -> ").Append(updated.RecruitmentTarget?.ToString() ?? "NULL").Append(delimiter);
            }
            if(original.NumberOfProjectSites != updated.NumberOfProjectSites)
            {
                sb.Append("Number Of Project Sites: ").Append(original.NumberOfProjectSites?.ToString() ?? "NULL").Append(" -> ").Append(updated.NumberOfProjectSites?.ToString() ?? "NULL").Append(delimiter);
            }
            if(original.SocialCare != updated.SocialCare)
            {
                sb.Append("Social Care: ").Append(original.SocialCare).Append(" -> ").Append(updated.SocialCare).Append(delimiter);
            }
            if(original.PublicHealth != updated.PublicHealth)
            {
                sb.Append("Public Health: ").Append(original.PublicHealth).Append(" -> ").Append(updated.PublicHealth).Append(delimiter);
            }
            if(original.OutlineSubmissionDeadline != updated.OutlineSubmissionDeadline)
            {
                sb.Append("Outline Submission Deadline: ").Append(original.OutlineSubmissionDeadline?.ToString() ?? "NULL").Append(" -> ").Append(updated.OutlineSubmissionDeadline?.ToString() ?? "NULL").Append(delimiter);
            }
            if(original.OutlineOutcomeExpectedDate != updated.OutlineOutcomeExpectedDate)
            {
                sb.Append("Outline Outcome Expected Date: ").Append(original.OutlineOutcomeExpectedDate?.ToString() ?? "NULL").Append(" -> ").Append(updated.OutlineOutcomeExpectedDate?.ToString() ?? "NULL").Append(delimiter);
            }
            if(original.FullSubmissionDeadline != updated.FullSubmissionDeadline)
            {
                sb.Append("Full Submission Deadline: ").Append(original.FullSubmissionDeadline?.ToString() ?? "NULL").Append(" -> ").Append(updated.FullSubmissionDeadline?.ToString() ?? "NULL").Append(delimiter);
            }
            if(original.FullOutcomeExpectedDate != updated.FullOutcomeExpectedDate)
            {
                sb.Append("Full Outcome Expected Date: ").Append(original.FullOutcomeExpectedDate?.ToString() ?? "NULL").Append(" -> ").Append(updated.FullOutcomeExpectedDate?.ToString() ?? "NULL").Append(delimiter);
            }
            if(original.MockInterviews != updated.MockInterviews)
            {
                sb.Append("Mock Interviews: ").Append(original.MockInterviews).Append(" -> ").Append(updated.MockInterviews).Append(delimiter);
            }
            if(original.GrantsmanshipReview != updated.GrantsmanshipReview)
            {
                sb.Append("Grantsmanship Review: ").Append(original.GrantsmanshipReview).Append(" -> ").Append(updated.GrantsmanshipReview).Append(delimiter);
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
            if(original.NIHRApplicationId != updated.NIHRApplicationId)
            {
                sb.Append("NIHRApplication: ").Append(original.NIHRApplicationId).Append(" -> ").Append(updated.NIHRApplicationId).Append(delimiter);
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
                sb.Append("Is CTUAlready Involved Free Text: ").Append(original.IsCTUAlreadyInvolvedFreeText).Append(" -> ").Append(updated.IsCTUAlreadyInvolvedFreeText).Append(delimiter);
            }
            if(original.ProfessionalBackgroundIdsJson != updated.ProfessionalBackgroundIdsJson)
            {
                var o = string.Join(", ", original.ProfessionalBackgroundIds.Select(async p => (await _staticDataService.GetResearcherProfessionalBackground(p))?.Name ?? "UNKNOWN"));
                var u = string.Join(", ", updated.ProfessionalBackgroundIds.Select(async p => (await _staticDataService.GetResearcherProfessionalBackground(p))?.Name ?? "UNKNOWN"));
                sb.Append("Professional Background: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.ProfessionalBackgroundFreeText != updated.ProfessionalBackgroundFreeText)
            {
                sb.Append("Professional Background Free Text: ").Append(original.ProfessionalBackgroundFreeText).Append(" -> ").Append(updated.ProfessionalBackgroundFreeText).Append(delimiter);
            }
            if(original.IsRoundRobinEnquiry != updated.IsRoundRobinEnquiry)
            {
                sb.Append("Is Round Robin Enquiry: ").Append(original.IsRoundRobinEnquiry).Append(" -> ").Append(updated.IsRoundRobinEnquiry).Append(delimiter);
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
            if(original.TeamContactAltEmail != updated.TeamContactAltEmail)
            {
                sb.Append("Team Contact Alt Email: ").Append(original.TeamContactAltEmail).Append(" -> ").Append(updated.TeamContactAltEmail).Append(delimiter);
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
            if(original.TeamContactServiceAgreementRead != updated.TeamContactServiceAgreementRead)
            {
                sb.Append("Team Contact Service Agreement Read: ").Append(original.TeamContactServiceAgreementRead).Append(" -> ").Append(updated.TeamContactServiceAgreementRead).Append(delimiter);
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
            if(original.LeadApplicantCareerStage != updated.LeadApplicantCareerStage)
            {
                sb.Append("Lead Applicant Career Stage: ").Append(original.LeadApplicantCareerStage).Append(" -> ").Append(updated.LeadApplicantCareerStage).Append(delimiter);
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
                sb.Append("Lead Applicant ORCID: ").Append(original.LeadApplicantORCID).Append(" -> ").Append(updated.LeadApplicantORCID).Append(delimiter);
            }
            if(original.LeadAdviserUserId != updated.LeadAdviserUserId)
            {
                var o = original.LeadAdviserUserId.HasValue ? (await _userRepository.GetUser(original.LeadAdviserUserId.Value))?.FullName ?? "UNKNOWN" : "NULL";
                var u = updated.LeadAdviserUserId.HasValue ? (await _userRepository.GetUser(updated.LeadAdviserUserId.Value))?.FullName ?? "UNKNOWN" : "NULL";
                sb.Append("Lead Adviser User: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.SubmittedFundingStreamId != updated.SubmittedFundingStreamId)
            {
                var o = original.SubmittedFundingStreamId.HasValue ? (await _staticDataService.GetFundingStream(original.SubmittedFundingStreamId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.SubmittedFundingStreamId.HasValue ? (await _staticDataService.GetFundingStream(updated.SubmittedFundingStreamId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Submitted Funding Stream: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.StatusId != updated.StatusId)
            {
                var o = (await _staticDataService.GetProjectStatus(original.StatusId))?.Name ?? "UNKNOWN";
                var u = (await _staticDataService.GetProjectStatus(updated.StatusId))?.Name ?? "UNKNOWN";
                sb.Append("Status: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.WillStudyUseCTUId != updated.WillStudyUseCTUId)
            {
                var o = original.WillStudyUseCTUId.HasValue ? (await _staticDataService.GetWillStudyUseCTU(original.WillStudyUseCTUId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.WillStudyUseCTUId.HasValue ? (await _staticDataService.GetWillStudyUseCTU(updated.WillStudyUseCTUId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Will Study Use CTUId: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.IsPaidRSSAdviserLeadId != updated.IsPaidRSSAdviserLeadId)
            {
                var o = original.IsPaidRSSAdviserLeadId.HasValue ? (await _staticDataService.GetIsPaidRSSAdviserLead(original.IsPaidRSSAdviserLeadId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.IsPaidRSSAdviserLeadId.HasValue ? (await _staticDataService.GetIsPaidRSSAdviserLead(updated.IsPaidRSSAdviserLeadId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Is Paid RSSAdviser Lead: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.IsPaidRSSAdviserCoapplicantId != updated.IsPaidRSSAdviserCoapplicantId)
            {
                var o = original.IsPaidRSSAdviserCoapplicantId.HasValue ? (await _staticDataService.GetIsPaidRSSAdviserCoapplicant(original.IsPaidRSSAdviserCoapplicantId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.IsPaidRSSAdviserCoapplicantId.HasValue ? (await _staticDataService.GetIsPaidRSSAdviserCoapplicant(updated.IsPaidRSSAdviserCoapplicantId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Is Paid RSSAdviser Coapplicant: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.IsInternationalMultiSiteStudyId != updated.IsInternationalMultiSiteStudyId)
            {
                var o = original.IsInternationalMultiSiteStudyId.HasValue ? (await _staticDataService.GetIsInternationalMultiSiteStudy(original.IsInternationalMultiSiteStudyId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.IsInternationalMultiSiteStudyId.HasValue ? (await _staticDataService.GetIsInternationalMultiSiteStudy(updated.IsInternationalMultiSiteStudyId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Is International Multi Site Study: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.OutlineSubmissionStatusId != updated.OutlineSubmissionStatusId)
            {
                var o = original.OutlineSubmissionStatusId.HasValue ? (await _staticDataService.GetOutlineSubmissionStatus(original.OutlineSubmissionStatusId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.OutlineSubmissionStatusId.HasValue ? (await _staticDataService.GetOutlineSubmissionStatus(updated.OutlineSubmissionStatusId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Outline Submission Status: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.OutlineOutcomeId != updated.OutlineOutcomeId)
            {
                var o = original.OutlineOutcomeId.HasValue ? (await _staticDataService.GetOutlineOutcome(original.OutlineOutcomeId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.OutlineOutcomeId.HasValue ? (await _staticDataService.GetOutlineOutcome(updated.OutlineOutcomeId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Outline Outcome: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.FullSubmissionStatusId != updated.FullSubmissionStatusId)
            {
                var o = original.FullSubmissionStatusId.HasValue ? (await _staticDataService.GetFullSubmissionStatus(original.FullSubmissionStatusId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.FullSubmissionStatusId.HasValue ? (await _staticDataService.GetFullSubmissionStatus(updated.FullSubmissionStatusId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Full Submission Status: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.FullOutcomeId != updated.FullOutcomeId)
            {
                var o = original.FullOutcomeId.HasValue ? (await _staticDataService.GetFullOutcome(original.FullOutcomeId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.FullOutcomeId.HasValue ? (await _staticDataService.GetFullOutcome(updated.FullOutcomeId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Full Outcome: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
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
                sb.Append("Is CTUAlready Involved: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.TeamContactRoleId != updated.TeamContactRoleId)
            {
                var o = original.TeamContactRoleId.HasValue ? (await _staticDataService.GetResearcherRole(original.TeamContactRoleId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.TeamContactRoleId.HasValue ? (await _staticDataService.GetResearcherRole(updated.TeamContactRoleId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Team Contact Role: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.LeadApplicantCareerStageId != updated.LeadApplicantCareerStageId)
            {
                var o = original.LeadApplicantCareerStageId.HasValue ? (await _staticDataService.GetResearcherCareerStage(original.LeadApplicantCareerStageId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.LeadApplicantCareerStageId.HasValue ? (await _staticDataService.GetResearcherCareerStage(updated.LeadApplicantCareerStageId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Lead Applicant Career Stage: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.LeadApplicantOrganisationTypeId != updated.LeadApplicantOrganisationTypeId)
            {
                var o = original.LeadApplicantOrganisationTypeId.HasValue ? (await _staticDataService.GetResearcherOrganisationType(original.LeadApplicantOrganisationTypeId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.LeadApplicantOrganisationTypeId.HasValue ? (await _staticDataService.GetResearcherOrganisationType(updated.LeadApplicantOrganisationTypeId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Lead Applicant Organisation Type: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.LeadApplicantLocationId != updated.LeadApplicantLocationId)
            {
                var o = original.LeadApplicantLocationId.HasValue ? (await _staticDataService.GetResearcherLocation(original.LeadApplicantLocationId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.LeadApplicantLocationId.HasValue ? (await _staticDataService.GetResearcherLocation(updated.LeadApplicantLocationId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Lead Applicant Location: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
            }
            if(original.IsLeadApplicantNHSId != updated.IsLeadApplicantNHSId)
            {
                var o = original.IsLeadApplicantNHSId.HasValue ? (await _staticDataService.GetIsLeadApplicantNHS(original.IsLeadApplicantNHSId.Value))?.Name ?? "UNKNOWN" : "NULL";
                var u = updated.IsLeadApplicantNHSId.HasValue ? (await _staticDataService.GetIsLeadApplicantNHS(updated.IsLeadApplicantNHSId.Value))?.Name ?? "UNKNOWN" : "NULL";
                sb.Append("Is Lead Applicant NHSId: ").Append(o).Append(" -> ").Append(u).Append(delimiter);
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

