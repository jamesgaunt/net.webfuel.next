namespace Webfuel.Domain
{
    public class AnnualReportData
    {
        public required string ProjectID { get; set; } = String.Empty;
        public required DateOnly DateOfFirstContact { get; set; } = DateOnly.MinValue;
        public required DateOnly? ClosureDate { get; set; }
        public required string NIHRApplicationID { get; set; } = String.Empty;
        public required string ApplicationDestination { get; set; } = String.Empty;
        public required string Submitted { get; set; } = String.Empty;
        public required string CommentOnApplicationDestination { get; set; } = String.Empty;
        public required string SubmissionStage { get; set; } = String.Empty;
        public required string Outcome { get; set; } = String.Empty;
        public required bool IsAPaidRSSStaffMemberLead { get; set; }
        public required bool IsAPaidRSSStaffMemberCoapplicant { get; set; }
        public required string WasAdvicePrePostAward { get; set; } = String.Empty;
        public required string TotalAmountOfSupportProvided { get; set;} = String.Empty;
        public required string CategoriesOfSupportProvided { get; set; } = String.Empty;
        public required string AdviserDisciplines { get; set; } = String.Empty;
        public required bool WillThisStudyUseACTU { get; set; }
        public required string IsThisCTUInternalOrExternalToTheRSS { get; set; } = String.Empty;
        public required string SitesInvolvedInProvidingAdvice { get; set;} = String.Empty;
        public required decimal? MonetaryValueOfFundingApplication { get; set;} = null;
        public required string ProjectTeamLeadCareerStage { get; set; } = String.Empty;
        public required string ProjectTeamLeadEmployingOrganisation { get; set; } = String.Empty;
        public required string ProjectTeamLeadEmployingOrganisationType { get; set; } = String.Empty;
        public required string WhereIsSupportBased { get; set; } = String.Empty;
        public required string OtherSupportBased { get; set; } = String.Empty;
        public required string ProjectTeamLeadORCID { get; set; } = String.Empty;
        public required string ProfessionalBackgroundOfApplicants { get; set; } = String.Empty;
        public required bool HasRSSProvidedPPIGrant { get; set; }
    }
}
