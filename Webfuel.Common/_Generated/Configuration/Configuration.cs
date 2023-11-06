using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Common
{
    public partial class Configuration
    {
        public Configuration() { }
        
        public Configuration(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(Configuration.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(Configuration.Prefix):
                        Prefix = (string)value!;
                        break;
                    case nameof(Configuration.NextProjectNumber):
                        NextProjectNumber = (int)value!;
                        break;
                    case nameof(Configuration.NextSupportRequestNumber):
                        NextSupportRequestNumber = (int)value!;
                        break;
                    case nameof(Configuration.DomainName):
                        DomainName = (string)value!;
                        break;
                    case nameof(Configuration.ReplyTo):
                        ReplyTo = (string)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Prefix  { get; set; } = String.Empty;
        public int NextProjectNumber  { get; set; } = 0;
        public int NextSupportRequestNumber  { get; set; } = 0;
        public string DomainName  { get; set; } = String.Empty;
        public string ReplyTo  { get; set; } = String.Empty;
        public Configuration Copy()
        {
            var entity = new Configuration();
            entity.Id = Id;
            entity.Prefix = Prefix;
            entity.NextProjectNumber = NextProjectNumber;
            entity.NextSupportRequestNumber = NextSupportRequestNumber;
            entity.DomainName = DomainName;
            entity.ReplyTo = ReplyTo;
            return entity;
        }
    }
}

