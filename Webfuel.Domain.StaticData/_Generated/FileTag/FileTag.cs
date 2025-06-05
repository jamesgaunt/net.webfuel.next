using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class FileTag: IStaticData
    {
        public FileTag() { }
        
        public FileTag(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(FileTag.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(FileTag.Name):
                        Name = (string)value!;
                        break;
                    case nameof(FileTag.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(FileTag.Default):
                        Default = (bool)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; internal set; } = Guid.Empty;
        public string Name  { get; internal set; } = String.Empty;
        public int SortOrder  { get; internal set; } = 0;
        public bool Default  { get; internal set; } = false;
        public FileTag Copy()
        {
            var entity = new FileTag();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            return entity;
        }
    }
}

