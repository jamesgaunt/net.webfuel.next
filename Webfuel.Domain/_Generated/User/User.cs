using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class User
    {
        public User() { }
        
        public User(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(User.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(User.Email):
                        Email = (string)value!;
                        break;
                    case nameof(User.FirstName):
                        FirstName = (string)value!;
                        break;
                    case nameof(User.LastName):
                        LastName = (string)value!;
                        break;
                    case nameof(User.PasswordHash):
                        PasswordHash = (string)value!;
                        break;
                    case nameof(User.PasswordSalt):
                        PasswordSalt = (string)value!;
                        break;
                    case nameof(User.PasswordResetAt):
                        PasswordResetAt = (DateTimeOffset)value!;
                        break;
                    case nameof(User.PasswordResetToken):
                        PasswordResetToken = (Guid)value!;
                        break;
                    case nameof(User.PasswordResetValidUntil):
                        PasswordResetValidUntil = (DateTimeOffset)value!;
                        break;
                    case nameof(User.Developer):
                        Developer = (bool)value!;
                        break;
                    case nameof(User.Birthday):
                        Birthday = DateOnly.FromDateTime((DateTime)value!);
                        break;
                    case nameof(User.CreatedAt):
                        CreatedAt = (DateTimeOffset)value!;
                        break;
                    case nameof(User.UserGroupId):
                        UserGroupId = (Guid)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Email  { get; set; } = String.Empty;
        public string FirstName  { get; set; } = String.Empty;
        public string LastName  { get; set; } = String.Empty;
        [JsonIgnore]
        public string PasswordHash  { get; set; } = String.Empty;
        [JsonIgnore]
        public string PasswordSalt  { get; set; } = String.Empty;
        [JsonIgnore]
        public DateTimeOffset PasswordResetAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        [JsonIgnore]
        public Guid PasswordResetToken  { get; set; } = Guid.Empty;
        [JsonIgnore]
        public DateTimeOffset PasswordResetValidUntil  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public bool Developer  { get; set; } = false;
        public DateOnly Birthday  { get; set; } = new DateOnly(1900, 1, 1);
        public DateTimeOffset CreatedAt  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public Guid UserGroupId { get; set; }
        public User Copy()
        {
            var entity = new User();
            entity.Id = Id;
            entity.Email = Email;
            entity.FirstName = FirstName;
            entity.LastName = LastName;
            entity.PasswordHash = PasswordHash;
            entity.PasswordSalt = PasswordSalt;
            entity.PasswordResetAt = PasswordResetAt;
            entity.PasswordResetToken = PasswordResetToken;
            entity.PasswordResetValidUntil = PasswordResetValidUntil;
            entity.Developer = Developer;
            entity.Birthday = Birthday;
            entity.CreatedAt = CreatedAt;
            entity.UserGroupId = UserGroupId;
            return entity;
        }
    }
}
