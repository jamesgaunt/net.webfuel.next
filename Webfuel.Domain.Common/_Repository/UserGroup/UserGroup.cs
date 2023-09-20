using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.Common
{
    public partial class UserGroup
    {
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public UserGroup Copy()
        {
            var entity = new UserGroup();
            entity.Id = Id;
            entity.Name = Name;
            return entity;
        }
    }
}

