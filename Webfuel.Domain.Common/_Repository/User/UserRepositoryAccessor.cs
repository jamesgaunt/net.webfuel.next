using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.Common
{
    internal class UserRepositoryAccessor: IRepositoryAccessor<User>
    {
        private readonly UserRepositoryValidator _validator = new UserRepositoryValidator();
        public string DatabaseTable => "User";
        public string DefaultOrderBy => "ORDER BY Id ASC";
        public object? GetValue(User entity, string property)
        {
            switch(property)
            {
                case nameof(User.Id):
                    return entity.Id;
                case nameof(User.Email):
                    return entity.Email;
                case nameof(User.FirstName):
                    return entity.FirstName;
                case nameof(User.LastName):
                    return entity.LastName;
                case nameof(User.PasswordHash):
                    return entity.PasswordHash;
                case nameof(User.PasswordSalt):
                    return entity.PasswordSalt;
                case nameof(User.PasswordResetAt):
                    return entity.PasswordResetAt.ToDateTime();
                case nameof(User.PasswordResetToken):
                    return entity.PasswordResetToken;
                case nameof(User.PasswordResetValidUntil):
                    return entity.PasswordResetValidUntil.ToDateTime();
                case nameof(User.Developer):
                    return entity.Developer;
                case nameof(User.UserGroupId):
                    return entity.UserGroupId;
                    default: throw new InvalidOperationException($"Unrecognised entity property {property}");
            }
        }
        public void SetValue(User entity, string property, object? value)
        {
            switch(property)
            {
                case nameof(User.Id):
                    entity.Id = (Guid)value!;
                    break;
                case nameof(User.Email):
                    entity.Email = (string)value!;
                    break;
                case nameof(User.FirstName):
                    entity.FirstName = (string)value!;
                    break;
                case nameof(User.LastName):
                    entity.LastName = (string)value!;
                    break;
                case nameof(User.PasswordHash):
                    entity.PasswordHash = (string)value!;
                    break;
                case nameof(User.PasswordSalt):
                    entity.PasswordSalt = (string)value!;
                    break;
                case nameof(User.PasswordResetAt):
                    entity.PasswordResetAt = new DateTimeUtc((DateTime)value!);
                    break;
                case nameof(User.PasswordResetToken):
                    entity.PasswordResetToken = (Guid)value!;
                    break;
                case nameof(User.PasswordResetValidUntil):
                    entity.PasswordResetValidUntil = new DateTimeUtc((DateTime)value!);
                    break;
                case nameof(User.Developer):
                    entity.Developer = (bool)value!;
                    break;
                case nameof(User.UserGroupId):
                    entity.UserGroupId = (Guid)value!;
                    break;
            }
        }
        public User CreateInstance()
        {
            return new User();
        }
        public void Validate(User entity)
        {
            entity.Email = entity.Email ?? String.Empty;
            entity.Email = entity.Email.Trim();
            entity.FirstName = entity.FirstName ?? String.Empty;
            entity.FirstName = entity.FirstName.Trim();
            entity.LastName = entity.LastName ?? String.Empty;
            entity.LastName = entity.LastName.Trim();
            entity.PasswordHash = entity.PasswordHash ?? String.Empty;
            entity.PasswordHash = entity.PasswordHash.Trim();
            entity.PasswordSalt = entity.PasswordSalt ?? String.Empty;
            entity.PasswordSalt = entity.PasswordSalt.Trim();
            _validator.ValidateAndThrow(entity);
        }
        public IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Email";
                yield return "FirstName";
                yield return "LastName";
                yield return "PasswordHash";
                yield return "PasswordSalt";
                yield return "PasswordResetAt";
                yield return "PasswordResetToken";
                yield return "PasswordResetValidUntil";
                yield return "Developer";
                yield return "UserGroupId";
            }
        }
        public IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Email";
                yield return "FirstName";
                yield return "LastName";
                yield return "PasswordHash";
                yield return "PasswordSalt";
                yield return "PasswordResetAt";
                yield return "PasswordResetToken";
                yield return "PasswordResetValidUntil";
                yield return "Developer";
                yield return "UserGroupId";
            }
        }
    }
}

