using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class RSSHub: IStaticData
    {
        public RSSHub() { }
        
        public RSSHub(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(RSSHub.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(RSSHub.Name):
                        Name = (string)value!;
                        break;
                    case nameof(RSSHub.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(RSSHub.Default):
                        Default = (bool)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; internal set; } = Guid.Empty;
        public string Name  { get; internal set; } = String.Empty;
        public int SortOrder  { get; internal set; } = 0;
        public bool Default  { get; internal set; } = false;
        public RSSHub Copy()
        {
            var entity = new RSSHub();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            return entity;
        }
    }
}

