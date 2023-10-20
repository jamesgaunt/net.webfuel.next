using FluentValidation;
using Microsoft.Data.SqlClient;
using System.Text.Json.Serialization;

namespace Webfuel.Domain
{
    public partial class Project
    {
        public Project() { }
        
        public Project(SqlDataReader dr)
        {
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var value = dr.GetValue(i);
                var property = dr.GetName(i);
                
                switch (property)
                {
                    case nameof(Project.Id):
                        Id = (Guid)value!;
                        break;
                    case nameof(Project.LinkId):
                        LinkId = (Guid)value!;
                        break;
                    case nameof(Project.Title):
                        Title = (string)value!;
                        break;
                    case nameof(Project.FundingBodyId):
                        FundingBodyId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                    case nameof(Project.ResearchMethodologyId):
                        ResearchMethodologyId = value == DBNull.Value ? (Guid?)null : (Guid?)value;
                        break;
                }
            }
        }
        public Guid Id  { get; set; } = Guid.Empty;
        public Guid LinkId  { get; set; } = Guid.Empty;
        public string Title  { get; set; } = String.Empty;
        public Guid? FundingBodyId { get; set; }
        public Guid? ResearchMethodologyId { get; set; }
        public Project Copy()
        {
            var entity = new Project();
            entity.Id = Id;
            entity.LinkId = LinkId;
            entity.Title = Title;
            entity.FundingBodyId = FundingBodyId;
            entity.ResearchMethodologyId = ResearchMethodologyId;
            return entity;
        }
    }
}

