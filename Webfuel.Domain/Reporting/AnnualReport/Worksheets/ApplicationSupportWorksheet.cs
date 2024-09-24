using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.DependencyInjection;
using Webfuel.Domain.StaticData;
using Webfuel.Excel;

namespace Webfuel.Domain
{
    internal class ApplicationSupportWorksheet : AnnualReportWorksheet
    {
        public ApplicationSupportWorksheet(ExcelWorkbook workbook) : base(workbook, "Application Support")
        {
            HeaderRow = 4;
            Initialise();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Initialise

        protected override void Initialise()
        {
            base.Initialise();

            MergeCell("A1", "CI1", "Application Support", AnnualReportUtility.GreyBG);
            MergeCell("A2", "K2", "Section A", AnnualReportUtility.DarkBlueBG);
            MergeCell("L2", "BO2", "Section B: Reporting Period", AnnualReportUtility.DarkBlueBG);
            MergeCell("BP2", "CG2", "Section C : Research team information", AnnualReportUtility.DarkBlueBG);
            MergeCell("CH2", "CI2", "Section D: Patient and Public Involvement and Engagement", AnnualReportUtility.DarkBlueBG);
            MergeCell("A3", "K3", "", AnnualReportUtility.DarkBlueBG);
            MergeCell("L3", "L3", "", AnnualReportUtility.LightBlueBG);
            MergeCell("M3", "AL3", "Categories of support provided  (more than one can be selected)", AnnualReportUtility.LightBlueBG);
            MergeCell("AM3", "BD3", "Specific type or discipline of advisor(s) providing advice (more than one can be selected)", AnnualReportUtility.LightBlueBG);
            MergeCell("BE3", "BO3", "Sites involved in providing advice (more than one can be selected)", AnnualReportUtility.LightBlueBG);
            MergeCell("BP3", "BU3", "", AnnualReportUtility.LightBlueBG);
            MergeCell("BV3", "CG3", "Professional background of all team/applicants (where known - more than one can be selected)", AnnualReportUtility.LightBlueBG);
            MergeCell("CH3", "CI3", "", AnnualReportUtility.LightBlueBG);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Schema

        // Section A

        public AnnualReportColumn ProjectID = new AnnualReportColumn { Index = 1, Title = "Project ID" };

        public AnnualReportColumn DateOfFirstContact = new AnnualReportColumn { Index = 2, Title = "Date of first contact" };

        public AnnualReportColumn ClosureDate = new AnnualReportColumn { Index = 3, Title = "Closure date", IsOptional = true };

        public AnnualReportColumn NIHRApplicationID = new AnnualReportColumn { Index = 4, Title = "NIHR application ID (if known)", IsOptional = true };

        public AnnualReportColumn ApplicationDestination = new AnnualReportColumn { Index = 5, Title = "Application destination (NIHR programme or other funder name)" }
        .WithValues("Charities", "EME", "HS&DR", "HTA", "i4i", "NIHR (other)", "NIHR joint funded", "Other", "PDG", "PGfAR", "PHR", "PRP", "RfPB", "Research Councils", "Unknown" );

        public AnnualReportColumn ProjectSubmitted = new AnnualReportColumn { Index = 6, Title = "Project Submitted" }.WithValues("Y", "N", "Don't know", "N/A");

        public AnnualReportColumn SubmissionStage = new AnnualReportColumn { Index = 7, Title = "Submission stage" }.WithValues("Outline", "Full", "One-stage", "Not applicable", "Unknown");

        public AnnualReportColumn Outcome = new AnnualReportColumn { Index = 8, Title = "Outcome" }.WithValues("Shortlisted", "Rejected", "Not submitted", "Successful", "Unknown");

        public AnnualReportColumn IsPaidRSSLead = new AnnualReportColumn { Index = 9, Title = "Is a paid RSS staff member, who provided advice, the lead/co-lead for this application?", IsBoolean = true };

        public AnnualReportColumn IsPaidRSSCoApplicant = new AnnualReportColumn { Index = 10, Title = "Is a paid RSS staff member, who provided advice, a co-applicant on this application?", IsBoolean = true };

        public AnnualReportColumn WasPrePostAdvice = new AnnualReportColumn { Index = 11, Title = "Was advice provided pre-award or post-award?" }.WithValues("Pre-award", "Pre- and post-award", "Post-award without pre-award support");

        // Section B

        public AnnualReportColumn TotalAmountOfSupport = new AnnualReportColumn { Index = 12, Title = "Total amount of support provided since first contact" }.WithValues("High ≥ 30 hours", "Medium  ≥ 10", "Low <10");

        // Section B: Categories of Support

        public AnnualReportColumn FormulatingResearchQuestions = new AnnualReportColumn { Index = 13, Title = "Formulating research questions", IsBoolean = true };

        public AnnualReportColumn QuantitativeResearchDesign = new AnnualReportColumn { Index = 15, Title = "Quantitative research design", IsBoolean = true };

        public AnnualReportColumn QualitativeResearchDesign = new AnnualReportColumn { Index = 14, Title = "Qualitative research design", IsBoolean = true };

        public AnnualReportColumn ResearchDesignForMixedMethods = new AnnualReportColumn { Index = 16, Title = "Research design for mixed methods", IsBoolean = true };

        public AnnualReportColumn ResearchDesignForTrials = new AnnualReportColumn { Index = 17, Title = "Research design for trials", IsBoolean = true };

        public AnnualReportColumn CostingAndCostApproval = new AnnualReportColumn { Index = 18, Title = "Costing and cost approval", IsBoolean = true };

        public AnnualReportColumn IdentifyingFundingSources = new AnnualReportColumn { Index = 19, Title = "Identifying and applying to appropriate funding sources", IsBoolean = true };

        public AnnualReportColumn IdentifyingCollaborators = new AnnualReportColumn { Index = 20, Title = "Identifying appropriate collaborators in research", IsBoolean = true };

        public AnnualReportColumn SignpostingForAdvice = new AnnualReportColumn { Index = 21, Title = "Signpost researchers to other specialised centres and individuals for advice", IsBoolean = true };

        public AnnualReportColumn EDI = new AnnualReportColumn { Index = 22, Title = "EDI", IsBoolean = true };

        public AnnualReportColumn PromotingPPI = new AnnualReportColumn { Index = 23, Title = "Promoting and facilitating patient and public involvement", IsBoolean = true };

        public AnnualReportColumn AccessingRDApproval = new AnnualReportColumn { Index = 24, Title = "Accessing and applying for R&D approval e.g IRAS/REC/sponsorship", IsBoolean = true };

        public AnnualReportColumn ApplicationWritingSkills = new AnnualReportColumn { Index = 25, Title = "Application writing/\"grantsmanship\" skills", IsBoolean = true };

        public AnnualReportColumn CriticalReview = new AnnualReportColumn { Index = 26, Title = "Critical review", IsBoolean = true };

        public AnnualReportColumn LiteratureReview = new AnnualReportColumn { Index = 27, Title = "Literature review", IsBoolean = true };

        public AnnualReportColumn ImpactAdvice = new AnnualReportColumn { Index = 28, Title = "Impact advice", IsBoolean = true };

        public AnnualReportColumn IntellectualPropertyAdvice = new AnnualReportColumn { Index = 29, Title = "Intellectual Property advice", IsBoolean = true };

        public AnnualReportColumn CollaborationAgreementSetup = new AnnualReportColumn { Index = 30, Title = "Collaboration agreement/contract negotiation and set-up", IsBoolean = true };

        public AnnualReportColumn RecruitmentForTrials = new AnnualReportColumn { Index = 31, Title = "Recruitment for trials", IsBoolean = true };

        public AnnualReportColumn MethodologyDevelopment = new AnnualReportColumn { Index = 32, Title = "Methodology development", IsBoolean = true };

        public AnnualReportColumn Randomisation = new AnnualReportColumn { Index = 33, Title = "Randomisation", IsBoolean = true };

        public AnnualReportColumn TrialManagement = new AnnualReportColumn { Index = 34, Title = "Trial management", IsBoolean = true };

        public AnnualReportColumn AnalysisIncludingStatistical = new AnnualReportColumn { Index = 35, Title = "Analysis including statistical", IsBoolean = true };

        public AnnualReportColumn StudyClose = new AnnualReportColumn { Index = 36, Title = "Study close", IsBoolean = true };

        public AnnualReportColumn SupportOther = new AnnualReportColumn { Index = 37, Title = "Other - comment", IsOptional = true };

        public AnnualReportColumn SupportDontKnow = new AnnualReportColumn { Index = 38, Title = "Don't know", IsBoolean = true };

        // Section B: Advisor Disciplines

        public AnnualReportColumn GeneralResearchMethodologist = new AnnualReportColumn { Index = 39, Title = "General research methodologist", IsBoolean = true };

        public AnnualReportColumn SocialScientist = new AnnualReportColumn { Index = 40, Title = "Social scientist", IsBoolean = true };

        public AnnualReportColumn HealthEconomist = new AnnualReportColumn { Index = 41, Title = "Health economist", IsBoolean = true };

        public AnnualReportColumn Statistician = new AnnualReportColumn { Index = 42, Title = "Statistician", IsBoolean = true };

        public AnnualReportColumn Epidemiologist = new AnnualReportColumn { Index = 43, Title = "Epidemiologist", IsBoolean = true };

        public AnnualReportColumn ClinicalTrialist = new AnnualReportColumn { Index = 44, Title = "Clinical trialist", IsBoolean = true };

        public AnnualReportColumn InformationSpecialist = new AnnualReportColumn { Index = 45, Title = "Information specialist/systematic reviewer", IsBoolean = true };

        public AnnualReportColumn HealthPsychologist = new AnnualReportColumn { Index = 46, Title = "Health psychologist/behavioural scientist", IsBoolean = true };

        public AnnualReportColumn PatientPublicInvolvement = new AnnualReportColumn { Index = 47, Title = "Patient and public involvement and engagement", IsBoolean = true };

        public AnnualReportColumn LocalAuthorityOfficer = new AnnualReportColumn { Index = 48, Title = "Local authority officer/policymaker", IsBoolean = true };

        public AnnualReportColumn QualitativeResearcher = new AnnualReportColumn { Index = 49, Title = "Qualitative researcher", IsBoolean = true };

        public AnnualReportColumn AdvisorOther = new AnnualReportColumn { Index = 50, Title = "Other - comment", IsOptional = true };

        public AnnualReportColumn AdvisorDontKnow = new AnnualReportColumn { Index = 51, Title = "Don't know", IsBoolean = true };

        public AnnualReportColumn AdvisorAdditionalInformation = new AnnualReportColumn { Index = 52, Title = "Additional Information + Comments (optional)", IsOptional = true };

        // Section B:  Other Details

        public AnnualReportColumn NonNHSSetting = new AnnualReportColumn { Index = 53, Title = "Will the research be taking place in a non-NHS setting?", IsBoolean = true };

        public AnnualReportColumn IsPublicHealthStudy = new AnnualReportColumn { Index = 54, Title = "Is this a public health or social care study?", IsOptional = true }.WithValues("Public Health", "Social Care");

        public AnnualReportColumn WillThisStudyUseACTU = new AnnualReportColumn { Index = 55, Title = "Will this study use a CTU?", IsBoolean = true };

        public AnnualReportColumn IsThisCTUAssociatedWithTheRSS = new AnnualReportColumn { Index = 56, Title = "Is this CTU associated with the RSS?", IsBoolean = true };

        // Section B: RSS providing support

        public AnnualReportColumn NewcastleUniversity = new AnnualReportColumn { Index = 57, Title = "Newcastle University and Partners", IsBoolean = true };

        public AnnualReportColumn UniversityOfBirmingham = new AnnualReportColumn { Index = 58, Title = "University of Birmingham and Partners", IsBoolean = true };

        public AnnualReportColumn UniversityOfLancaster = new AnnualReportColumn { Index = 59, Title = "University of Lancaster and Partners", IsBoolean = true };

        public AnnualReportColumn UniversityOfYork = new AnnualReportColumn { Index = 60, Title = "University of York and Partners", IsBoolean = true };

        public AnnualReportColumn ImperialCollegeLondon = new AnnualReportColumn { Index = 61, Title = "Imperial College London and Partners", IsBoolean = true };

        public AnnualReportColumn UniversityOfSouthampon = new AnnualReportColumn { Index = 62, Title = "University of Southampton and Partners", IsBoolean = true };

        public AnnualReportColumn KingsCollegeLondon = new AnnualReportColumn { Index = 63, Title = "Kings College London and Partners", IsBoolean = true };

        public AnnualReportColumn UniversityOfLeicester = new AnnualReportColumn { Index = 64, Title = "University of Leicester and Partners", IsBoolean = true };

        public AnnualReportColumn SpecialistsCentreUniveristyOfSouthampton = new AnnualReportColumn { Index = 65, Title = "Southampton and Partners Public Health Specialist Centre", IsBoolean = true };

        public AnnualReportColumn SpecialistCentreNewcastleUniversity = new AnnualReportColumn { Index = 66, Title = "Newcastle and Partners Public Health Specialist Centre", IsBoolean = true };

        public AnnualReportColumn SpecialistCentreLancasterUniversity = new AnnualReportColumn { Index = 67, Title = "Lancaster University and Partners Social Care Specialist Centre", IsBoolean = true };

        // Section C:

        public AnnualReportColumn MonetaryValueOfApplication = new AnnualReportColumn { Index = 68, Title = "Monetary value of funding application" };

        public AnnualReportColumn ProjectTeamLeadCareerStage = new AnnualReportColumn { Index = 69, Title = "Project team lead/lead applicant's career stage" }.WithValues("Early", "Mid", "Senior");

        public AnnualReportColumn ProjectTeamLeadOrganisation = new AnnualReportColumn { Index = 70, Title = "Project team lead/lead applicant’s employing organisation (if known)", IsOptional = true };

        public AnnualReportColumn ProjectTeamLeadOrganisationType = new AnnualReportColumn { Index = 71, Title = "Project team lead/lead applicant's employing organisation type" }.WithValues("HEI", "NHS Trust", "Local authority", "Voluntary or third sector", "SME", "Commercial organisation");

        public AnnualReportColumn WhereIsApplicantBased = new AnnualReportColumn { Index = 72, Title = "Where is the applicant being supported based?" }.WithValues("East Midlands", "West Midlands", "London", "North East", "North West", "South East", "South West", "Yorkshire and the Humber", "East of England");

        public AnnualReportColumn ProjectTeamLeadORCID = new AnnualReportColumn { Index = 73, Title = "Project team lead/lead applicant’s ORCID (if known)", IsOptional = true };

        // Section C: Professional Backgrounds

        public AnnualReportColumn ClinicalAcademic = new AnnualReportColumn { Index = 74, Title = "Clinical academic", IsBoolean = true };

        public AnnualReportColumn Academic = new AnnualReportColumn { Index = 75, Title = "Academic", IsBoolean = true };

        public AnnualReportColumn NonAcademicClinician = new AnnualReportColumn { Index = 76, Title = "Non-academic clinician", IsBoolean = true };

        public AnnualReportColumn NurseMidwife = new AnnualReportColumn { Index = 77, Title = "Nurse/Midwife/AHP/GP", IsBoolean = true };

        public AnnualReportColumn NHSManager = new AnnualReportColumn { Index = 78, Title = "NHS manager", IsBoolean = true };

        public AnnualReportColumn SocialCarePractitioner = new AnnualReportColumn { Index = 79, Title = "Social care practitioner", IsBoolean = true };

        public AnnualReportColumn PublicHealthPractitioner = new AnnualReportColumn { Index = 80, Title = "Public health practitioner", IsBoolean = true };

        public AnnualReportColumn SME = new AnnualReportColumn { Index = 81, Title = "SME", IsBoolean = true };

        public AnnualReportColumn NHSOther = new AnnualReportColumn { Index = 82, Title = "NHS other", IsBoolean = true };

        public AnnualReportColumn LivedExperience = new AnnualReportColumn { Index = 83, Title = "Lived experience/public/community contributor", IsBoolean = true };

        public AnnualReportColumn TeamCompositionOther = new AnnualReportColumn { Index = 84, Title = "Other - comment", IsOptional = true };

        public AnnualReportColumn TeamCompositionDontKnow = new AnnualReportColumn { Index = 85, Title = "Don't know", IsBoolean = true };

        // Section D:

        public AnnualReportColumn HasRSSProvidedAPIFGrant = new AnnualReportColumn { Index = 86, Title = "Has the RSS provided a Public Involvement Fund (PIF) grant to support this application?", IsBoolean = true };

        public AnnualReportColumn PIFGrantAmount = new AnnualReportColumn { Index = 87, Title = "Public Involvement Fund (PIF) grant amount (£)", IsOptional = true };


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Rendering

        List<Guid> SeenProjects = new List<Guid>();

        public async Task RenderProject(IServiceProvider serviceProvider, AnnualReportContext context, Project project)
        {
            if(SeenProjects.Contains(project.Id))
                throw new InvalidOperationException("Duplicate project Id: " + project.Id);
            SeenProjects.Add(project.Id);

            NextRow();

            var supportEvents = await serviceProvider.GetRequiredService<IProjectSupportRepository>().SelectProjectSupportByProjectId(project.Id);
            var submissions = await serviceProvider.GetRequiredService<IProjectSubmissionRepository>().SelectProjectSubmissionByProjectId(project.Id);

            if(context.Settings.ToDate.HasValue) { 
                supportEvents = supportEvents.Where(p => p.Date <= context.Settings.ToDate.Value).ToList(); // Filter out support events after the report end
                submissions = submissions.Where(p => p.SubmissionDate <= context.Settings.ToDate.Value).ToList(); // Filter out submissions after the report end
            }

            // Section A

            SetValue(ProjectID, project.PrefixedNumber);
            SetValue(DateOfFirstContact, project.DateOfRequest);
            SetValue(ClosureDate, project.ClosureDate);
            SetValue(NIHRApplicationID, Render_NIHRApplicationID(context, project, submissions));
            SetValue(ApplicationDestination, Render_ApplicationDestination(context, project));

            SetValue(ProjectSubmitted, Render_ProjectSubmitted(context, project, submissions), bypassFormatting: true);
            SetValue(SubmissionStage, Render_SubmissionStage(context, project, submissions));
            SetValue(Outcome, Render_Outcome(context, project, submissions));

            SetValue(IsPaidRSSLead, project.IsPaidRSSAdviserLeadId == IsPaidRSSAdviserLeadEnum.Yes);
            SetValue(IsPaidRSSCoApplicant, project.IsPaidRSSAdviserCoapplicantId == IsPaidRSSAdviserCoapplicantEnum.Yes);
            SetValue(WasPrePostAdvice, Render_PrePostAward(context, project, supportEvents));

            // Section B

            SetValue(TotalAmountOfSupport, Render_TotalAmountOfSupport(context, project, supportEvents));

            // From this point on we only need support events in the date range of the report
            { 
                var filteredSupportEvents = new List<ProjectSupport>();
                foreach(var supportEvent in supportEvents)
                {
                    if(context.Settings.FromDate.HasValue && supportEvent.Date < context.Settings.FromDate.Value)
                        continue;
                    if(context.Settings.ToDate.HasValue && supportEvent.Date > context.Settings.ToDate.Value)
                        continue;
                    filteredSupportEvents.Add(supportEvent);
                }
                supportEvents = filteredSupportEvents;
            }

            // Section B: Support Provided

            Render_SupportProvided(context, project, supportEvents,
                FormulatingResearchQuestions,
                QuantitativeResearchDesign,
                QualitativeResearchDesign,
                ResearchDesignForMixedMethods,
                ResearchDesignForTrials,
                CostingAndCostApproval,
                IdentifyingFundingSources,
                IdentifyingCollaborators,
                SignpostingForAdvice,
                EDI,
                PromotingPPI,
                AccessingRDApproval,
                ApplicationWritingSkills,
                CriticalReview,
                LiteratureReview,
                ImpactAdvice,
                IntellectualPropertyAdvice,
                CollaborationAgreementSetup,
                RecruitmentForTrials,
                MethodologyDevelopment,
                Randomisation,
                TrialManagement,
                AnalysisIncludingStatistical,
                StudyClose);
            SetValue(SupportDontKnow, "N");
            SetValue(SupportOther, Render_SupportProvidedOther(context, project, supportEvents));

            // Section B: Advisor Disciplines

            Render_AdvisorDisciplines(context, project, supportEvents,
                GeneralResearchMethodologist,
                SocialScientist,
                HealthEconomist,
                Statistician,
                Epidemiologist,
                ClinicalTrialist,
                InformationSpecialist,
                HealthPsychologist,
                PatientPublicInvolvement,
                LocalAuthorityOfficer,
                QualitativeResearcher);
            SetValue(AdvisorOther, GenerateAdvisorDisciplineOther(context, project, supportEvents));
            SetValue(AdvisorAdditionalInformation, String.Empty);
            SetValue(AdvisorDontKnow, "N");

            SetValue(NonNHSSetting, Render_NonNHSSetting(context, project));
            SetValue(IsPublicHealthStudy, Render_PublicHealthOrSocialCare(context, project));
            SetValue(WillThisStudyUseACTU, project.WillStudyUseCTUId == WillStudyUseCTUEnum.YesExternalToThisRSS || project.WillStudyUseCTUId == WillStudyUseCTUEnum.YesInternalToThisRSS);
            SetValue(IsThisCTUAssociatedWithTheRSS, project.WillStudyUseCTUId == WillStudyUseCTUEnum.YesInternalToThisRSS);

            SetValue(NewcastleUniversity, false);
            SetValue(UniversityOfBirmingham, false);
            SetValue(UniversityOfLancaster, false);
            SetValue(UniversityOfYork, false);
            SetValue(ImperialCollegeLondon, false);
            SetValue(UniversityOfSouthampon, false);
            SetValue(KingsCollegeLondon, false);
            SetValue(UniversityOfLeicester, true); // TRUE!
            SetValue(SpecialistsCentreUniveristyOfSouthampton, false);
            SetValue(SpecialistCentreNewcastleUniversity, false);
            SetValue(SpecialistCentreLancasterUniversity, false);

            SetValue(MonetaryValueOfApplication, project.MonetaryValueOfFundingApplication);

            Render_LeadApplicant(context, project);

            Render_ProfessionalBackgrounds(context, project,
                ClinicalAcademic,
                Academic,
                NonAcademicClinician,
                NurseMidwife,
                NHSManager,
                SocialCarePractitioner,
                PublicHealthPractitioner,
                SME,
                NHSOther,
                LivedExperience,
                TeamCompositionDontKnow);
            SetValue(TeamCompositionOther, project.ProfessionalBackgroundFreeText);

            SetValue(HasRSSProvidedAPIFGrant, false);
            SetValue(PIFGrantAmount, String.Empty);

            await Task.Delay(1);
        }

        string Render_ApplicationDestination(AnnualReportContext context, Project project)
        {
            var fundingStream = context.StaticData.FundingStream.FirstOrDefault(x => x.Id == project.ProposedFundingStreamId);
            var name = fundingStream?.Name ?? String.Empty;

            switch (name)
            {
                case "NIHR RfPB": return "RfPB";
                case "NIHR PDG": return "PDG";
                case "NIHR PRP": return "PRP";
                case "NIHR PGfAR": return "PGfAR";
                case "NIHR HTA": return "HTA";
                case "NIHR PHR": return "PHR";
                case "NIHR HSDR": return "HS&DR";
                case "NIHR EME": return "EME";
                case "NIHR i4i": return "i4i";
                case "NIHR Joint Funded": return "NIHR joint funded";
                case "Other Research Councils": return "Research Councils";

                case "Not Known": return "Unknown";
                case "NIHR Training Programmes": return "NIHR (other)";
                case "NIHR Global Health": return "NIHR (other)";
                case "DHSC": return "Other";
                case "Wellcome": return "Other";
                case "MRC": return "Other";
                case "UKRI": return "Other";
            }
            return name;
        }

        string Render_ProjectSubmitted(AnnualReportContext context, Project project, List<ProjectSubmission> submissions)
        {
            if(submissions.Count() == 0)
                return "N";
            return "Y";
        }

        bool Render_NonNHSSetting(AnnualReportContext context, Project project)
        {
            return Render_LeadApplicantEmployingOrganisationType(context, project).Contains("NHS");
        }

        string Render_SubmissionStage(AnnualReportContext context, Project project, List<ProjectSubmission> submissions)
        {
            if (submissions.Count() == 0)
                return "Not applicable";

            var latest = submissions.OrderByDescending(p => p.SubmissionDate).First();
            
            var submissionStage = context.StaticData.SubmissionStage.FirstOrDefault(x => x.Id == latest.SubmissionStageId);
            var name = submissionStage?.Name ?? String.Empty;

            switch (name)
            {
                case "Outline Stage (Stage 1)": return "Outline";
                case "Stage 2": return "Full";
                case "One Stage Application": return "One-stage";
            }
            return name;
        }

        string Render_NIHRApplicationID(AnnualReportContext context, Project project, List<ProjectSubmission> submissions)
        {
            if (submissions.Count() == 0)
                return "";

            var latest = submissions.OrderByDescending(p => p.SubmissionDate).First();
            return latest.NIHRReference;
        }

        string Render_Outcome(AnnualReportContext context, Project project, List<ProjectSubmission> submissions)
        {
            if (submissions.Count() == 0)
                return "Not submitted";

            var latest = submissions.OrderByDescending(p => p.SubmissionDate).First();
            if(latest.SubmissionOutcomeId == null)
                return "Unknown";

            var outcome = context.StaticData.SubmissionOutcome.FirstOrDefault(x => x.Id == latest.SubmissionOutcomeId);
            var name = outcome?.Name ?? String.Empty;

            switch (name)
            {
                case "Funded (full stage only)": return "Successful";
            }
            return name;
        }

        string Render_PrePostAward(AnnualReportContext context, Project project, List<ProjectSupport> supportEvents)
        {
            var pre = false;
            var post = false;

            foreach(var supportEvent in supportEvents)
            {
                if (supportEvent.IsPrePostAwardId == IsPrePostAwardEnum.PreAward)
                    pre = true;
                if (supportEvent.IsPrePostAwardId == IsPrePostAwardEnum.PostAward)
                    post = true;
            }

            if (pre && post)
                return "Pre- and post-award";
            if (pre) 
                return "Pre-award";
            if (post)
                return "Post-award without pre-award support";
            return String.Empty;
        }

        string Render_PublicHealthOrSocialCare(AnnualReportContext context, Project project)
        {
            if(project.PublicHealth)
                return "Public Health";
            if (project.SocialCare)
                return "Social Care";
            return String.Empty;
        }

        // Support

        string Render_TotalAmountOfSupport(AnnualReportContext context, Project project, List<ProjectSupport> supportEvents)
        {
            decimal totalMinutes = 0;
            foreach (var supportEvent in supportEvents)
            {
                var minutes = supportEvent.WorkTimeInHours * 60M * supportEvent.AdviserIds.Count();
                totalMinutes += minutes;
            }

            if (totalMinutes < 10 * 60)
                return "Low <10";
            if (totalMinutes <= 30 * 60)
                return "Medium  ≥ 10";
            return "High ≥ 30 hours";
        }

        void Render_SupportProvided(AnnualReportContext context, Project project, List<ProjectSupport> supportEvents, params AnnualReportColumn[] columns)
        {
            var supportProvided = GenerateSupportProvided(context, project, supportEvents);
            foreach (var column in columns)
            {
                SetValue(column, Render_SupportProvided(context, project, supportProvided, column.Title));
            }
        }

        string Render_SupportProvided(AnnualReportContext context, Project project, List<string> supportProvided, string title)
        {
            var checkExists = context.StaticData.SupportProvided.FirstOrDefault(p => String.IsNullOrEmpty(p.Alias) ? p.Name == title : p.Alias == title);
            if (checkExists == null)
                return "MISSING STATIC";

            return supportProvided.Contains(title) ? "Y" : "N";
        }

        string Render_SupportProvidedOther(AnnualReportContext context, Project project, List<ProjectSupport> supportEvents)
        {
            var result = new List<string>();

            foreach (var supportEvent in supportEvents)
            {
                foreach (var supportProvidedId in supportEvent.SupportProvidedIds)
                {
                    var supportProvided = context.StaticData.SupportProvided.FirstOrDefault(p => p.Id == supportProvidedId);
                    if (supportProvided == null)
                        continue; // This static has been deleted

                    if (supportProvided.Alias != "OTHER" || result.Contains(supportProvided.Name))
                        continue;

                    result.Add(supportProvided.Name);
                }
            }
            return String.Join(", ", result);
        }

        // Advisor Disciplines

        void Render_AdvisorDisciplines(AnnualReportContext context, Project project, List<ProjectSupport> supportEvents, params AnnualReportColumn[] columns)
        {
            var advisorDisciplines = GenerateAdvisorDisciplines(context, project, supportEvents);
            foreach (var column in columns)
            {
                SetValue(column, Render_AdvisorDisciplines(context, project, advisorDisciplines, column.Title));
            }
        }

        string Render_AdvisorDisciplines(AnnualReportContext context, Project project, List<string> advisorDisciplines, string title)
        {
            var checkExists = context.StaticData.UserDiscipline.FirstOrDefault(p => p.Name == title);
            if (checkExists == null)
                return "MISSING STATIC";

            return advisorDisciplines.Contains(title) ? "Y" : "N";
        }

        // Researcher

        void Render_LeadApplicant(AnnualReportContext context, Project project)
        {
            SetValue(ProjectTeamLeadCareerStage, Render_LeadApplicantCareerStage(context, project));
            SetValue(ProjectTeamLeadOrganisation, project.LeadApplicantOrganisation);
            SetValue(ProjectTeamLeadOrganisationType, Render_LeadApplicantEmployingOrganisationType(context, project));
            SetValue(WhereIsApplicantBased, Render_LeadApplicantLocation(context, project));
            SetValue(ProjectTeamLeadORCID, project.LeadApplicantORCID);
        }

        string Render_LeadApplicantCareerStage(AnnualReportContext context, Project project)
        {
            var careerStage = context.StaticData.ResearcherCareerStage.FirstOrDefault(x => x.Id == project.LeadApplicantCareerStageId);
            if (careerStage == null)
                return String.Empty;

            return careerStage.Name;
        }

        string Render_LeadApplicantEmployingOrganisationType(AnnualReportContext context, Project project)
        {
            var organisationType = context.StaticData.ResearcherOrganisationType.FirstOrDefault(x => x.Id == project.LeadApplicantOrganisationTypeId);
            if (organisationType == null)
                return String.Empty;

            var name = organisationType.Name;

            switch (name)
            {
                case "NHS": return "NHS Trust";
                case "University": return "HEI";
                case "Local Authority": return "Local authority";
                case "Company": return "Commercial organisation";
            }
            return name;
        }

        string Render_LeadApplicantLocation(AnnualReportContext context, Project project)
        {
            var location = context.StaticData.ResearcherLocation.FirstOrDefault(x => x.Id == project.LeadApplicantLocationId);
            if (location == null)
                return String.Empty;

            return location.Name;
        }

        // Professional Backgrounds

        void Render_ProfessionalBackgrounds(AnnualReportContext context, Project project, params AnnualReportColumn[] columns)
        {
            var professionalBackgrounds = GenerateProfessionalBackgrounds(context, project);
            foreach (var column in columns)
            {
                SetValue(column, Render_ProfessionalBackgrounds(context, project, professionalBackgrounds, column.Title));
            }
        }

        string Render_ProfessionalBackgrounds(AnnualReportContext context, Project project, List<string> professionalBackgrounds, string title)
        {
            var checkExists = context.StaticData.ProfessionalBackground.FirstOrDefault(p => AliasProfessionalBackground(p.Name) == title);
            if (checkExists == null)
                return "MISSING STATIC";

            return professionalBackgrounds.Contains(title) ? "Y" : "N";
        }

        string AliasProfessionalBackground(string input)
        {
            if (input == "Patient/public contributor")
                return "Lived experience/public/community contributor";
            return input;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Utility

        List<string> GenerateSupportProvided(AnnualReportContext context, Project project, List<ProjectSupport> supportEvents)
        {
            var result = new List<string>();

            foreach (var supportEvent in supportEvents)
            {
                foreach (var supportProvidedId in supportEvent.SupportProvidedIds)
                {
                    var supportProvided = context.StaticData.SupportProvided.FirstOrDefault(p => p.Id == supportProvidedId);
                    if (supportProvided == null)
                        continue; // This static has been deleted

                    if (supportProvided.Alias == "OTHER")
                        continue; // This item is added to Other - Comment

                    // Use the alias first and foremost
                    var text = supportProvided.Alias;
                    if (String.IsNullOrEmpty(text))
                        text = supportProvided.Name;

                    if (result.Contains(text))
                        continue;
                    result.Add(text);
                }
            }
            return result;
        }

        List<string> GenerateAdvisorDisciplines(AnnualReportContext context, Project project, List<ProjectSupport> supportEvents)
        {
            var result = new List<string>();
            var seen = new List<Guid>();

            foreach (var supportEvent in supportEvents)
            {
                foreach (var userId in supportEvent.AdviserIds)
                {
                    if (seen.Contains(userId))
                        continue;
                    seen.Add(userId);

                    var user = context.Users.FirstOrDefault(u => u.Id == userId);
                    if (user == null)
                        continue;
                    AppendAdvisorDisciplines(context, result, user.DisciplineIds);
                }
            }
            return result;
        }

        string GenerateAdvisorDisciplineOther(AnnualReportContext context, Project project, List<ProjectSupport> supportEvents)
        {
            var result = new List<string>();
            var seen = new List<Guid>();

            // We've had to call this twice!
            var adviserDisciplines = GenerateAdvisorDisciplines(context, project, supportEvents);
            if (adviserDisciplines.Contains("Mixed methods"))
                result.Add("Mixed methods");
            if (adviserDisciplines.Contains("EDIT"))
                result.Add("EDI");

            foreach (var supportEvent in supportEvents)
            {
                foreach (var userId in supportEvent.AdviserIds)
                {
                    if (seen.Contains(userId))
                        continue;
                    seen.Add(userId);

                    var user = context.Users.FirstOrDefault(u => u.Id == userId);
                    if (user == null)
                        continue;

                    if (!String.IsNullOrEmpty(user.DisciplineFreeText) && !result.Contains(user.DisciplineFreeText))
                        result.Add(user.DisciplineFreeText);
                }
            }
            return String.Join(", ", result);
        }

        void AppendAdvisorDisciplines(AnnualReportContext context, List<string> result, List<Guid> advisorDisciplineIds)
        {
            foreach (var advisorDisciplineId in advisorDisciplineIds)
            {
                var advisorDiscipline = context.StaticData.UserDiscipline.FirstOrDefault(a => a.Id == advisorDisciplineId);
                if (advisorDiscipline == null)
                    continue;
                
                var text = advisorDiscipline.Name;

                if (result.Contains(text))
                    continue;
                result.Add(text);
            }
        }

        List<string> GenerateProfessionalBackgrounds(AnnualReportContext context, Project project)
        {
            var result = new List<string>();
            foreach (var professionalBackgroundId in project.ProfessionalBackgroundIds)
            {
                var rpb = context.StaticData.ProfessionalBackground.FirstOrDefault(p => p.Id == professionalBackgroundId);
                if (rpb == null)
                    continue; // This static has been deleted

                // Use the alias first and foremost
                var text = AliasProfessionalBackground(rpb.Name);

                if (result.Contains(text))
                    continue;
                result.Add(text);
            }
            return result;
        }
    }
}
