using System;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public class SchemaEntityPropertyInt32 : SchemaEntityPropertyPrimative
    {
        public SchemaEntityPropertyInt32(SchemaEntity entity, XElement element, bool nullable, string tag)
            : base(entity, element, new PrimativeInt32(nullable))
        {
            var minMax = ParseMinMaxTag(tag);
            MinValue = (int?)minMax.min;
            MaxValue = (int?)minMax.max;
        }

        public int? MinValue { get; private set; }

        public int? MaxValue { get; private set; }

        // Generators

        public override IEnumerable<string> GenerateValidationMetadata()
        {
            if (MinValue.HasValue)
                yield return $"public const int {Name}_MinValue = {MinValue};";
            if (MaxValue.HasValue)
                yield return $"public const int {Name}_MaxValue = {MaxValue};";
        }

        public override IEnumerable<string> GenerateValidationRules()
        {
            if (MinValue.HasValue)
                yield return $".GreaterThanOrEqualTo({Name}_MinValue)" + (Nullable ? ".When(x => x != null, ApplyConditionTo.CurrentValidator)" : "");

            if (MaxValue.HasValue)
                yield return $".LessThanOrEqualTo({Name}_MaxValue)" + (Nullable ? ".When(x => x != null, ApplyConditionTo.CurrentValidator)" : "");
        }
    }
}
