using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class Gender: IStaticData
    {
        public Gender() { }
        
        public Gender(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(Gender.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(Gender.Name):
                        Name = (string)value!;
                        break;
                    case nameof(Gender.Code):
                        Code = (string)value!;
                        break;
                    case nameof(Gender.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(Gender.Default):
                        Default = (bool)value!;
                        break;
                    case nameof(Gender.Hidden):
                        Hidden = (bool)value!;
                        break;
                    case nameof(Gender.Hint):
                        Hint = (string)value!;
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
        public string Hint  { get; internal set; } = String.Empty;
        public Gender Copy()
        {
            var entity = new Gender();
            entity.Id = Id;
            entity.Name = Name;
            entity.Code = Code;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            entity.Hidden = Hidden;
            entity.Hint = Hint;
            return entity;
        }
    }
}

