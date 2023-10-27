using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class Researcher
    {
        public Researcher() { }
        
        public Researcher(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(Researcher.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(Researcher.Email):
                        Email = (string)value!;
                        break;
                    case nameof(Researcher.CreatedAt):
                        CreatedAt = (DateTimeOffset)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Email  { get; set; } = String.Empty;
        public DateTimeOffset CreatedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public Researcher Copy()
        {
            var entity = new Researcher();
            entity.Id = Id;
            entity.Email = Email;
            entity.CreatedAt = CreatedAt;
            return entity;
        }
    }
}

