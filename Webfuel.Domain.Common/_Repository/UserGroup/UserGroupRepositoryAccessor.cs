using FluentValidation;

namespace Webfuel.Domain.Common
{
    internal class UserGroupRepositoryAccessor: IRepositoryAccessor<UserGroup>
    {
        private readonly UserGroupRepositoryValidator _validator = new UserGroupRepositoryValidator();
        public string DatabaseSchema => "next";
        public string DatabaseTable => "UserGroup";
        public string DefaultOrderBy => "ORDER BY Id ASC";
        public object? GetValue(UserGroup entity, string property)
        {
            switch(property)
            {
                case nameof(UserGroup.Id):
                    return entity.Id;
                case nameof(UserGroup.Name):
                    return entity.Name;
                    default: throw new InvalidOperationException($"Unrecognised entity property {property}");
            }
        }
        public void SetValue(UserGroup entity, string property, object? value)
        {
            switch(property)
            {
                case nameof(UserGroup.Id):
                    entity.Id = (Guid)value!;
                    break;
                case nameof(UserGroup.Name):
                    entity.Name = (string)value!;
                    break;
            }
        }
        public UserGroup CreateInstance()
        {
            return new UserGroup();
        }
        public void Validate(UserGroup entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            _validator.ValidateAndThrow(entity);
        }
        public IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
            }
        }
        public IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
            }
        }
    }
}

