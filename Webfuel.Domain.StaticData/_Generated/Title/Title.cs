using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class Title: IStaticData
    {
        public Title() { }
        
        public Title(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(Title.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(Title.Name):
                        Name = (string)value!;
                        break;
                    case nameof(Title.Code):
                        Code = (string)value!;
                        break;
                    case nameof(Title.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(Title.Default):
                        Default = (bool)value!;
                        break;
                    case nameof(Title.Hidden):
                        Hidden = (bool)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; internal set; } = Guid.Empty;
        public string Name  { get; internal set; } = String.Empty;
        public string Code  { get; internal set; } = String.Empty;
        public int SortOrder  { get; internal set; } = 0;
        public bool Default  { get; internal set; } = false;
        public bool Hidden  { get; internal set; } = false;
        public Title Copy()
        {
            var entity = new Title();
            entity.Id = Id;
            entity.Name = Name;
            entity.Code = Code;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            entity.Hidden = Hidden;
            return entity;
        }
    }
}
