using DocumentFormat.OpenXml.Office.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Excel
{
    public class ExcelData
    {
        public IReadOnlyList<ExcelDataRow> Rows => _rows;

        public ExcelDataRow AddRow()
        {
            var row = new ExcelDataRow(this);
            _rows.Add(row);
            return row;
        }

        // Implementation

        internal List<ExcelDataRow> _rows { get; } = new List<ExcelDataRow>();
    }

    public class ExcelDataRow
    {
        internal readonly ExcelData _data;

        internal ExcelDataRow(ExcelData data)
        {
            _data = data;
            Row = _data._rows.Count() + 1;
        }

        public int Row { get; }

        public IReadOnlyList<ExcelDataCell> Cells => _cells;

        public ExcelDataCell AddCell(object? value = null)
        {
            var cell = new ExcelDataCell(this);
            cell.Value = value;

            _cells.Add(cell);
            return cell;
        }

        // Implementation

        internal List<ExcelDataCell> _cells { get; } = new List<ExcelDataCell>();
    }

    public class ExcelDataCell
    {
        internal ExcelDataRow _row;

        internal ExcelDataCell(ExcelDataRow row)
        {
            _row = row;
            Col = _row._cells.Count + 1;
        }

        public object? Value { get; set; }

        public int Col { get; }
    }
}
