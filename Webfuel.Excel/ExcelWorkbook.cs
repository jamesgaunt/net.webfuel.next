using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Drawing;

namespace Webfuel.Excel
{
    public class ExcelWorkbook : IDisposable
    {
        internal readonly XLWorkbook _workbook;

        public ExcelWorkbook()
        {
            _workbook = new XLWorkbook();
        }

        internal ExcelWorkbook(XLWorkbook workbook)
        {
            _workbook = workbook;
        }

        public ExcelWorksheet AddWorksheet(string sheetName)
        {
            var worksheet = _workbook.AddWorksheet(sheetName);
            return new ExcelWorksheet(worksheet);
        }

        public ExcelWorksheet Worksheet(string sheetName)
        {
            var worksheet = _workbook.Worksheet(sheetName);
            return new ExcelWorksheet(worksheet);
        }

        public ExcelWorksheet Worksheet(int position)
        {
            var worksheet = _workbook.Worksheet(position);
            return new ExcelWorksheet(worksheet);
        }

        public ExcelWorksheet GetOrCreateWorksheet(string? sheetName = null)
        {
            if (string.IsNullOrEmpty(sheetName))
                sheetName = "Sheet1";

            if (!_workbook.TryGetWorksheet(sheetName, out var _worksheet))
                _worksheet = _workbook.AddWorksheet(sheetName);

            return Worksheet(sheetName);
        }

        public ExcelWorksheet GetWorksheet(string? sheetName = null)
        {
            if (string.IsNullOrEmpty(sheetName))
                sheetName = "Sheet1";

            if (!_workbook.TryGetWorksheet(sheetName, out var _worksheet))
                throw new InvalidOperationException("Unable to get worksheet " + sheetName);

            return Worksheet(sheetName);
        }

        public MemoryStream ToMemoryStream()
        {
            var ms = new MemoryStream();
            _workbook.SaveAs(ms);
            ms.Position = 0;
            return ms;
        }

        public static ExcelWorkbook Load(string filename)
        {
            var workbook = new XLWorkbook(filename);
            return new ExcelWorkbook(workbook);
        }

        public static ExcelWorkbook Load(Stream stream)
        {
            var workbook = new XLWorkbook(stream);
            return new ExcelWorkbook(workbook);
        }

        public void Dispose()
        {
            _workbook.Dispose();
        }
    }

    public class ExcelWorksheet
    {
        internal readonly IXLWorksheet _worksheet;

        internal ExcelWorksheet(IXLWorksheet worksheet)
        {
            _worksheet = worksheet;
        }

        public string Name 
        {
            get => _worksheet.Name;
            set => _worksheet.Name = value;
        }

        public int Position
        {
            get => _worksheet.Position;
            set => _worksheet.Position = value;
        }

        public ExcelCell Merge(string topLeft, string bottomRight, bool border)
        {
            var range = _worksheet.Range(topLeft, bottomRight).Merge();

            if (border)
                range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            return Cell(topLeft);
        }

        public ExcelCell Cell(string cellAddress)
        {
            var cell = _worksheet.Cell(cellAddress);
            return new ExcelCell(cell);
        }

        public ExcelCell Cell(int row, int col)
        {
            var cell = _worksheet.Cell(row, col);
            return new ExcelCell(cell);
        }

        public ExcelCell Cell(int row, string header)
        {
            return Cell(row, FindColumnIndex(header));
        }

        public ExcelColumn Column(int col)
        {
            return new ExcelColumn(_worksheet.Column(col));
        }

        public ExcelRow Row(int row)
        {
            return new ExcelRow(_worksheet.Row(row));
        }

        public int FindColumnIndex(string header)
        {
            var col = 1;

            header = header.Trim();

            while (true)
            {
                if (_worksheet.Cell(1, col).TryGetValue(out string value))
                {
                    if (value.Trim() == header)
                        return col;
                }

                col++;
                if (col > 100)
                    return -1;
            }
        }

        public bool IsRowEmpty(int row, int colsToCheck = 1)
        {
            for (var col = 1; col <= colsToCheck; col++)
            {
                var value = _worksheet.Cell(row, col).GetString().Trim();
                if (!String.IsNullOrEmpty(value))
                    return false;
            }
            return true;
        }
    }

    public class ExcelRow
    {
        internal readonly IXLRow _row;

        internal ExcelRow(IXLRow row)
        {
            _row = row;
        }

