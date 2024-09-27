using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class StaffRole: IStaticData
    {
        public StaffRole() { }
        
        public StaffRole(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(StaffRole.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(StaffRole.Name):
                        Name = (string)value!;
                        break;
                    case nameof(StaffRole.Alias):
                        Alias = (string)value!;
                        break;
                    case nameof(StaffRole.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(StaffRole.Default):
                        Default = (bool)value!;
                        break;
                    case nameof(StaffRole.Hidden):
                        Hidden = (bool)value!;
                        break;
                    case nameof(StaffRole.FreeText):
                        FreeText = (bool)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; internal set; } = Guid.Empty;
        public string Name  { get; internal set; } = String.Empty;
        public string Alias  { get; internal set; } = String.Empty;
        public int SortOrder  { get; internal set; } = 0;
        public bool Default  { get; internal set; } = false;
        public bool Hidden  { get; internal set; } = false;
        public bool FreeText  { get; internal set; } = false;
        public StaffRole Copy()
        {
            var entity = new StaffRole();
            entity.Id = Id;
            entity.Name = Name;
            entity.Alias = Alias;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            entity.Hidden = Hidden;
            entity.FreeText = FreeText;
            return entity;
        }
    }
}

