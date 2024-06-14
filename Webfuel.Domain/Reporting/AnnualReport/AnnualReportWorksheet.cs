using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Excel;

namespace Webfuel.Domain
{
    internal abstract class AnnualReportWorksheet
    {
        public ExcelWorksheet Worksheet;

        public int HeaderRow { get; set; } = 1;

        public bool DarkHeader { get; set; }

        public int ActiveRow { get; set; }

        public AnnualReportWorksheet(ExcelWorkbook workbook, string sheetName)
        {
            Worksheet = workbook.AddWorksheet(sheetName);
        }

        protected virtual void Initialise()
        {
            foreach (var column in Columns)
                HeaderCell(column);
        }

        void HeaderCell(AnnualReportColumn column)
        {
            if (Worksheet == null)
                throw new Exception("Worksheet is null");

            var cell = Worksheet.Cell(HeaderRow, column.Index);

            cell.SetValue(column.Title);
            cell.SetWidth(20.36);
            cell.SetHeight(103.5);
            cell.HorizontalCentreAlign();
            cell.VerticalCentreAlign();
            cell.SetTextWrap(true);

            cell.SetBackgroundColor(DarkHeader == true ? AnnualReportUtility.DarkBlueBG : AnnualReportUtility.LightBlueBG);
            cell.SetOutsideBorderThin();
            cell.SetBottomBorderThick();
        }

        protected void MergeCell(string start, string end, string value, System.Drawing.Color bg)
        {
            if (Worksheet == null)
                throw new Exception("Worksheet is null");

            Worksheet.Merge(start, end, true)
                .SetHeight(25.5)
                .SetBold(true)
                .SetBackgroundColor(bg)
                .SetValue(value);
        }

        // Row Rendering

        protected void NextRow()
        {
            if (ActiveRow == 0)
                ActiveRow = HeaderRow + 1;
            else
                ActiveRow++;
        }

        protected virtual void SetValue(AnnualReportColumn column, object? value, bool bypassFormatting = false)
        {
            if (bypassFormatting)
            {
                Worksheet.Cell(ActiveRow, column.Index).SetValue(value);
                return;

            }

            if (value is Boolean b)
            {
                Worksheet.Cell(ActiveRow, column.Index).SetValue(b ? "Y" : "N").HorizontalCentreAlign();
                if (b) 
                { 
                    Worksheet.Cell(ActiveRow, column.Index).SetBackgroundColor(AnnualReportUtility.LightGreenBG);
                }
                return;
            }

            if(value is String s && (s == "Y" || s == "N"))
            {
                Worksheet.Cell(ActiveRow, column.Index).SetValue(s).HorizontalCentreAlign();
                if (s == "Y")
                {
                    Worksheet.Cell(ActiveRow, column.Index).SetBackgroundColor(AnnualReportUtility.LightGreenBG);
                }
                return;
            }

            Worksheet.Cell(ActiveRow, column.Index).SetValue(value);

            if(value is Decimal || value is Decimal? || value is Double || value is Double?)
                Worksheet.Cell(ActiveRow, column.Index).SetNumberFormat("#,##0.00;-#,##0.00");
        }

        // Validating

        public bool IsInvalid { get; set; }

        public virtual void Validate()
        {
            IsInvalid = false;
            for (var i = HeaderRow + 1; i <= ActiveRow; i++)
                ValidateRow(i);
        }

        protected virtual void ValidateRow(int row)
        {
            foreach (var column in Columns)
                ValidateCell(row, column);
        }

        protected virtual void ValidateCell(int row, AnnualReportColumn column)
        {
            var stringValue = Worksheet.Cell(row, column.Index).GetValue<string>();

            if(stringValue == "MISSING STATIC")
            {
                InvalidateCell(row, column);
                return;
            }

            if (column.IsOptional && string.IsNullOrWhiteSpace(stringValue))
                return;

            if (column.IsBoolean)
            {
                if (stringValue != "Y" && stringValue != "N")
                    InvalidateCell(row, column);
                return;
            }

            if(column.Values != null)
            {
                if (!column.Values.Contains(stringValue)) 
                    InvalidateCell(row, column);
                return;
            }

            if (string.IsNullOrWhiteSpace(stringValue))
            {
                InvalidateCell(row, column);
                return;
            }
        }

        public virtual void InvalidateCell(int row, AnnualReportColumn column)
        {
            Worksheet.Cell(row, column.Index).SetBackgroundColor(AnnualReportUtility.RedBG);
            IsInvalid = true;
        }

        protected List<AnnualReportColumn> Columns
        {
            get
            {
                if (_columns != null)
                    return _columns;

                var items = this.GetType().GetMembers()
                        .Where(m => m.MemberType == MemberTypes.Field)
                        .Select(m => m as FieldInfo)
                        .Where(f => f?.FieldType == typeof(AnnualReportColumn))
                        .Select(f => f!.GetValue(this) as AnnualReportColumn)
                        .OrderBy(c => c!.Index)
                        .ToList();

                var result = new List<AnnualReportColumn>(items.Count);
                foreach (var item in items)
                {
                    if (item == null)
                        continue;
                    result.Add(item);
                }

                for (var i = 0; i < result.Count; i++)
                {
                    if (result[i].Index != i + 1)
                        throw new InvalidOperationException($"Annual report column index mismatch at index {i + 1}");
                }

                return _columns = result;
            }
        }
        List<AnnualReportColumn>? _columns = null;
    }
}
