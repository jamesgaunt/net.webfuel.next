using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class ProjectStatus: IStaticData
    {
        public ProjectStatus() { }
        
        public ProjectStatus(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(ProjectStatus.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(ProjectStatus.Name):
                        Name = (string)value!;
                        break;
                    case nameof(ProjectStatus.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(ProjectStatus.Default):
                        Default = (bool)value!;
                        break;
                    case nameof(ProjectStatus.Hidden):
                        Hidden = (bool)value!;
                        break;
                    case nameof(ProjectStatus.Active):
                        Active = (bool)value!;
                        break;
                    case nameof(ProjectStatus.Locked):
                        Locked = (bool)value!;
                        break;
                    case nameof(ProjectStatus.Discarded):
                        Discarded = (bool)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; internal set; } = Guid.Empty;
        public string Name  { get; internal set; } = String.Empty;
        public int SortOrder  { get; internal set; } = 0;
        public bool Default  { get; internal set; } = false;
        public bool Hidden  { get; internal set; } = false;
        public bool Active  { get; internal set; } = false;
        public bool Locked  { get; internal set; } = false;
        public bool Discarded  { get; internal set; } = false;
        public ProjectStatus Copy()
        {
            var entity = new ProjectStatus();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            entity.Hidden = Hidden;
            entity.Active = Active;
            entity.Locked = Locked;
            entity.Discarded = Discarded;
            return entity;
        }
    }
}

