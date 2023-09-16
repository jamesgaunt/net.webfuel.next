using FluentValidation;

namespace Webfuel
{
    public partial class Widget
    {
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public int Age  { get; set; } = 0;
        public DateOnly ShippingDate  { get; set; } = new DateOnly(1900, 1, 1);
        public int? NullableInt  { get; set; } = null;
        public string? NullableString  { get; set; } = null;
        public DayOfWeek DayOfWeek  { get; set; } = 0;
        public DateTimeUtc CreatedAt  { get; set; } = new DateTimeUtc(599266080000000000L);
        public DateTimeUtc UpdatedAt  { get; set; } = new DateTimeUtc(599266080000000000L);
        public Widget Copy()
        {
            var entity = new Widget();
            entity.Id = Id;
            entity.Name = Name;
            entity.Age = Age;
            entity.ShippingDate = ShippingDate;
            entity.NullableInt = NullableInt;
            entity.NullableString = NullableString;
            entity.DayOfWeek = DayOfWeek;
            entity.CreatedAt = CreatedAt;
            entity.UpdatedAt = UpdatedAt;
            return entity;
        }
    }
}

