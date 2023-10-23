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
                    case nameof(UserActivity.Date):
                        Date = DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(UserActivity.Description):
                        Description = (string)value!;
                        break;
                    case nameof(UserActivity.UserId):
                        UserId = (Guid)value!;
                        break;
                    case nameof(UserActivity.WorkActivityId):
                        WorkActivityId = (Guid)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public DateOnly Date  { get; set; } = new DateOnly(1900, 1, 1);
        public string Description  { get; set; } = String.Empty;
        public Guid UserId { get; set; }
        public Guid WorkActivityId { get; set; }
        public UserActivity Copy()
        {
            var entity = new UserActivity();
            entity.Id = Id;
            entity.Date = Date;
            entity.Description = Description;
            entity.UserId = UserId;
            entity.WorkActivityId = WorkActivityId;
            return entity;
        }
    }
}

