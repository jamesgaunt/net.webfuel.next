using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.Common
{
    internal class UserListViewRepositoryAccessor: IRepositoryAccessor<UserListView>
    {
        private readonly UserListViewRepositoryValidator _validator = new UserListViewRepositoryValidator();
        public string DatabaseTable => "UserListView";
        public string DefaultOrderBy => "ORDER BY Id ASC";
        public object? GetValue(UserListView entity, string property)
        {
            switch(property)
            {
                case nameof(UserListView.Id):
                    return entity.Id;
                case nameof(UserListView.Email):
                    return entity.Email;
                    default: throw new InvalidOperationException($"Unrecognised entity property {property}");
            }
        }
        public void SetValue(UserListView entity, string property, object? value)
        {
            switch(property)
            {
                case nameof(UserListView.Id):
                    entity.Id = (Guid)value!;
                    break;
                case nameof(UserListView.Email):
                    entity.Email = (string)value!;
                    break;
            }
        }
        public UserListView CreateInstance()
        {
            return new UserListView();
        }
        public void Validate(UserListView entity)
        {
            entity.Email = entity.Email ?? String.Empty;
            entity.Email = entity.Email.Trim();
            _validator.ValidateAndThrow(entity);
        }
        public IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Email";
            }
        }
        public IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Email";
            }
        }
    }
}

