using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class Job
    {
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public Job Copy()
        {
            var entity = new Job();
            entity.Id = Id;
            entity.Name = Name;
            return entity;
        }
    }
}

