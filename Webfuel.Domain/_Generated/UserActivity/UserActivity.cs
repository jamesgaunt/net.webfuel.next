using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class UserActivity
    {
        public UserActivity() { }
        
        public UserActivity(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(UserActivity.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(UserActivity.UserId):
                        UserId = (Guid)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public Guid UserId { get; set; }
        public UserActivity Copy()
        {
            var entity = new UserActivity();
            entity.Id = Id;
            entity.UserId = UserId;
            return entity;
        }
    }
}

