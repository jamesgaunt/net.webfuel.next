using FluentValidation;

namespace Webfuel.Common
{
    public partial class TenantDomain
    {
        public Guid Id  { get; set; } = Guid.Empty;
        public string Domain  { get; set; } = String.Empty;
        public string RedirectTo  { get; set; } = String.Empty;
        public Guid TenantId { get; set; }
        public TenantDomain Copy()
        {
            var entity = new TenantDomain();
            entity.Id = Id;
            entity.Domain = Domain;
            entity.RedirectTo = RedirectTo;
            entity.TenantId = TenantId;
            return entity;
        }
    }
}

