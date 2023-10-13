using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class Job
    {
        public Job() { }
        
        public Job(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(Job.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(Job.Name):
                        Name = (string)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public Job Copy()
        {
            var entity = new Job();
            entity.Id = Id;
            entity.Name = Name;
            return entity;
        }
    }
}

