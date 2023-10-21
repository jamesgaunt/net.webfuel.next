using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
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
                    case nameof(Configuration.NextProjectNumber):
                        NextProjectNumber = (int)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public int NextProjectNumber  { get; set; } = 1;
        public Configuration Copy()
        {
            var entity = new Configuration();
            entity.Id = Id;
            entity.NextProjectNumber = NextProjectNumber;
            return entity;
        }
    }
}

