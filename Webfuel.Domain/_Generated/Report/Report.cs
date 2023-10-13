using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class Report
    {
        public Report() { }
        
        public Report(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(Report.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(Report.Name):
                        Name = (string)value!;
                        break;
                    case nameof(Report.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(Report.ReportingContextId):
                        ReportingContextId = (Guid)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public int SortOrder  { get; set; } = 0;
        public Guid ReportingContextId  { get; set; } = Guid.Empty;
        public Report Copy()
        {
            var entity = new Report();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            entity.ReportingContextId = ReportingContextId;
            return entity;
        }
    }
}

