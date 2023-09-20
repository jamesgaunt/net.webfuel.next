using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.Common
{
    public partial class UserListView
    {
        public Guid Id  { get; set; } = Guid.Empty;
        public string Email  { get; set; } = String.Empty;
        public UserListView Copy()
        {
            var entity = new UserListView();
            entity.Id = Id;
            entity.Email = Email;
            return entity;
        }
    }
}

