using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class ActivityLog
    {
        public ActivityLog() { }
        
        public ActivityLog(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(ActivityLog.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(ActivityLog.EntityId):
                        EntityId = (Guid)value!;
                        break;
                    case nameof(ActivityLog.Summary):
                        Summary = (string)value!;
                        break;
                    case nameof(ActivityLog.Message):
                        Message = (string)value!;
                        break;
                    case nameof(ActivityLog.CreatedAt):
                        CreatedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(ActivityLog.CreatedBy):
                        CreatedBy = (string)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public Guid EntityId  { get; set; } = Guid.Empty;
        public string Summary  { get; set; } = String.Empty;
        public string Message  { get; set; } = String.Empty;
        public DateTimeOffset CreatedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public string CreatedBy  { get; set; } = String.Empty;
        public ActivityLog Copy()
        {
            var entity = new ActivityLog();
            entity.Id = Id;
            entity.EntityId = EntityId;
            entity.Summary = Summary;
            entity.Message = Message;
            entity.CreatedAt = CreatedAt;
            entity.CreatedBy = CreatedBy;
            return entity;
        }
    }
}

