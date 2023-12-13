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
        // public Guid ReportProviderId { get; set; }

        // Columns

        public List<ReportColumn> Columns { get; set; } = new List<ReportColumn>();

        public Guid LatestColumnId { get; set; }

        internal ReportColumn? GetColumn(Guid id)
        {
            return Columns.FirstOrDefault(c => c.Id == id);
        }

        internal ReportColumn InsertColumn(ReportColumn column)
        {
            if (column.Id == Guid.Empty)
                column.Id = Guid.NewGuid();

            Columns.Add(column);
            LatestColumnId = column.Id;
            return column;
        }

        internal void DeleteColumn(Guid id)
        {
            var column = GetColumn(id);
            if (column == null)
                return;
            Columns.Remove(column);
        }

        // Filters

        public List<ReportFilter> Filters { get; set; } = new List<ReportFilter>();

        public Guid LatestFilterId { get; set; }

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

                if (filter is ReportFilterGroup group)
                {
                    var result = GetFilterWithGroup(id, group);
                    if (result != null)
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

        internal ReportFilter InsertFilter(ReportFilter filter, ReportFilterGroup? group)
        {
            if (filter.Id == Guid.Empty)
                filter.Id = Guid.NewGuid();

            if (group != null)
                group.Filters.Add(filter);
            else
                Filters.Add(filter);

            LatestFilterId = filter.Id;
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
            {
                if (!column.ValidateColumn(schema))
                    column.FieldId = Guid.Empty; // Flag for deletion
            }
            Columns.RemoveAll(c => c.FieldId == Guid.Empty);

            foreach (var filter in Filters)
            {
                filter.ValidateFilter(schema);
            }
        }
    }
}
