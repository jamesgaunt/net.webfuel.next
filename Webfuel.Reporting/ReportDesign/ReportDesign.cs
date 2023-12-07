using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    [ApiType]
    public class ReportDesign
    {
        // Columns

        public List<ReportColumn> Columns { get; set; } = new List<ReportColumn>();

        internal ReportColumn GetColumn(Guid id)
        {
            return Columns.FirstOrDefault(c => c.Id == id) ?? throw new InvalidOperationException("The specified column does not exist");
        }

        internal ReportColumn InsertColumn(ReportColumn column)
        {
            if (column.Id == Guid.Empty)
                column.Id = Guid.NewGuid();

            Columns.Add(column);
            return column;
        }

        internal void DeleteColumn(Guid id)
        {
            Columns.Remove(GetColumn(id));
        }

        // Filters

        public List<ReportFilter> Filters { get; set; } = new List<ReportFilter>();

        internal ReportFilter GetFilter(Guid id)
        {
            return GetFilterWithParent(id).filter;
        }
        internal (ReportFilter filter, List<ReportFilter> parent) GetFilterWithParent(Guid id)
        {
            foreach (var filter in FilterIterator())
            {
                if (filter.filter.Id == id)
                    return filter;
            }
            throw new InvalidOperationException("The specified filter does not exist");
        }

        internal ReportFilter InsertFilter(ReportFilter filter)
        {
            if (filter.Id == Guid.Empty)
                filter.Id = Guid.NewGuid();

            Filters.Add(filter);
            return filter;
        }

        internal void DeleteFilter(Guid id)
        {
            var filter = GetFilterWithParent(id);
            filter.parent.Remove(filter.filter);
        }

        // Validation

        public void ValidateDesign(ReportSchema schema)
        {
            foreach (var column in Columns)
                column.ValidateColumn(schema);

            foreach (var filter in Filters)
                filter.ValidateFilter(schema);
        }

        // Helpers

        IEnumerable<(ReportFilter filter, List<ReportFilter> parent)> FilterIterator()
        {
            foreach(var filter in FilterIterator(Filters))
                yield return filter;
        }

        IEnumerable<(ReportFilter filter, List<ReportFilter> parent)> FilterIterator(List<ReportFilter> filters)
        {
            foreach (var filter in Filters)
            {
                yield return (filter, Filters);

                if (filter is ReportFilterGroup group)
                {
                    foreach (var child in FilterIterator(group.Filters))
                        yield return child;
                }
            }
        }
    }
}
