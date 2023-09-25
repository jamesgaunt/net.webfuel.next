using FluentValidation;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    internal class FundingStreamRepositoryAccessor: IRepositoryAccessor<FundingStream>
    {
        private readonly FundingStreamRepositoryValidator _validator = new FundingStreamRepositoryValidator();
        public string DatabaseTable => "FundingStream";
        public string DefaultOrderBy => "ORDER BY Id ASC";
        public object? GetValue(FundingStream entity, string property)
        {
            switch(property)
            {
                case nameof(FundingStream.Id):
                    return entity.Id;
                case nameof(FundingStream.Name):
                    return entity.Name;
                case nameof(FundingStream.Code):
                    return entity.Code;
                case nameof(FundingStream.SortOrder):
                    return entity.SortOrder;
                case nameof(FundingStream.Default):
                    return entity.Default;
                case nameof(FundingStream.Hidden):
                    return entity.Hidden;
                    default: throw new InvalidOperationException($"Unrecognised entity property {property}");
            }
        }
        public void SetValue(FundingStream entity, string property, object? value)
        {
            switch(property)
            {
                case nameof(FundingStream.Id):
                    entity.Id = (Guid)value!;
                    break;
                case nameof(FundingStream.Name):
                    entity.Name = (string)value!;
                    break;
                case nameof(FundingStream.Code):
                    entity.Code = (string)value!;
                    break;
                case nameof(FundingStream.SortOrder):
                    entity.SortOrder = (Double)value!;
                    break;
                case nameof(FundingStream.Default):
                    entity.Default = (bool)value!;
                    break;
                case nameof(FundingStream.Hidden):
                    entity.Hidden = (bool)value!;
                    break;
            }
        }
        public FundingStream CreateInstance()
        {
            return new FundingStream();
        }
        public void Validate(FundingStream entity)
        {
            entity.Name = entity.Name ?? String.Empty;
            entity.Name = entity.Name.Trim();
            entity.Code = entity.Code ?? String.Empty;
            entity.Code = entity.Code.Trim();
            _validator.ValidateAndThrow(entity);
        }
        public IEnumerable<string> InsertProperties
        {
            get
            {
                yield return "Id";
                yield return "Name";
                yield return "Code";
                yield return "SortOrder";
                yield return "Default";
                yield return "Hidden";
            }
        }
        public IEnumerable<string> UpdateProperties
        {
            get
            {
                yield return "Name";
                yield return "Code";
                yield return "SortOrder";
                yield return "Default";
                yield return "Hidden";
            }
        }
    }
}

