using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class WidgetType
    {
        public WidgetType() { }
        
        public WidgetType(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(WidgetType.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(WidgetType.Name):
                        Name = (string)value!;
                        break;
                    case nameof(WidgetType.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(WidgetType.Description):
                        Description = (string)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public int SortOrder  { get; set; } = 0;
        public string Description  { get; set; } = String.Empty;
        public WidgetType Copy()
        {
            var entity = new WidgetType();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            entity.Description = Description;
            return entity;
        }
    }
}

