using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class SuportRequestStatus: IStaticData
    {
        public SuportRequestStatus() { }
        
        public SuportRequestStatus(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(SuportRequestStatus.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(SuportRequestStatus.Name):
                        Name = (string)value!;
                        break;
                    case nameof(SuportRequestStatus.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(SuportRequestStatus.Default):
                        Default = (bool)value!;
                        break;
                    case nameof(SuportRequestStatus.Hidden):
                        Hidden = (bool)value!;
                        break;
                    case nameof(SuportRequestStatus.FreeText):
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
        public SuportRequestStatus Copy()
        {
            var entity = new SuportRequestStatus();
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