        public ExcelRow SetHeight(double height)
        {
            _row.Height = height;
            return this;
        }

        public ExcelRow SetTextRotation(int value)
        {
            _row.Style.Alignment.TextRotation = value;
            return this;
        }

        public ExcelRow CentreAlign()
        {
            _row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            return this;
        }
    }

    public class ExcelColumn
    {
        internal readonly IXLColumn _column;

        internal ExcelColumn(IXLColumn column)
        {
            _column = column;
        }

        public ExcelColumn AdjustToContents()
        {
            _column.AdjustToContents();
            return this;
        }

        public ExcelColumn SetWidth(double width)
        {
            _column.Width = width;
            return this;
        }

        public ExcelColumn SetBold(bool value)
        {
            _column.Style.Font.Bold = value;
            return this;
        }
    }

    public class ExcelCell
    {
        internal readonly IXLCell _cell;

        internal ExcelCell(IXLCell cell)
        {
            _cell = cell;
        }

        public T GetValue<T>()
        {
            return _cell.GetValue<T>();
        }

        public bool TryGetValue<T>(out T result)
        {
            return _cell.TryGetValue<T>(out result);
        }

        public ExcelCell SetValue(string? value)
        {
            _cell.SetValue(value ?? String.Empty);
            return this;
        }

        public ExcelCell SetValue(int value)
        {
            _cell.SetValue(value);
            return this;
        }

        public ExcelCell SetValue(double value)
        {
            _cell.SetValue(value);
            return this;
        }

        public ExcelCell SetValue(bool value)
        {
            _cell.SetValue(value);
            return this;
        }

        public ExcelCell SetValue(DateTime value)
        {
            _cell.SetValue(value);
            return this;
        }

        public ExcelCell SetValue(object? value)
        {
            if (value == null)
                return Clear();

            if (value is String s)
                return SetValue(s);

            if (value is bool b)
                return SetValue(b);

            if (value is int i)
                return SetValue(i);

            if (value is Decimal dc)
                return SetValue(Decimal.ToDouble(dc));

            if (value is DateTime dt)
                return SetValue(dt);

            if (value is DateOnly donly)
                return SetValue(donly.ToDateTime(TimeOnly.MinValue));

            if (value is DateTimeOffset doffset)
                return SetValue(doffset.LocalDateTime);

            if (IsNumericType(value.GetType()))
                return SetValue((double)value);

            return SetValue(value.ToString());
        }

        public ExcelCell Clear()
        {
            _cell.Clear();
            return this;
        }

        // Style

        public bool Bold
        {
            get { return _cell.Style.Font.Bold; }
            set { _cell.Style.Font.Bold = value; }
        }

        public ExcelCell SetBold(bool value)
        {
            Bold = value;
            return this;
        }

        public bool Italic
        {
            get { return _cell.Style.Font.Italic; }
            set { _cell.Style.Font.Italic = value; }
        }

        public ExcelCell SetItalic(bool value)
        {
            Italic = value;
            return this;
        }

        public ExcelCell SetNumberFormat(string format)
        {
            _cell.Style.NumberFormat.Format = format;
            return this;
        }

        public int TextRotation
        {
            get { return _cell.Style.Alignment.TextRotation; }
            set { _cell.Style.Alignment.TextRotation = value; }
        }

        public ExcelCell SetTextRotation(int value)
        {
            TextRotation = value;
            return this;
        }

        public ExcelCell HorizontalCentreAlign()
        {
            _cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            return this;
        }

        public ExcelCell VerticalCentreAlign()
        {
            _cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            return this;
        }

        public ExcelCell SetBackgroundColor(System.Drawing.Color color)
        {
            _cell.Style.Fill.BackgroundColor = XLColor.FromColor(color);
            return this;
        }

        public ExcelCell SetWidth(double? width)
        {
            if (width == null)
                return this;
            _cell.WorksheetColumn().Width = width.Value;
            return this;
        }

        public ExcelCell SetHeight(double? height)
        {
            if (height == null)
                return this;
            _cell.WorksheetRow().Height = height.Value;
            return this;
        }

        public ExcelCell SetTextWrap(bool value)
        {
            _cell.Style.Alignment.WrapText = value;
            return this;
        }

        public ExcelCell SetOutsideBorderThin()
        {
            _cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            return this;
        }

        public ExcelCell SetBottomBorderThick()
        {
            _cell.Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            return this;
        }

        // Helpers

        static bool IsNumericType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}
