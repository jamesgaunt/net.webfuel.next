using FluentValidation;

namespace Webfuel.Domain.Common
{
    internal class UserRepositoryAccessor: IRepositoryAccessor<User>
    {
        private readonly UserRepositoryValidator _validator = new UserRepositoryValidator();
        public string DatabaseSchema => "next";
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
                case nameof(User.IsDeveloper):
                    return entity.IsDeveloper;
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
                case nameof(User.IsDeveloper):
                    entity.IsDeveloper = (bool)value!;
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
                yield return "IsDeveloper";
            }
        }
        public IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Email";
                yield return "FirstName";
                yield return "LastName";
                yield return "IsDeveloper";
            }
        }
    }
}

