using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.Common
{
    public partial class UserGroupListView
    {
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public UserGroupListView Copy()
        {
            var entity = new UserGroupListView();
            entity.Id = Id;
            entity.Name = Name;
            return entity;
        }
    }
}

