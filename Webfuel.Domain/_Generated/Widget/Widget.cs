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
                    case nameof(Widget.ConfigData):
                        ConfigData = (string)value!;
                        break;
                    case nameof(Widget.CachedData):
                        CachedData = (string)value!;
                        break;
                    case nameof(Widget.CachedDataVersion):
                        CachedDataVersion = (int)value!;
                        break;
                    case nameof(Widget.CachedDataTimestamp):
                        CachedDataTimestamp = (DateTimeOffset)value!;
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
        public string ConfigData  { get; set; } = String.Empty;
        public string CachedData  { get; set; } = String.Empty;
        public int CachedDataVersion  { get; set; } = 0;
        public DateTimeOffset CachedDataTimestamp  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public Guid UserId { get; set; }
        public Guid WidgetTypeId { get; set; }
        public Widget Copy()
        {
            var entity = new Widget();
            entity.Id = Id;
            entity.SortOrder = SortOrder;
            entity.ConfigData = ConfigData;
            entity.CachedData = CachedData;
            entity.CachedDataVersion = CachedDataVersion;
            entity.CachedDataTimestamp = CachedDataTimestamp;
            entity.UserId = UserId;
            entity.WidgetTypeId = WidgetTypeId;
            return entity;
        }
    }
}

