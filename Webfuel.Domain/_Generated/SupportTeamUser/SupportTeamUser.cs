using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class SupportTeamUser
    {
        public SupportTeamUser() { }
        
        public SupportTeamUser(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(SupportTeamUser.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(SupportTeamUser.IsTeamLead):
                        IsTeamLead = (bool)value!;
                        break;
                    case nameof(SupportTeamUser.UserId):
                        UserId = (Guid)value!;
                        break;
                    case nameof(SupportTeamUser.SupportTeamId):
                        SupportTeamId = (Guid)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public bool IsTeamLead  { get; set; } = false;
        public Guid UserId { get; set; }
        public Guid SupportTeamId { get; set; }
        public SupportTeamUser Copy()
        {
            var entity = new SupportTeamUser();
            entity.Id = Id;
            entity.IsTeamLead = IsTeamLead;
            entity.UserId = UserId;
            entity.SupportTeamId = SupportTeamId;
            return entity;
        }
    }
}

