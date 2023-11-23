using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class ErrorLog
    {
        public ErrorLog() { }
        
        public ErrorLog(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(ErrorLog.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(ErrorLog.EntityId):
                        EntityId = (Guid)value!;
                        break;
                    case nameof(ErrorLog.Summary):
                        Summary = (string)value!;
                        break;
                    case nameof(ErrorLog.Message):
                        Message = (string)value!;
                        break;
                    case nameof(ErrorLog.CreatedAt):
                        CreatedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(ErrorLog.CreatedBy):
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
        public ErrorLog Copy()
        {
            var entity = new ErrorLog();
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

