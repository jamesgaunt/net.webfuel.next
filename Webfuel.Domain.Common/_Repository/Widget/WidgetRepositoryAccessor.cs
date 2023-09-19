using FluentValidation;

namespace Webfuel.Domain.Common
{
    internal class WidgetRepositoryAccessor: IRepositoryAccessor<Widget>
    {
        private readonly WidgetRepositoryValidator _validator = new WidgetRepositoryValidator();
        public string DatabaseSchema => "next";
        public string DatabaseTable => "Widget";
        public string DefaultOrderBy => "ORDER BY Id ASC";
        public object? GetValue(Widget entity, string property)
        {
            switch(property)
            {
                case nameof(Widget.Id):
                    return entity.Id;
                case nameof(Widget.Name):
                    return entity.Name;
                case nameof(Widget.Age):
                    return entity.Age;
                case nameof(Widget.ShippingDate):
                    return entity.ShippingDate;
                case nameof(Widget.NullableInt):
                    return entity.NullableInt;
                case nameof(Widget.NullableString):
                    return entity.NullableString;
                case nameof(Widget.DayOfWeek):
                    return entity.DayOfWeek;
                case nameof(Widget.CreatedAt):
                    return entity.CreatedAt.ToDateTime();
                case nameof(Widget.UpdatedAt):
                    return entity.UpdatedAt.ToDateTime();
                    default: throw new InvalidOperationException($"Unrecognised entity property {property}");
            }
        }
        public void SetValue(Widget entity, string property, object? value)
        {
            switch(property)
            {
                case nameof(Widget.Id):
                    entity.Id = (Guid)value!;
                    break;
                case nameof(Widget.Name):
                    entity.Name = (string)value!;
                    break;
                case nameof(Widget.Age):
                    entity.Age = (int)value!;
                    break;
                case nameof(Widget.ShippingDate):
                    entity.ShippingDate = DateOnly.FromDateTime((DateTime)value!);
                    break;
                case nameof(Widget.NullableInt):
                    entity.NullableInt = value == DBNull.Value ? (int?)null : (int?)value;
                    break;
                case nameof(Widget.NullableString):
                    entity.NullableString = value == DBNull.Value ? (string?)null : (string?)value;
                    break;
                case nameof(Widget.DayOfWeek):
                    entity.DayOfWeek = (DayOfWeek)value!;
                    break;
                case nameof(Widget.CreatedAt):
                    entity.CreatedAt = new DateTimeUtc((DateTime)value!);
                    break;
                case nameof(Widget.UpdatedAt):
                    entity.UpdatedAt = new DateTimeUtc((DateTime)value!);
                    break;
            }
        }
        public Widget CreateInstance()
        {
            return new Widget();
        }
        public void Validate(Widget entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            entity.NullableString = entity.NullableString?.Trim();
            _validator.ValidateAndThrow(entity);
        }
        public IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "Age";
                yield return "ShippingDate";
                yield return "NullableInt";
                yield return "NullableString";
                yield return "DayOfWeek";
                yield return "CreatedAt";
                yield return "UpdatedAt";
            }
        }
        public IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
                yield return "Age";
                yield return "ShippingDate";
                yield return "NullableInt";
                yield return "NullableString";
                yield return "DayOfWeek";
                yield return "CreatedAt";
                yield return "UpdatedAt";
            }
        }
    }
}

