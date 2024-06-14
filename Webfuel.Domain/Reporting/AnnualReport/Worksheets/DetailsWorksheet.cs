using DocumentFormat.OpenXml.Spreadsheet;
using Webfuel.Excel;

namespace Webfuel.Domain
{
    internal class DetailsWorksheet: AnnualReportWorksheet
    {
        public DetailsWorksheet(ExcelWorkbook workbook) : base(workbook, "NIHR RSS Details")
        {
            Initialise();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Initialise

        protected override void Initialise()
        {
            base.Initialise();

            Worksheet.Column(1).SetWidth(62.91);
            Worksheet.Column(2).SetWidth(48.09);

            Worksheet.Cell("A1").SetValue("NIHR Research Support Service Workforce & Metrics Report 2023-24").SetBackgroundColor(AnnualReportUtility.GreyBG).SetBold(true);

            Worksheet.Cell("A3").SetValue("Name of Research Support Service Hub");
            Worksheet.Cell("B3").SetValue("Imperial College London and Partners").SetOutsideBorderThin();

            Worksheet.Cell("A5").SetValue("Contact details for Sign-off (DocuSign) [Director of the NIHR RSS Hub]").SetBold(true);
            Worksheet.Cell("A6").SetValue("Name");
            Worksheet.Cell("A7").SetValue("Email");
            Worksheet.Cell("A8").SetValue("Date submitted");
            Worksheet.Cell("B6").SetValue("").SetOutsideBorderThin();
            Worksheet.Cell("B7").SetValue("").SetOutsideBorderThin();
            Worksheet.Cell("B8").SetValue("").SetOutsideBorderThin();

            Worksheet.Cell("A10").SetValue("Contact details of a person to whom any queries could be made").SetBold(true);
            Worksheet.Cell("A11").SetValue("Name");
            Worksheet.Cell("A12").SetValue("Email");
            Worksheet.Cell("B11").SetValue("").SetOutsideBorderThin();
            Worksheet.Cell("B12").SetValue("").SetOutsideBorderThin();

            var blurb = "Our purpose for collecting this data is to evaluate the management and delivery of the NIHR award. The information collected here about you and those supported through the NIHR award will allow us to evaluate the management and delivery of the NIHR award. You are responsible for informing the members of your team that their information will be submitted to the NIHR for the purposes of evaluating the management of the award.\r\n\r\nThe Central Commissioning Facility (CC) is part of the Department for Health and Social Care (DHSC) National Institute for Health Research (NIHR). The contracting agent for CC is LGC.  The DHSC is the data controller and LGC is the data processor under the General Data Protection Regulation (GDPR) EC 2016/679.  The NIHR and its contracting agents respect the privacy of individuals who share their data and process it in a manner that meets the requirements of the GDPR. The data we collect here is collected in the public interest. Information provided here may be subject to Freedom of Information requests.\r\n\r\nThe NIHR privacy policy (https://www.nihr.ac.uk/privacy-policy.htm) includes further information including ways we may use your data, NIHR contact details and details on your individual rights regarding how your data is used. The data submitted may be shared across the NIHR, including with other NIHR coordinating centres, to allow the establishment of a collaboration, provide funding opportunities and for statistical analysis.";

            Worksheet.Merge("A14", "B14", false).SetValue(blurb).SetHeight(228.0).SetTextWrap(true);
        }
    }
}
