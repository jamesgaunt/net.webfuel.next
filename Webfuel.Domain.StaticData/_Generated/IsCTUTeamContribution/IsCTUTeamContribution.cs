using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Webfuel.Domain.StaticData
{
    public partial class IsCTUTeamContribution: IStaticData
    {
        public IsCTUTeamContribution() { }
        
        public IsCTUTeamContribution(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(IsCTUTeamContribution.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(IsCTUTeamContribution.Name):
                        Name = (string)value!;
                        break;
                    case nameof(IsCTUTeamContribution.SortOrder):
                        SortOrder = (int)value!;
                        break;
                    case nameof(IsCTUTeamContribution.Default):
                        Default = (bool)value!;
                        break;
                }
            }
        }
        public Guid Id  { get; internal set; } = Guid.Empty;
        public string Name  { get; internal set; } = String.Empty;
        public int SortOrder  { get; internal set; } = 0;
        public bool Default  { get; internal set; } = false;
        public IsCTUTeamContribution Copy()
        {
            var entity = new IsCTUTeamContribution();
            entity.Id = Id;
            entity.Name = Name;
            entity.SortOrder = SortOrder;
            entity.Default = Default;
            return entity;
        }
    }
}

