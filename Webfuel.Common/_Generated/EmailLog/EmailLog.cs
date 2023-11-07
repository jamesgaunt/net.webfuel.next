using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class EmailLog
    {
        public EmailLog() { }
        
        public EmailLog(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(EmailLog.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(EmailLog.EntityId):
                        EntityId = (Guid)value!;
                        break;
                    case nameof(EmailLog.SendTo):
                        SendTo = (string)value!;
                        break;
                    case nameof(EmailLog.SendCc):
                        SendCc = (string)value!;
                        break;
                    case nameof(EmailLog.SendBcc):
                        SendBcc = (string)value!;
                        break;
                    case nameof(EmailLog.SentBy):
                        SentBy = (string)value!;
                        break;
                    case nameof(EmailLog.ReplyTo):
                        ReplyTo = (string)value!;
                        break;
                    case nameof(EmailLog.Subject):
                        Subject = (string)value!;
                        break;
                    case nameof(EmailLog.HtmlBody):
                        HtmlBody = (string)value!;
                        break;
                    case nameof(EmailLog.SentAt):
                        SentAt = (DateTimeOffset)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public Guid EntityId  { get; set; } = Guid.Empty;
        public string SendTo  { get; set; } = String.Empty;
        public string SendCc  { get; set; } = String.Empty;
        public string SendBcc  { get; set; } = String.Empty;
        public string SentBy  { get; set; } = String.Empty;
        public string ReplyTo  { get; set; } = String.Empty;
        public string Subject  { get; set; } = String.Empty;
        public string HtmlBody  { get; set; } = String.Empty;
        public DateTimeOffset SentAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public EmailLog Copy()
        {
            var entity = new EmailLog();
            entity.Id = Id;
            entity.EntityId = EntityId;
            entity.SendTo = SendTo;
            entity.SendCc = SendCc;
            entity.SendBcc = SendBcc;
            entity.SentBy = SentBy;
            entity.ReplyTo = ReplyTo;
            entity.Subject = Subject;
            entity.HtmlBody = HtmlBody;
            entity.SentAt = SentAt;
            return entity;
        }
    }
}

