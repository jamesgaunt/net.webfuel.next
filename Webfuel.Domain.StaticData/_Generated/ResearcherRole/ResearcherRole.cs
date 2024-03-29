using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class ResearcherRole: IStaticData
    {
        public ResearcherRole() { }
        
        public ResearcherRole(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(ResearcherRole.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(ResearcherRole.Name):
                        Name = (string)value!;
                        break;
                    case nameof(ResearcherRole.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(ResearcherRole.Default):
                        Default = (bool)value!;
                        break;
                    case nameof(ResearcherRole.Hidden):
                        Hidden = (bool)value!;
                        break;
                    case nameof(ResearcherRole.FreeText):
                        FreeText = (bool)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; internal set; } = Guid.Empty;
        public string Name  { get; internal set; } = String.Empty;
        public int SortOrder  { get; internal set; } = 0;
        public bool Default  { get; internal set; } = false;
        public bool Hidden  { get; internal set; } = false;
        public bool FreeText  { get; internal set; } = false;
        public ResearcherRole Copy()
        {
            var entity = new ResearcherRole();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            entity.Hidden = Hidden;
            entity.FreeText = FreeText;
            return entity;
        }
    }
}

