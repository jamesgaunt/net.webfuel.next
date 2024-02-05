using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Webfuel.Reporting
{
    [ApiEnum]
    public enum ReportFilterBooleanCondition
    {
        True = 10,
        False = 20,
        Any = 99,
    }

    [ApiType]
    public class ReportFilterBoolean : ReportFilterField
    {
        public override ReportFilterType FilterType => ReportFilterType.Boolean;

        public override IEnumerable<ReportFilterCondition> Conditions => new List<ReportFilterCondition>
        {
            new ReportFilterCondition { Value = (int)ReportFilterBooleanCondition.True, Description = "is true", Unary = true },
            new ReportFilterCondition { Value = (int)ReportFilterBooleanCondition.False, Description = "is false", Unary = true },
            new ReportFilterCondition { Value = (int)ReportFilterBooleanCondition.Any, Description = "is any value", Unary = true },
        };

        public override async Task<bool> Validate(ReportSchema schema, IServiceProvider services)
        {
            if(!await base.Validate(schema, services))
                return false;

            Description = $"{DisplayName} {ConditionDescription}";
            return true;
        }

        public override async Task<bool> Apply(object context, ReportBuilder builder)
        {
            var argument = builder.Request.Arguments.FirstOrDefault(a => a.FilterId == Id);
            var condition = argument?.Condition ?? Condition;

            var field = builder.Schema.Fields.FirstOrDefault(f => f.Id == FieldId);
            if (field == null)
                return false;

            if (condition == (int)ReportFilterBooleanCondition.Any)
                return true;

            var untyped = await field.Evaluate(context, builder);
            if (untyped is not bool typed)
                return false;

            if(condition == (int)ReportFilterBooleanCondition.True)
                return typed;

            return !typed;
        }

        public override void Update(ReportFilter filter, ReportSchema schema)
        {
            if (filter is not ReportFilterBoolean typed)
                throw new Exception($"Cannot apply filter of type {filter.FilterType} to filter of type {FilterType}");

            base.Update(filter, schema);
        }

        public override Task<ReportArgument?> GenerateArgument(ReportDesign design, IServiceProvider services)
        {
            return Task.FromResult<ReportArgument?>(new ReportArgument
            {
                Name = DisplayName,
                FilterId = Id,
                FieldId = FieldId,
                FieldType = ReportFieldType.Boolean,
                Condition = Condition,
                Conditions = Conditions.ToList(),
                ReportProviderId = design.ReportProviderId,
                FilterType = FilterType,
            });
        }

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            return base.ReadProperty(propertyName, ref reader);
        }

        public override void WriteProperties(Utf8JsonWriter writer)
        {
            base.WriteProperties(writer);
        }
    }
}
