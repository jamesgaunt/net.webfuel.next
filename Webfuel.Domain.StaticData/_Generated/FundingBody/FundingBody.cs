using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class FundingBody: IStaticData
    {
        public FundingBody() { }
        
        public FundingBody(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(FundingBody.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(FundingBody.Name):
                        Name = (string)value!;
                        break;
                    case nameof(FundingBody.Code):
                        Code = (string)value!;
                        break;
                    case nameof(FundingBody.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(FundingBody.Default):
                        Default = (bool)value!;
                        break;
                    case nameof(FundingBody.Hidden):
                        Hidden = (bool)value!;
                        break;
                    case nameof(FundingBody.Hint):
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
        public FundingBody Copy()
        {
            var entity = new FundingBody();
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

