using DocumentFormat.OpenXml.Office2010.Excel;
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
    public enum ReportFilterReferenceCondition
    {
        OneOf = 10,
        NotOneOf = 20,

        AllOf = 30,
        Only = 40,
        AllOfAndOnly,

        IsSet = 1000,
        IsNotSet = 1001,
    }

    [ApiType]
    public class ReportFilterReference : ReportFilterField
    {
        public override ReportFilterType FilterType => ReportFilterType.Reference;

        public override IEnumerable<ReportFilterCondition> Conditions => new List<ReportFilterCondition>
        {
            new ReportFilterCondition { Value = (int)ReportFilterReferenceCondition.OneOf, Description = "is one of", Unary = false },
            new ReportFilterCondition { Value = (int)ReportFilterReferenceCondition.NotOneOf, Description = "is not one of", Unary = false },
            new ReportFilterCondition { Value = (int)ReportFilterReferenceCondition.AllOf, Description = "is all of", Unary = false },
            new ReportFilterCondition { Value = (int)ReportFilterReferenceCondition.Only, Description = "is only", Unary = false },
            new ReportFilterCondition { Value = (int)ReportFilterReferenceCondition.AllOfAndOnly, Description = "is all of and only", Unary = false },

            new ReportFilterCondition { Value = (int)ReportFilterReferenceCondition.IsSet, Description = "is set", Unary = true },
            new ReportFilterCondition { Value = (int)ReportFilterReferenceCondition.IsNotSet, Description = "is not set", Unary = true },
        };

        public List<Guid> Value { get; set; } = new List<Guid>();

        public override async Task<bool> Validate(ReportSchema schema, IServiceProvider services)
        {
            if (!await base.Validate(schema, services))
                return false;

            var valueDescription = new StringBuilder();
            if (Value.Count > 0)
            {
                var field = schema.GetField(FieldId);
                if (field == null)
                    return false;

                var mapper = field.GetMapper(services);
                if (mapper == null)
                    return false;

                Value = Value.Distinct().ToList();

                for (var i = 0; i < Value.Count; i++)
                {
                    var entity = await mapper.Get(Value[i]);
                    if (entity == null)
                    {
                        Value[i] = Guid.Empty; // flag for deletion
                        continue;
                    }

                    var name = mapper.DisplayName(entity);
                    if (valueDescription.Length > 0)
                        valueDescription.Append(", ");

                    valueDescription.Append(name);
                }
                Value.RemoveAll(v => v == Guid.Empty);
            }
            if (Value.Count == 0)
            {
                valueDescription.Append("(empty list)");
            }

            Description = Condition switch
            {
                (int)ReportFilterReferenceCondition.IsNotSet => $"{DisplayName} {ConditionDescription}",
                (int)ReportFilterReferenceCondition.IsSet => $"{DisplayName} {ConditionDescription}",
                _ => $"{DisplayName} {ConditionDescription} {valueDescription}",
            };
            return true;
        }

        public override async Task<bool> Apply(object context, ReportBuilder builder)
        {
            var argument = builder.Request.Arguments.FirstOrDefault(a => a.FilterId == Id);
            var condition = argument?.Condition ?? Condition;
            var value = argument?.GuidsValue ?? Value;

            var field = builder.Schema.Fields.FirstOrDefault(f => f.Id == FieldId);
            if (field == null)
                return false;

            if (field is not ReportReferenceField referenceField)
                throw new InvalidOperationException($"Field {field.Name} is not a reference field");

            var entities = await field.MapContextToEntities(context, builder);

            if (condition == (int)ReportFilterReferenceCondition.IsSet)
                return entities.Count > 0;

            if (condition == (int)ReportFilterReferenceCondition.IsNotSet)
                return entities.Count == 0;

            var ids = new List<Guid>();
            foreach(var entity in entities)
                ids.Add(referenceField.GetMapper(builder.ServiceProvider).Id(entity));

            if(condition == (int)ReportFilterReferenceCondition.OneOf)
            {
                foreach(var v in value)
                {
                    if(ids.Contains(v))
                        return true;
                }
                return false;
            }

            if (condition == (int)ReportFilterReferenceCondition.NotOneOf)
            {
                foreach (var v in value)
                {
                    if (ids.Contains(v))
                        return false;
                }
                return true;
            }

            if (condition == (int)ReportFilterReferenceCondition.AllOf)
            {
                foreach(var v in value)
                {
                    if(!ids.Contains(v))
                        return false;
                }
                return true;
            }

            if(condition == (int)ReportFilterReferenceCondition.Only)
            {
                foreach(var id in ids)
                {
                    if(!value.Contains(id))
                        return false;
                }
                return true;
            }

            if (condition == (int)ReportFilterReferenceCondition.AllOfAndOnly)
            {
                foreach (var v in value)
                {
                    if (!ids.Contains(v))
                        return false;
                }
                foreach (var id in ids)
                {
                    if (!value.Contains(id))
                        return false;
                }
                return true;
            }

            throw new InvalidOperationException($"Unknown condition {condition}");
        }

        public override void Update(ReportFilter filter, ReportSchema schema)
        {
            if (filter is not ReportFilterReference typed)
                throw new Exception($"Cannot apply filter of type {filter.FilterType} to filter of type {FilterType}");

            Value = typed.Value;
            base.Update(filter, schema);
        }

        public override Task<ReportArgument?> GenerateArgument(ReportDesign design, IServiceProvider services)
        {
            return Task.FromResult<ReportArgument?>(new ReportArgument
            {
                Name = DisplayName,
                FilterId = Id,
                FieldId = FieldId,
                FieldType = ReportFieldType.Reference,
                Condition = Condition,
                Conditions = Conditions.ToList(),
                GuidsValue = Value,
                ReportProviderId = design.ReportProviderId,
                FilterType = FilterType,
            });
        }

        // Serialization

        public override bool ReadProperty(string propertyName, ref Utf8JsonReader reader)
        {
            if (String.Compare(nameof(Value), propertyName, true) == 0)
            {
                Value = JsonSerializer.Deserialize<List<Guid>>(ref reader, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                                       ?? new List<Guid>();
                return reader.Read();
            }

            return base.ReadProperty(propertyName, ref reader);
        }

        public override void WriteProperties(Utf8JsonWriter writer)
        {
            base.WriteProperties(writer);

            writer.WritePropertyName("value");
            JsonSerializer.Serialize(writer, Value, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
