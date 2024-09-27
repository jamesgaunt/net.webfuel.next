using DocumentFormat.OpenXml.InkML;
using Webfuel.Excel;

namespace Webfuel.Domain
{
    internal class WorkforceWorksheet: AnnualReportWorksheet
    {
        public WorkforceWorksheet(ExcelWorkbook workbook) : base(workbook, "Workforce")
        {
            HeaderRow = 2;
            DarkHeader = true;
            Initialise();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Initialise

        protected override void Initialise()
        {
            base.Initialise();

            MergeCell("A1", "L1", "Workforce", AnnualReportUtility.GreyBG);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Schema

        public AnnualReportColumn Title = new AnnualReportColumn { Index = 1, Title = "Title" };

        public AnnualReportColumn Surname = new AnnualReportColumn { Index = 2, Title = "Surname" };

        public AnnualReportColumn FirstName = new AnnualReportColumn { Index = 3, Title = "First Name" };

        public AnnualReportColumn JobTitle = new AnnualReportColumn { Index = 4, Title = "Job Title" };

        public AnnualReportColumn RSSFundedFTE = new AnnualReportColumn { Index = 5, Title = "RSS Funded FTE" };

        public AnnualReportColumn Email = new AnnualReportColumn { Index = 6, Title = "Email" };

        public AnnualReportColumn ProfessionalBackground = new AnnualReportColumn { Index = 7, Title = "Professional Background" }.WithValues("Allied Healthcare Professional", "Doctor", "Dentist", "Midwife", "Psychologist", "Nurse", "Public Health Specialist", "Social Care Specialist", "Other Healthcare Professional", "Not Healthcare Professional");

        public AnnualReportColumn ProfessionalBackgroundDetail = new AnnualReportColumn { Index = 8, Title = "Professional Background Detail (if AHP, OHP or NHP)" };

        public AnnualReportColumn ProfessionalBackgroundFreeText = new AnnualReportColumn { Index = 9, Title = "Other - Comment", IsOptional = true };

        public AnnualReportColumn RSSStaffRole = new AnnualReportColumn { Index = 10, Title = "RSS staff role" }.WithValues(
            "Director", 
            "Co-Director/Deputy or Associate Director", 
            "Advisor- non specialist", 
            "Advisor- specialist", 
            "Business Support - manager or administrator",
            "Advisory - member of executive/advisory committee", 
            "Health Economist", 
            "Statistician", 
            "Social worker", 
            "PPIE lead/advisor", 
            "Other - please specify");

        public AnnualReportColumn OtherRSSStaffRole = new AnnualReportColumn { Index = 11, Title = "Other - Comment", IsOptional = true };

        public AnnualReportColumn NumberOfYearsEmployed = new AnnualReportColumn { Index = 12, Title = "Number of Years Employed by RSS" }.WithValues("< 1 year", "1-3 years", "3-6 years");

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Rendering
        
        public async Task RenderUser(IServiceProvider serviceProvider, AnnualReportContext context, User user)
        {
            NextRow();

            SetValue(Title, user.Title);
            SetValue(Surname, user.LastName);
            SetValue(FirstName, user.FirstName);
            SetValue(JobTitle, user.StaffRole);
            SetValue(RSSFundedFTE, user.FullTimeEquivalentForRSS);
            SetValue(Email, user.Email);

            SetValue(ProfessionalBackground, Render_ProfessionalBackground(context, user));
            SetValue(ProfessionalBackgroundDetail, Render_ProfessionalBackgroundDetail(context, user));
            SetValue(ProfessionalBackgroundFreeText, user.ProfessionalBackgroundFreeText);
            SetValue(RSSStaffRole, Render_RSSStaffRole(context, user));
            SetValue(OtherRSSStaffRole, String.Empty);
            SetValue(NumberOfYearsEmployed, Render_NumberOfYearsEmployed(context, user));

            await Task.Delay(1);
        }

        string Render_ProfessionalBackground(AnnualReportContext context, User user)
        {
            return String.Empty;
        }

        string Render_ProfessionalBackgroundDetail(AnnualReportContext context, User user)
        {
            return String.Empty;
        }

        string Render_RSSStaffRole(AnnualReportContext context, User user)
        {
            return String.Empty;
        }

        string Render_NumberOfYearsEmployed(AnnualReportContext context, User user)
        {
            if (user.StartDateForRSS == null)
                return String.Empty;

            var startDate = user.StartDateForRSS.Value;
            var endDate = user.EndDateForRSS ?? DateOnly.FromDateTime(DateTime.Now);

            var days = (float)endDate.DayNumber - (float)startDate.DayNumber;
            var years = days / 365f;

            if (years < 1f)
                return "< 1 year";
            if (years < 3f)
                return "1-3 years";
            return "3-6 years";
        }
    }
}
