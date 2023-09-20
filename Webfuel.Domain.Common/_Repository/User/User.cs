using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.Common
{
    public partial class User
    {
        public Guid Id  { get; set; } = Guid.Empty;
        public string Email  { get; set; } = String.Empty;
        public string FirstName  { get; set; } = String.Empty;
        public string LastName  { get; set; } = String.Empty;
        [JsonIgnore]
        public string PasswordHash  { get; set; } = String.Empty;
        [JsonIgnore]
        public string PasswordSalt  { get; set; } = String.Empty;
        [JsonIgnore]
        public DateTimeUtc PasswordResetAt  { get; set; } = new DateTimeUtc(599266080000000000L);
        [JsonIgnore]
        public Guid PasswordResetToken  { get; set; } = Guid.Empty;
        [JsonIgnore]
        public DateTimeUtc PasswordResetValidUntil  { get; set; } = new DateTimeUtc(599266080000000000L);
        public bool Developer  { get; set; } = false;
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
            return entity;
        }
    }
}

