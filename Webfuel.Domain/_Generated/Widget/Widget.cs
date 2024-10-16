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
                    case nameof(Widget.ConfigJson):
                        ConfigJson = (string)value!;
                        break;
                    case nameof(Widget.HeaderText):
                        HeaderText = (string)value!;
                        break;
                    case nameof(Widget.DataJson):
                        DataJson = (string)value!;
                        break;
                    case nameof(Widget.DataVersion):
                        DataVersion = (int)value!;
                        break;
                    case nameof(Widget.DataTimestamp):
                        DataTimestamp = (DateTimeOffset)value!;
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
        public string ConfigJson  { get; set; } = String.Empty;
        public string HeaderText  { get; set; } = String.Empty;
        public string DataJson  { get; set; } = String.Empty;
        public int DataVersion  { get; set; } = 0;
        public DateTimeOffset DataTimestamp  { get; set; } = new DateTimeOffset(599266080000000000L, TimeSpan.Zero);
        public Guid UserId { get; set; }
        public Guid WidgetTypeId { get; set; }
        public Widget Copy()
        {
            var entity = new Widget();
            entity.Id = Id;
            entity.SortOrder = SortOrder;
            entity.ConfigJson = ConfigJson;
            entity.HeaderText = HeaderText;
            entity.DataJson = DataJson;
            entity.DataVersion = DataVersion;
            entity.DataTimestamp = DataTimestamp;
            entity.UserId = UserId;
            entity.WidgetTypeId = WidgetTypeId;
            return entity;
        }
    }
}

