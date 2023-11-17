using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class SupportRequestChangeLog
    {
        public SupportRequestChangeLog() { }
        
        public SupportRequestChangeLog(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(SupportRequestChangeLog.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(SupportRequestChangeLog.Message):
                        Message = (string)value!;
                        break;
                    case nameof(SupportRequestChangeLog.CreatedAt):
                        CreatedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(SupportRequestChangeLog.SupportRequestId):
                        SupportRequestId = (Guid)value!;
                        break;
                    case nameof(SupportRequestChangeLog.CreatedByUserId):
                        CreatedByUserId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Message  { get; set; } = String.Empty;
        public DateTimeOffset CreatedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public Guid SupportRequestId { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public SupportRequestChangeLog Copy()
        {
            var entity = new SupportRequestChangeLog();
            entity.Id = Id;
            entity.Message = Message;
            entity.CreatedAt = CreatedAt;
            entity.SupportRequestId = SupportRequestId;
            entity.CreatedByUserId = CreatedByUserId;
            return entity;
        }
    }
}

