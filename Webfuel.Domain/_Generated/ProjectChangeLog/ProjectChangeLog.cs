using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class ProjectChangeLog
    {
        public ProjectChangeLog() { }
        
        public ProjectChangeLog(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(ProjectChangeLog.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(ProjectChangeLog.Message):
                        Message = (string)value!;
                        break;
                    case nameof(ProjectChangeLog.CreatedAt):
                        CreatedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(ProjectChangeLog.ProjectId):
                        ProjectId = (Guid)value!;
                        break;
                    case nameof(ProjectChangeLog.CreatedByUserId):
                        CreatedByUserId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Message  { get; set; } = String.Empty;
        public DateTimeOffset CreatedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public Guid ProjectId { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public ProjectChangeLog Copy()
        {
            var entity = new ProjectChangeLog();
            entity.Id = Id;
            entity.Message = Message;
            entity.CreatedAt = CreatedAt;
            entity.ProjectId = ProjectId;
            entity.CreatedByUserId = CreatedByUserId;
            return entity;
        }
    }
}

