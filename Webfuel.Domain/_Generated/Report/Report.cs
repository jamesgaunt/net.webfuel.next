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
                    case nameof(Report.Description):
                        Description = (string)value!;
                        break;
                    case nameof(Report.Design):
                        DesignJson = (string)value!;
                        break;
                    case nameof(Report.IsPublic):
                        IsPublic = (bool)value!;
                        break;
                    case nameof(Report.CreatedAt):
                        CreatedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(Report.ReportProviderId):
                        ReportProviderId = (Guid)value!;
                        break;
                    case nameof(Report.OwnerUserId):
                        OwnerUserId = (Guid)value!;
                        break;
                    case nameof(Report.ReportGroupId):
                        ReportGroupId = (Guid)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public string Description  { get; set; } = String.Empty;
        public Webfuel.Reporting.ReportDesign Design
        {
            get { return _Design ?? (_Design = SafeJsonSerializer.Deserialize<Webfuel.Reporting.ReportDesign>(_DesignJson)); }
            set { _Design = value; }
        }
        Webfuel.Reporting.ReportDesign? _Design = null;
        internal string DesignJson
        {
            get { var result = _Design == null ? _DesignJson : (_DesignJson = SafeJsonSerializer.Serialize(_Design)); _Design = null; return result; }
            set { _DesignJson = value; _Design = null; }
        }
        string _DesignJson = String.Empty;
        public bool IsPublic  { get; set; } = false;
        public DateTimeOffset CreatedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public Guid ReportProviderId { get; set; }
        public Guid OwnerUserId { get; set; }
        public Guid ReportGroupId { get; set; }
        public Report Copy()
        {
            var entity = new Report();
            entity.Id = Id;
            entity.Name = Name;
            entity.Description = Description;
            entity.DesignJson = DesignJson;
            entity.IsPublic = IsPublic;
            entity.CreatedAt = CreatedAt;
            entity.ReportProviderId = ReportProviderId;
            entity.OwnerUserId = OwnerUserId;
            entity.ReportGroupId = ReportGroupId;
            return entity;
        }
    }
}

