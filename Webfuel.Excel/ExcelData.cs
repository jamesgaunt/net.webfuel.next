using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Excel
{
    public class ExcelData
    {
        public List<ExcelDataRow> Rows { get; } = new List<ExcelDataRow>();


    }

    public class ExcelDataRow
    {
        public required int Row { get; init; }

        public List<ExcelDataCell> Cells { get; } = new List<ExcelDataCell>();
    }

    public class ExcelDataCell
    {
        public required object? Value { get; init; }

        public required int Col { get; init; }
    }
}
