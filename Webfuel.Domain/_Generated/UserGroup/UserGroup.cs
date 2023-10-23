using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class UserGroup
    {
        public UserGroup() { }
        
        public UserGroup(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(UserGroup.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(UserGroup.Name):
                        Name = (string)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public UserGroup Copy()
        {
            var entity = new UserGroup();
            entity.Id = Id;
            entity.Name = Name;
            return entity;
        }
    }
}

