using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.Common
{
    public partial class Tenant
    {
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public bool Live  { get; set; } = false;
        public Tenant Copy()
        {
            var entity = new Tenant();
            entity.Id = Id;
            entity.Name = Name;
            entity.Live = Live;
            return entity;
        }
    }
}

