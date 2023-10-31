using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class AgeRange: IStaticData
    {
        public AgeRange() { }
        
        public AgeRange(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(AgeRange.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(AgeRange.Name):
                        Name = (string)value!;
                        break;
                    case nameof(AgeRange.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(AgeRange.Default):
                        Default = (bool)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; internal set; } = Guid.Empty;
        public string Name  { get; internal set; } = String.Empty;
        public int SortOrder  { get; internal set; } = 0;
        public bool Default  { get; internal set; } = false;
        public AgeRange Copy()
        {
            var entity = new AgeRange();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            return entity;
        }
    }
}

