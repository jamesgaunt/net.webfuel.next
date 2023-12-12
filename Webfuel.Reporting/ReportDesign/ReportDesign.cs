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

        internal ReportFilter? GetFilter(Guid id)
        {
            return GetFilterWithGroup(id)?.filter;
        }

        internal (ReportFilter filter, ReportFilterGroup? group)? GetFilterWithGroup(Guid id)
        {
            foreach (var filter in Filters)
            {
                if (filter.Id == id)
                    return (filter, null);

                if(filter is ReportFilterGroup group)
                {
                    var result = GetFilterWithGroup(id, group);
                    if(result != null)
                        return result;
                }
            }
            return null;
        }

        (ReportFilter filter, ReportFilterGroup group)? GetFilterWithGroup(Guid id, ReportFilterGroup parent)
        {
            foreach (var filter in parent.Filters)
            {
                if (filter.Id == id)
                    return (filter, parent);

                if (filter is ReportFilterGroup group)
                {
                    var result = GetFilterWithGroup(id, group);
                    if (result != null)
                        return result;
                }
            }
            return null;
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
            var filter = GetFilterWithGroup(id);
            if (filter == null)
                throw new InvalidOperationException("The specified filter does not exist");

            if (filter.Value.group == null)
                Filters.RemoveAt(Filters.FindIndex(p => p.Id == filter.Value.filter.Id));
            else
                filter.Value.group.Filters.RemoveAt(filter.Value.group.Filters.FindIndex(p => p.Id == filter.Value.filter.Id));
        }

        // Validation

        public void ValidateDesign(ReportSchema schema)
        {
            foreach (var column in Columns)
                column.ValidateColumn(schema);

            foreach (var filter in Filters)
                filter.ValidateFilter(schema);
        }


    }
}
