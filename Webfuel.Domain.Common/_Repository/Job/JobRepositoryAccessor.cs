using FluentValidation;

namespace Webfuel.Domain.Common
{
    internal class JobRepositoryAccessor: IRepositoryAccessor<Job>
    {
        private readonly JobRepositoryValidator _validator = new JobRepositoryValidator();
        public string DatabaseSchema => "next";
        public string DatabaseTable => "Job";
        public string DefaultOrderBy => "ORDER BY Id ASC";
        public object? GetValue(Job entity, string property)
        {
            switch(property)
            {
                case nameof(Job.Id):
                    return entity.Id;
                case nameof(Job.Name):
                    return entity.Name;
                    default: throw new InvalidOperationException($"Unrecognised entity property {property}");
            }
        }
        public void SetValue(Job entity, string property, object? value)
        {
            switch(property)
            {
                case nameof(Job.Id):
                    entity.Id = (Guid)value!;
                    break;
                case nameof(Job.Name):
                    entity.Name = (string)value!;
                    break;
            }
        }
        public Job CreateInstance()
        {
            return new Job();
        }
        public void Validate(Job entity)
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

