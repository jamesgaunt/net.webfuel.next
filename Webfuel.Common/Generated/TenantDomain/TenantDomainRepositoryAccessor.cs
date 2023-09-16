using FluentValidation;

namespace Webfuel.Common
{
    internal class TenantDomainRepositoryAccessor: IRepositoryAccessor<TenantDomain>
    {
        private readonly TenantDomainRepositoryValidator _validator = new TenantDomainRepositoryValidator();
        public string DatabaseSchema => "next";
        public string DatabaseTable => "TenantDomain";
        public string DefaultOrderBy => "ORDER BY Id ASC";
        public object? GetValue(TenantDomain entity, string property)
        {
            switch(property)
            {
                case nameof(TenantDomain.Id):
                    return entity.Id;
                case nameof(TenantDomain.Domain):
                    return entity.Domain;
                case nameof(TenantDomain.RedirectTo):
                    return entity.RedirectTo;
                case nameof(TenantDomain.TenantId):
                    return entity.TenantId;
                    default: throw new InvalidOperationException($"Unrecognised entity property {property}");
            }
        }
        public void SetValue(TenantDomain entity, string property, object? value)
        {
            switch(property)
            {
                case nameof(TenantDomain.Id):
                    entity.Id = (Guid)value!;
                    break;
                case nameof(TenantDomain.Domain):
                    entity.Domain = (string)value!;
                    break;
                case nameof(TenantDomain.RedirectTo):
                    entity.RedirectTo = (string)value!;
                    break;
                case nameof(TenantDomain.TenantId):
                    entity.TenantId = (Guid)value!;
                    break;
            }
        }
        public TenantDomain CreateInstance()
        {
            return new TenantDomain();
        }
        public void Validate(TenantDomain entity)
        {
            entity.Domain = entity.Domain ?? String.Empty;
            entity.Domain = entity.Domain.Trim();
            entity.RedirectTo = entity.RedirectTo ?? String.Empty;
            entity.RedirectTo = entity.RedirectTo.Trim();
            _validator.ValidateAndThrow(entity);
        }
        public IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Domain";
                yield return "RedirectTo";
                yield return "TenantId";
            }
        }
        public IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Domain";
                yield return "RedirectTo";
                yield return "TenantId";
            }
        }
    }
}

