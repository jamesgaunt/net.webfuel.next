using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Excel;

namespace Webfuel.Domain
{
    class AnnualReportWorkbook
    {
        public ExcelWorkbook Workbook;

        public DetailsWorksheet Details;
        public ApplicationSupportWorksheet ApplicationSupport;
        public WorkforceWorksheet Workforce;

        public AnnualReportWorkbook()
        {
            Workbook = new ExcelWorkbook();
            Details = new DetailsWorksheet(Workbook);
            ApplicationSupport = new ApplicationSupportWorksheet(Workbook);
            Workforce = new WorkforceWorksheet(Workbook);
        }
    }
}
