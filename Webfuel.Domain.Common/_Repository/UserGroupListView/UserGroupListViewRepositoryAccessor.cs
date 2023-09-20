using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.Common
{
    internal class UserGroupListViewRepositoryAccessor: IRepositoryAccessor<UserGroupListView>
    {
        private readonly UserGroupListViewRepositoryValidator _validator = new UserGroupListViewRepositoryValidator();
        public string DatabaseTable => "UserGroupListView";
        public string DefaultOrderBy => "ORDER BY Id ASC";
        public object? GetValue(UserGroupListView entity, string property)
        {
            switch(property)
            {
                case nameof(UserGroupListView.Id):
                    return entity.Id;
                case nameof(UserGroupListView.Name):
                    return entity.Name;
                    default: throw new InvalidOperationException($"Unrecognised entity property {property}");
            }
        }
        public void SetValue(UserGroupListView entity, string property, object? value)
        {
            switch(property)
            {
                case nameof(UserGroupListView.Id):
                    entity.Id = (Guid)value!;
                    break;
                case nameof(UserGroupListView.Name):
                    entity.Name = (string)value!;
                    break;
            }
        }
        public UserGroupListView CreateInstance()
        {
            return new UserGroupListView();
        }
        public void Validate(UserGroupListView entity)
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

