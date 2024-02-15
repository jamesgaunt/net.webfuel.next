using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class SupportProvided: IStaticData
    {
        public SupportProvided() { }
        
        public SupportProvided(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(SupportProvided.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(SupportProvided.Name):
                        Name = (string)value!;
                        break;
                    case nameof(SupportProvided.Alias):
                        Alias = (string)value!;
                        break;
                    case nameof(SupportProvided.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(SupportProvided.Default):
                        Default = (bool)value!;
                        break;
                    case nameof(SupportProvided.Hidden):
                        Hidden = (bool)value!;
                        break;
                    case nameof(SupportProvided.FreeText):
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
        public SupportProvided Copy()
        {
            var entity = new SupportProvided();
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

