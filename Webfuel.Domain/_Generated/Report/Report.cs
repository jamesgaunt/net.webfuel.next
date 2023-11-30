using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
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
                    case nameof(Report.FileName):
                        FileName = (string)value!;
                        break;
                    case nameof(Report.WorksheetName):
                        WorksheetName = (string)value!;
                        break;
                    case nameof(Report.Design):
                        DesignJson = (string)value!;
                        break;
                    case nameof(Report.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(Report.OwnerUserId):
                        OwnerUserId = (Guid)value!;
                        break;
                    case nameof(Report.ReportGroupId):
                        ReportGroupId = (Guid)value!;
                        break;
                    case nameof(Report.ReportProviderId):
                        ReportProviderId = (Guid)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public string FileName  { get; set; } = String.Empty;
        public string WorksheetName  { get; set; } = String.Empty;
        public ReportDesign Design
        {
            get { return _Design ?? (_Design = SafeJsonSerializer.Deserialize<ReportDesign>(_DesignJson)); }
            set { _Design = value; }
        }
        ReportDesign? _Design = null;
        internal string DesignJson
        {
            get { var result = _Design == null ? _DesignJson : (_DesignJson = SafeJsonSerializer.Serialize(_Design)); _Design = null; return result; }
            set { _DesignJson = value; _Design = null; }
        }
        string _DesignJson = String.Empty;
        public int SortOrder  { get; set; } = 0;
        public Guid OwnerUserId { get; set; }
        public Guid ReportGroupId { get; set; }
        public Guid ReportProviderId { get; set; }
        public Report Copy()
        {
            var entity = new Report();
            entity.Id = Id;
            entity.Name = Name;
            entity.FileName = FileName;
            entity.WorksheetName = WorksheetName;
            entity.DesignJson = DesignJson;
            entity.SortOrder = SortOrder;
            entity.OwnerUserId = OwnerUserId;
            entity.ReportGroupId = ReportGroupId;
            entity.ReportProviderId = ReportProviderId;
            return entity;
        }
    }
}

