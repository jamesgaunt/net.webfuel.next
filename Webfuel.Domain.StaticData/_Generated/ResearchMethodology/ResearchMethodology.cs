using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class ResearchMethodology: IStaticData
    {
        public ResearchMethodology() { }
        
        public ResearchMethodology(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(ResearchMethodology.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(ResearchMethodology.Name):
                        Name = (string)value!;
                        break;
                    case nameof(ResearchMethodology.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(ResearchMethodology.Default):
                        Default = (bool)value!;
                        break;
                    case nameof(ResearchMethodology.Hidden):
                        Hidden = (bool)value!;
                        break;
                    case nameof(ResearchMethodology.FreeText):
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
        public ResearchMethodology Copy()
        {
            var entity = new ResearchMethodology();
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

