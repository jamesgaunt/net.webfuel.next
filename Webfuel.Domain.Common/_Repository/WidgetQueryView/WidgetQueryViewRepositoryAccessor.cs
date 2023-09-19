using FluentValidation;

namespace Webfuel.Domain.Common
{
    internal class WidgetQueryViewRepositoryAccessor: IRepositoryAccessor<WidgetQueryView>
    {
        private readonly WidgetQueryViewRepositoryValidator _validator = new WidgetQueryViewRepositoryValidator();
        public string DatabaseSchema => "next";
        public string DatabaseTable => "WidgetQueryView";
        public string DefaultOrderBy => "ORDER BY Id ASC";
        public object? GetValue(WidgetQueryView entity, string property)
        {
            switch(property)
            {
                case nameof(WidgetQueryView.Id):
                    return entity.Id;
                case nameof(WidgetQueryView.Name):
                    return entity.Name;
                case nameof(WidgetQueryView.Age):
                    return entity.Age;
                    default: throw new InvalidOperationException($"Unrecognised entity property {property}");
            }
        }
        public void SetValue(WidgetQueryView entity, string property, object? value)
        {
            switch(property)
            {
                case nameof(WidgetQueryView.Id):
                    entity.Id = (Guid)value!;
                    break;
                case nameof(WidgetQueryView.Name):
                    entity.Name = (string)value!;
                    break;
                case nameof(WidgetQueryView.Age):
                    entity.Age = (int)value!;
                    break;
            }
        }
        public WidgetQueryView CreateInstance()
        {
            return new WidgetQueryView();
        }
        public void Validate(WidgetQueryView entity)
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
                yield return "Age";
            }
        }
        public IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
                yield return "Age";
            }
        }
    }
}

