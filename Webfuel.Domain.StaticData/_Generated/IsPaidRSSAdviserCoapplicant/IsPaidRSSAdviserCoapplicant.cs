using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsPaidRSSAdviserCoapplicant: IStaticData
    {
        public IsPaidRSSAdviserCoapplicant() { }
        
        public IsPaidRSSAdviserCoapplicant(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(IsPaidRSSAdviserCoapplicant.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(IsPaidRSSAdviserCoapplicant.Name):
                        Name = (string)value!;
                        break;
                    case nameof(IsPaidRSSAdviserCoapplicant.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(IsPaidRSSAdviserCoapplicant.Default):
                        Default = (bool)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; internal set; } = Guid.Empty;
        public string Name  { get; internal set; } = String.Empty;
        public int SortOrder  { get; internal set; } = 0;
        public bool Default  { get; internal set; } = false;
        public IsPaidRSSAdviserCoapplicant Copy()
        {
            var entity = new IsPaidRSSAdviserCoapplicant();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            return entity;
        }
    }
}

