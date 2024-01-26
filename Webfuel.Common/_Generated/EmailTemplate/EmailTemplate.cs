using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class EmailTemplate
    {
        public EmailTemplate() { }
        
        public EmailTemplate(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(EmailTemplate.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(EmailTemplate.Name):
                        Name = (string)value!;
                        break;
                    case nameof(EmailTemplate.SendTo):
                        SendTo = (string)value!;
                        break;
                    case nameof(EmailTemplate.SendCc):
                        SendCc = (string)value!;
                        break;
                    case nameof(EmailTemplate.SendBcc):
                        SendBcc = (string)value!;
                        break;
                    case nameof(EmailTemplate.SentBy):
                        SentBy = (string)value!;
                        break;
                    case nameof(EmailTemplate.ReplyTo):
                        ReplyTo = (string)value!;
                        break;
                    case nameof(EmailTemplate.Subject):
                        Subject = (string)value!;
                        break;
                    case nameof(EmailTemplate.HtmlTemplate):
                        HtmlTemplate = (string)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public string SendTo  { get; set; } = String.Empty;
        public string SendCc  { get; set; } = String.Empty;
        public string SendBcc  { get; set; } = String.Empty;
        public string SentBy  { get; set; } = String.Empty;
        public string ReplyTo  { get; set; } = String.Empty;
        public string Subject  { get; set; } = String.Empty;
        public string HtmlTemplate  { get; set; } = String.Empty;
        public EmailTemplate Copy()
        {
            var entity = new EmailTemplate();
            entity.Id = Id;
            entity.Name = Name;
            entity.SendTo = SendTo;
            entity.SendCc = SendCc;
            entity.SendBcc = SendBcc;
            entity.SentBy = SentBy;
            entity.ReplyTo = ReplyTo;
            entity.Subject = Subject;
            entity.HtmlTemplate = HtmlTemplate;
            return entity;
        }
    }
}

