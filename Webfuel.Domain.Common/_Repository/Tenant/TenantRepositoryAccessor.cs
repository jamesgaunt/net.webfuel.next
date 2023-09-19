using FluentValidation;

namespace Webfuel.Domain.Common
{
    internal class TenantRepositoryAccessor: IRepositoryAccessor<Tenant>
    {
        private readonly TenantRepositoryValidator _validator = new TenantRepositoryValidator();
        public string DatabaseSchema => "next";
        public string DatabaseTable => "Tenant";
        public string DefaultOrderBy => "ORDER BY Id ASC";
        public object? GetValue(Tenant entity, string property)
        {
            switch(property)
            {
                case nameof(Tenant.Id):
                    return entity.Id;
                case nameof(Tenant.Name):
                    return entity.Name;
                case nameof(Tenant.Live):
                    return entity.Live;
                    default: throw new InvalidOperationException($"Unrecognised entity property {property}");
            }
        }
        public void SetValue(Tenant entity, string property, object? value)
        {
            switch(property)
            {
                case nameof(Tenant.Id):
                    entity.Id = (Guid)value!;
                    break;
                case nameof(Tenant.Name):
                    entity.Name = (string)value!;
                    break;
                case nameof(Tenant.Live):
                    entity.Live = (bool)value!;
                    break;
            }
        }
        public Tenant CreateInstance()
        {
            return new Tenant();
        }
        public void Validate(Tenant entity)
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
                yield return "Live";
            }
        }
        public IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
                yield return "Live";
            }
        }
    }
}

