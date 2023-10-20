using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class FundingCallType: IStaticData
    {
        public FundingCallType() { }
        
        public FundingCallType(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(FundingCallType.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(FundingCallType.Name):
                        Name = (string)value!;
                        break;
                    case nameof(FundingCallType.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(FundingCallType.Default):
                        Default = (bool)value!;
                        break;
                    case nameof(FundingCallType.Hidden):
                        Hidden = (bool)value!;
                        break;
                    case nameof(FundingCallType.FreeText):
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
        public FundingCallType Copy()
        {
            var entity = new FundingCallType();
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

