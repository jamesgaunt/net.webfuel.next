using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class Widget
    {
        public Widget() { }
        
        public Widget(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(Widget.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(Widget.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(Widget.UserId):
                        UserId = (Guid)value!;
                        break;
                    case nameof(Widget.WidgetTypeId):
                        WidgetTypeId = (Guid)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public int SortOrder  { get; set; } = 0;
        public Guid UserId { get; set; }
        public Guid WidgetTypeId { get; set; }
        public Widget Copy()
        {
            var entity = new Widget();
            entity.Id = Id;
            entity.SortOrder = SortOrder;
            entity.UserId = UserId;
            entity.WidgetTypeId = WidgetTypeId;
            return entity;
        }
    }
}

