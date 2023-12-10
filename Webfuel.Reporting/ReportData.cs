using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    public class ReportData
    {
        public List<ReportRow> Rows => _rows;
        List<ReportRow> _rows = new List<ReportRow>();

        public ReportRow AddRow()
        {
            var row = new ReportRow();
            _rows.Add(row);
            return row;
        }
    }

    public class ReportRow
    {
        public List<ReportCell> Cells => _cells;
        List<ReportCell> _cells = new List<ReportCell>();

        public ReportCell AddCell()
        {
            var cell = new ReportCell();
            _cells.Add(cell);
            return cell;
        }
    }

    public class ReportCell
    {
        public object? Value { get; set; }
    }

}
