using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsFellowship: IStaticData
    {
        public IsFellowship() { }
        
        public IsFellowship(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(IsFellowship.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(IsFellowship.Name):
                        Name = (string)value!;
                        break;
                    case nameof(IsFellowship.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(IsFellowship.Default):
                        Default = (bool)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; internal set; } = Guid.Empty;
        public string Name  { get; internal set; } = String.Empty;
        public int SortOrder  { get; internal set; } = 0;
        public bool Default  { get; internal set; } = false;
        public IsFellowship Copy()
        {
            var entity = new IsFellowship();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            return entity;
        }
    }
}

