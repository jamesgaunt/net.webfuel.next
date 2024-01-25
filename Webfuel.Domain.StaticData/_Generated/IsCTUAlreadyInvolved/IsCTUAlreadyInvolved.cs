using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsCTUAlreadyInvolved: IStaticData
    {
        public IsCTUAlreadyInvolved() { }
        
        public IsCTUAlreadyInvolved(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(IsCTUAlreadyInvolved.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(IsCTUAlreadyInvolved.Name):
                        Name = (string)value!;
                        break;
                    case nameof(IsCTUAlreadyInvolved.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(IsCTUAlreadyInvolved.Default):
                        Default = (bool)value!;
                        break;
                    case nameof(IsCTUAlreadyInvolved.FreeText):
                        FreeText = (bool)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; internal set; } = Guid.Empty;
        public string Name  { get; internal set; } = String.Empty;
        public int SortOrder  { get; internal set; } = 0;
        public bool Default  { get; internal set; } = false;
        public bool FreeText  { get; internal set; } = false;
        public IsCTUAlreadyInvolved Copy()
        {
            var entity = new IsCTUAlreadyInvolved();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            entity.FreeText = FreeText;
            return entity;
        }
    }
}

