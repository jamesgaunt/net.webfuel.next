using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class UserLogin
    {
        public UserLogin() { }
        
        public UserLogin(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(UserLogin.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(UserLogin.UserId):
                        UserId = (Guid)value!;
                        break;
                    case nameof(UserLogin.Email):
                        Email = (string)value!;
                        break;
                    case nameof(UserLogin.IPAddress):
                        IPAddress = (string)value!;
                        break;
                    case nameof(UserLogin.Successful):
                        Successful = (bool)value!;
                        break;
                    case nameof(UserLogin.Reason):
                        Reason = (string)value!;
                        break;
                    case nameof(UserLogin.CreatedAt):
                        CreatedAt = (DateTimeOffset)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public Guid UserId  { get; set; } = Guid.Empty;
        public string Email  { get; set; } = String.Empty;
        public string IPAddress  { get; set; } = String.Empty;
        public bool Successful  { get; set; } = false;
        public string Reason  { get; set; } = String.Empty;
        public DateTimeOffset CreatedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public UserLogin Copy()
        {
            var entity = new UserLogin();
            entity.Id = Id;
            entity.UserId = UserId;
            entity.Email = Email;
            entity.IPAddress = IPAddress;
            entity.Successful = Successful;
            entity.Reason = Reason;
            entity.CreatedAt = CreatedAt;
            return entity;
        }
    }
}

