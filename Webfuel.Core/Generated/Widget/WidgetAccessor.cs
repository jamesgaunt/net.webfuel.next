using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webfuel
{
    internal class WidgetRepositoryAccessor: IRepositoryAccessor<Widget>
    {
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
                    return entity.ShippingDate.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
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
            if(entity.Name.Length > 64) throw new InvalidOperationException("Name: Cannot be longer than 64 characters.");
        }
        public IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "Age";
                yield return "ShippingDate";
            }
        }
        public IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
                yield return "Age";
                yield return "ShippingDate";
            }
        }
    }
}

