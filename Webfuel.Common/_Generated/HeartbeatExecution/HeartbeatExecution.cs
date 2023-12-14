using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class HeartbeatExecution
    {
        public HeartbeatExecution() { }
        
        public HeartbeatExecution(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(HeartbeatExecution.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(HeartbeatExecution.ExecutedAt):
                        ExecutedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(HeartbeatExecution.Message):
                        Message = (string)value!;
                        break;
                    case nameof(HeartbeatExecution.Success):
                        Success = (bool)value!;
                        break;
                    case nameof(HeartbeatExecution.Microseconds):
                        Microseconds = (int)value!;
                        break;
                    case nameof(HeartbeatExecution.MetadataJson):
                        MetadataJson = (string)value!;
                        break;
                    case nameof(HeartbeatExecution.HeartbeatId):
                        HeartbeatId = (Guid)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public DateTimeOffset ExecutedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public string Message  { get; set; } = String.Empty;
        public bool Success  { get; set; } = false;
        public int Microseconds  { get; set; } = 0;
        public string MetadataJson  { get; set; } = String.Empty;
        public Guid HeartbeatId { get; set; }
        public HeartbeatExecution Copy()
        {
            var entity = new HeartbeatExecution();
            entity.Id = Id;
            entity.ExecutedAt = ExecutedAt;
            entity.Message = Message;
            entity.Success = Success;
            entity.Microseconds = Microseconds;
            entity.MetadataJson = MetadataJson;
            entity.HeartbeatId = HeartbeatId;
            return entity;
        }
    }
}

