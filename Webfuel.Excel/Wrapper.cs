using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Webfuel.Excel
{
    public class ExcelWorkbook
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
    }

    public struct ExcelWorksheet
    {
        internal readonly IXLWorksheet _worksheet;

        internal ExcelWorksheet(IXLWorksheet worksheet)
        {
            _worksheet = worksheet;
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

        public int FindHeaderCol(string header)
        {
            var col = 1;

            header = header.Trim();

            while (true)
            {
                if(_worksheet.Cell(1, col).TryGetValue(out string value))
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
            for(var col = 1; col <= colsToCheck; col++)
            {
                var value = _worksheet.Cell(row, col).GetString().Trim();
                if (!String.IsNullOrEmpty(value))
                    return false;
            }
            return true;
        }
    }

    public struct ExcelCell
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

        public ExcelCell SetValue(string value)
        {
            _cell.SetValue(value);
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
            if(value == null)
                return Clear();

            if(value is String)
                return SetValue((String)value);

            if(value is bool)
                return SetValue((bool)value);

            if(value is int)
                return SetValue((int)value);

            if(value is DateTime)
                return SetValue((DateTime)value);

            if(value is Decimal || IsNumericType(value.GetType()))
                return SetValue((Decimal)value);

            return SetValue("ERROR: Unable to map object value");
        }

        public ExcelCell Clear()
        {
            _cell.Clear();
            return this;
        }

        // Helpers

        bool IsNumericType(Type type)
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
