using FluentValidation;

namespace Webfuel
{
    public partial class WidgetQueryView
    {
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public int Age  { get; set; } = 0;
        public WidgetQueryView Copy()
        {
            var entity = new WidgetQueryView();
            entity.Id = Id;
            entity.Name = Name;
            entity.Age = Age;
            return entity;
        }
    }
}

