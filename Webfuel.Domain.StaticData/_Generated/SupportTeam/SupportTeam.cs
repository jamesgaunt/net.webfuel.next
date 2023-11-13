using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class SupportTeam: IStaticData
    {
        public SupportTeam() { }
        
        public SupportTeam(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(SupportTeam.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(SupportTeam.Name):
                        Name = (string)value!;
                        break;
                    case nameof(SupportTeam.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(SupportTeam.Default):
                        Default = (bool)value!;
                        break;
                    case nameof(SupportTeam.Hidden):
                        Hidden = (bool)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; internal set; } = Guid.Empty;
        public string Name  { get; internal set; } = String.Empty;
        public int SortOrder  { get; internal set; } = 0;
        public bool Default  { get; internal set; } = false;
        public bool Hidden  { get; internal set; } = false;
        public SupportTeam Copy()
        {
            var entity = new SupportTeam();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            entity.Hidden = Hidden;
            return entity;
        }
    }
}

